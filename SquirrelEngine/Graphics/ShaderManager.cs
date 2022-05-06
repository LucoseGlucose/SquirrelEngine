using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace SquirrelEngine.Graphics
{
    internal static class ShaderManager
    {
        public static ShaderProgram defaultShader;

        public static void Init()
        {
            defaultShader = ShaderProgram.CreateShaderProgram(
                @"..\..\..\Shaders\StandardVertexShader.glsl", @"..\..\..\Shaders\StandardFragmentShader.glsl");
        }
    }
}
