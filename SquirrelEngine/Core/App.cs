using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using SquirrelEngine.Graphics;

namespace SquirrelEngine.Core
{
    internal static class App
    {
        public static Scene CurentScene { get; private set; }

        public static void Run(Scene scene)
        {
            CurentScene = scene;

            NativeWindowSettings nativeWindowSettings = new();
            nativeWindowSettings.Size = new Vector2i(1280, 720);
            nativeWindowSettings.Title = "Game Engine";

            GameWindowSettings gameWindowSettings = new();
            gameWindowSettings.RenderFrequency = 60;
            gameWindowSettings.UpdateFrequency = 60;

            GameWindow gameWindow = new(gameWindowSettings, nativeWindowSettings);

            gameWindow.Load += () =>
            {
                scene.Start();
                Rendering.Init();
            };

            gameWindow.RenderFrame += (frameArgs) =>
            {
                Time.Update((float)frameArgs.Time);
                CurentScene.Update();
                Rendering.Render(CurentScene);
                gameWindow.SwapBuffers();
            };

            gameWindow.Run();
        }
    }
}
