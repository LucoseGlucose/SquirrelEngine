using OpenTK;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Collections.Generic;
using SquirrelEngine.Graphics;
using SquirrelEngine.Components;

namespace SquirrelEngine.Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App.Run(Scene.CreateScene(() =>
            {
                Scene scene = new("Scene");

                CameraComponent cam = scene.CreateObject("Camera").AddComponent<CameraComponent>();
                cam.Transform.position = new(0f, 0f, -10f);
                cam.settings = new Vector4(MathHelper.DegreesToRadians(60f), 2f, .1f, 100f);
                cam.Object.AddComponent<CameraControllerComponent>();

                SkyboxComponent sky = scene.CreateObject("Sky").AddComponent<SkyboxComponent>();
                sky.mesh = Mesh.ImportMesh("../../../Resources/Models/Inverted Cube.obj");

                ModelComponent cube = scene.CreateObject("Cube").AddComponent<ModelComponent>();
                cube.mesh = Mesh.ImportMesh("../../../Resources/Models/Cube.obj");
                cube.Transform.rotation = new(0f, 180f, 0f);

                LightComponent light = scene.CreateObject("Light").AddComponent<LightComponent>();
                light.strength = .2f;
                light.Transform.position = new Vector3(0f, 0f, -5f);
                light.type = LightType.Directional;

                scene.onStart = () =>
                {
                    cube.material.SetProperty("_mainTex", Texture.CreateTexture("../../../Resources/Textures/SquirrelEngine.png"));
                    cube.material.SetProperty("_metallic", .08f);
                    cube.material.SetProperty("_smoothness", .75f);

                    sky.material = new("../../../Shaders/SkyboxVertexShader.glsl", "../../../Shaders/SkyboxFragmentShader.glsl");
                    sky.material.SetProperty("_cubeMap", Texture.CreateCubemap(new string[6]
                    {
                        "../../../Resources/Textures/Skybox/right.jpg",
                        "../../../Resources/Textures/Skybox/left.jpg",
                        "../../../Resources/Textures/Skybox/top.jpg",
                        "../../../Resources/Textures/Skybox/bottom.jpg",
                        "../../../Resources/Textures/Skybox/front.jpg",
                        "../../../Resources/Textures/Skybox/back.jpg"
                    }));
                };

                scene.onUpdate = () => { cube.Transform.rotation += new Vector3(0f, 10f, 0f) * Time.DeltaTime; };

                return scene;
            }));
        }
    }
}