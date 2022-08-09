using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Core;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SquirrelEngine.Components;

namespace SquirrelEngine.Graphics
{
    internal class Rendering
    {
        public static CameraComponent CurrentCamera { get; private set; }
        public static float ambientLight = .2f;
        public static float specularFalloff = 10f;

        public static void Start()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.TextureCubeMap);
            GL.Enable(EnableCap.Blend);

            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.DepthFunc(DepthFunction.Less);
            GL.ClearColor(.5f, .5f, .5f, 1f);
            GL.CullFace(CullFaceMode.Back);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }
        public static void Render(Scene scene)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            CameraComponent[] cameras = scene.FindComponentsOfType<CameraComponent>();

            foreach (CameraComponent cam in cameras)
            {
                CurrentCamera = cam;

                ModelComponent[] models = scene.FindComponentsOfType<ModelComponent>();
                foreach (ModelComponent model in models)
                {
                    model.OnBeforeRender();

                    GL.UseProgram(model.material.Shader.ID);
                    for (int i = 0; i < model.material.Textures.Count; i++)
                    {
                        Texture tex = model.material.Textures.ElementAt(i).Value;
                        GL.BindTexture(tex.textureType, tex.ID);
                    }
                    GL.BindVertexArray(model.glVao);
                    GL.DrawArrays(PrimitiveType.Triangles, 0, model.FaceCount);

                    GL.BindVertexArray(0);
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                    GL.BindTexture(TextureTarget.TextureCubeMap, 0);
                    GL.UseProgram(0);

                    model.OnAfterRender();
                }
            }
        }
    }
}