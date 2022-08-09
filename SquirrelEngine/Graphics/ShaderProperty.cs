using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace SquirrelEngine.Graphics
{
    internal class ShaderProperty
    {
        public Type CSType { get; private set; }
        public string Name { get; private set; }
        private object value;
        private ShaderProgram shader;

        public ShaderProperty(Type type, string name, object value, ShaderProgram shader)
        {
            CSType = type;
            Name = name;
            this.value = value;
            this.shader = shader;
        }
        public ShaderProperty(string name, object value, ShaderProgram shader)
        {
            CSType = value.GetType();
            Name = name;
            this.value = value;
            this.shader = shader;
        }
        public void SetValue(object value)
        {
            if (value.GetType() != CSType) return;
            this.value = value;

            GL.UseProgram(shader.ID);

            if (CSType == typeof(int))
                GL.Uniform1(GL.GetUniformLocation(shader.ID, Name), (int)value);
            else if (CSType == typeof(Matrix4))
            {
                Matrix4 mat = (Matrix4)value;
                GL.UniformMatrix4(GL.GetUniformLocation(shader.ID, Name), false, ref mat);
            }
            else if (CSType == typeof(uint))
                GL.Uniform1(GL.GetUniformLocation(shader.ID, Name), (uint)value);
            else if (CSType == typeof(float))
                GL.Uniform1(GL.GetUniformLocation(shader.ID, Name), (float)value);
            else if (CSType == typeof(double))
                GL.Uniform1(GL.GetUniformLocation(shader.ID, Name), (double)value);
            else if (CSType == typeof(Vector2))
                GL.Uniform2(GL.GetUniformLocation(shader.ID, Name), (Vector2)value);
            else if (CSType == typeof(Vector3))
                GL.Uniform3(GL.GetUniformLocation(shader.ID, Name), (Vector3)value);
            else if (CSType == typeof(Vector4))
                GL.Uniform4(GL.GetUniformLocation(shader.ID, Name), (Vector4)value);
            else if (CSType == typeof(Matrix2))
            {
                Matrix2 mat = (Matrix2)value;
                GL.UniformMatrix2(GL.GetUniformLocation(shader.ID, Name), false, ref mat);
            }
            else if (CSType == typeof(Matrix3))
            {
                Matrix3 mat = (Matrix3)value;
                GL.UniformMatrix3(GL.GetUniformLocation(shader.ID, Name), false, ref mat);
            }
        }
        public object GetValue()
        {
            return value;
        }
    }
}
