using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using SquirrelEngine.Graphics;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace SquirrelEngine.Core
{
    internal static class App
    {
        public static Scene CurentScene { get; private set; }
        public static GameWindow GameWindow { get; private set; }

        public static void Run(Scene scene)
        {
            CurentScene = scene;

            NativeWindowSettings nativeWindowSettings = new();
            nativeWindowSettings.Size = new Vector2i(1280, 720);
            nativeWindowSettings.Title = "SquirrelEngine";

            using (Image<Rgba32> icon = Image.Load<Rgba32>("../../../Resources/SquirrelEngine.png"))
                nativeWindowSettings.Icon = new(new OpenTK.Windowing.Common.Input.Image
                    (icon.Width, icon.Height, GraphicsUtils.GetImagePixelData(icon)));

            GameWindowSettings gameWindowSettings = new();
            gameWindowSettings.RenderFrequency = 60;
            gameWindowSettings.UpdateFrequency = 60;

            GameWindow = new(gameWindowSettings, nativeWindowSettings);

            GameWindow.Load += () =>
            {
                scene.Start();
                Rendering.Start();
            };

            GameWindow.RenderFrame += (frameArgs) =>
            {
                Time.Update((float)frameArgs.Time);
                CurentScene.Update();
                Rendering.Render(CurentScene);
                GameWindow.SwapBuffers();
            };

            GameWindow.Run();
        }
    }
}
