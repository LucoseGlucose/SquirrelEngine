using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SquirrelEngine.Graphics
{
    internal class Shader
    {
        public int ID { get; private set; }
        public string FilePath { get; private set; }
        public ShaderType Type { get; private set; }

        public Shader(int ID, string FilePath, ShaderType Type)
        {
            this.ID = ID;
            this.FilePath = FilePath;
            this.Type = Type;
        }

        public static Shader CreateShader(string fileLocation, ShaderType type)
        {
            int shaderID = GL.CreateShader(type);
            GL.ShaderSource(shaderID, File.ReadAllText(fileLocation));
            GL.CompileShader(shaderID);

            string shaderLog = GL.GetShaderInfoLog(shaderID);
            if (!string.IsNullOrEmpty(shaderLog)) throw new Exception(shaderLog);

            return new Shader(shaderID, fileLocation, type);
        }
    }
}
