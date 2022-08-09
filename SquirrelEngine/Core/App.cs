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

            using (Image<Rgba32> icon = Image.Load<Rgba32>("../../../Resources/Textures/SquirrelEngine.png"))
                nativeWindowSettings.Icon = new(new OpenTK.Windowing.Common.Input.Image
                    (icon.Width, icon.Height, GraphicsUtils.GetImagePixelData(icon)));

            GameWindowSettings gameWindowSettings = new();

            GameWindow = new(gameWindowSettings, nativeWindowSettings);

            GameWindow.Load += () =>
            {
                ShaderManager.Start();
                scene.Start();
                Rendering.Start();
            };

            GameWindow.RenderFrame += (frameArgs) =>
            {
                Time.Update((float)frameArgs.Time);
                CurentScene.Update();
                Rendering.Render(CurentScene);
                GameWindow.SwapBuffers();
                //Console.WriteLine(1f / frameArgs.Time);
            };

            GameWindow.Closing += (cancelArgs) =>
            {
                CurentScene.End();
            };

            GameWindow.Run();
        }
    }
}
