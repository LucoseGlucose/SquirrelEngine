using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Core;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace SquirrelEngine.Graphics
{
    internal class Rendering
    {
        public static CameraComponent CurrentCamera { get; private set; }
        public static Vector3 ambientLight = .2f * Vector3.One;
        public static float specularFalloff = 10f;
        public static Texture tex;

        public static void Start()
        {
            ShaderManager.Start();
            CurrentCamera = App.CurentScene.FindComponentOfType<CameraComponent>();

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);

            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.DepthFunc(DepthFunction.Less);
            GL.ClearColor(.5f, .5f, .5f, 1f);
        }
        public static void Render(Scene scene)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            ModelComponent[] models = scene.FindComponentsOfType<ModelComponent>();
            foreach (ModelComponent model in models)
            {
                GL.UseProgram(model.material.shader.ID);
                Mesh mesh = model.mesh;
                GL.ActiveTexture(TextureUnit.Texture0);
                int texID = Texture.CreateTexture("../../../Resources/Test.bmp").ID;
                GL.BindTexture(TextureTarget.Texture2D, texID);

                float[] verts = GraphicsUtils.Expand(mesh.Vertices);
                int glVerts = GL.GenBuffer();
                int vao = GL.GenVertexArray();

                GL.BindVertexArray(vao);
                GL.BindBuffer(BufferTarget.ArrayBuffer, glVerts);
                GL.BufferStorage(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferStorageFlags.None);

                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

                float[] normals = GraphicsUtils.Expand(mesh.Normals);
                int glNormals = GL.GenBuffer();

                GL.BindBuffer(BufferTarget.ArrayBuffer, glNormals);
                GL.BufferStorage(BufferTarget.ArrayBuffer, normals.Length * sizeof(float), normals, BufferStorageFlags.None);

                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);

                float[] uvs = GraphicsUtils.Expand(mesh.UVs);
                int glUVs = GL.GenBuffer();

                GL.BindBuffer(BufferTarget.ArrayBuffer, glUVs);
                GL.BufferStorage(BufferTarget.ArrayBuffer, uvs.Length * sizeof(float), uvs, BufferStorageFlags.None);

                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);

                GL.DrawArrays(PrimitiveType.Triangles, 0, verts.Length / 3);

                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.BindVertexArray(0);
                GL.BindTexture(TextureTarget.Texture2D, 0);

                GL.DeleteVertexArray(vao);
                GL.DeleteBuffer(glVerts);
                GL.DeleteBuffer(glNormals);
                GL.DeleteBuffer(glUVs);
                GL.DeleteTexture(texID);
            }
        }
    }
}
