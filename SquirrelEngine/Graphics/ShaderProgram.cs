using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace SquirrelEngine.Graphics
{
    internal class ShaderProgram
    {
        public int ID { get; private set; }
        public Shader FragmentShader { get; private set; }
        public Shader VertexShader { get; private set; }

        public ShaderProgram(int id, Shader vert, Shader frag)
        {
            ID = id;
            VertexShader = vert;
            FragmentShader = frag;
        }

        public static ShaderProgram CreateShaderProgram(string vertLoc, string fragLoc)
        {
            int programID = GL.CreateProgram();
            Shader vert = Shader.CreateShader(vertLoc, ShaderType.VertexShader);
            Shader frag = Shader.CreateShader(fragLoc, ShaderType.FragmentShader);

            GL.AttachShader(programID, vert.ID);
            GL.AttachShader(programID, frag.ID);
            GL.LinkProgram(programID);

            GL.DetachShader(programID, vert.ID);
            GL.DetachShader(programID, frag.ID);

            GL.DeleteShader(vert.ID);
            GL.DeleteShader(frag.ID);

            string programLog = GL.GetProgramInfoLog(programID);
            if (!string.IsNullOrEmpty(programLog)) throw new Exception(programLog);

            return new ShaderProgram(programID, vert, frag);
        }
    }
}
