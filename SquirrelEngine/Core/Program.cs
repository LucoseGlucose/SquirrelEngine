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
                cam.Transform.position = new(0f, 4f, 8f);
                cam.settings = new Vector4(MathHelper.DegreesToRadians(60f), 2f, .1f, 100f);
                cam.Transform.EulerAngles = new Vector3(30f, 0f, 0f);

                ModelComponent monkey = scene.CreateObject("Monkey").AddComponent<ModelComponent>();
                monkey.mesh = Mesh.LoadFromOBJ("../../../Resources/Monkey.obj");
                monkey.Transform.position = new Vector3(-2f, 0f, 0f);

                ModelComponent cube = scene.CreateObject("Cube").AddComponent<ModelComponent>();
                cube.mesh = Mesh.LoadFromOBJ("../../../Resources/Cube.obj");
                cube.Transform.position = new Vector3(2f, 0f, 0f);

                LightComponent light = scene.CreateObject("Light").AddComponent<LightComponent>();
                light.Transform.position = new Vector3(0f, 0f, 5f);
                light.strength = 3f;

                scene.onStart = () =>
                {
                    scene.FindComponentOfType<ModelComponent>("Cube").material.SetProperty("_mainColor", new Vector4(1f, 0f, 0f, 1f));
                    scene.FindComponentOfType<ModelComponent>("Monkey").material.SetProperty("_mainColor", new Vector4(0f, 0f, 1f, 1f));
                };

                return scene;
            }));
        }
    }
}