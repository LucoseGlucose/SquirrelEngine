using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace SquirrelEngine.Graphics
{
    internal class Material
    {
        public List<ShaderProperty> properties;
        public ShaderProgram shader;

        public Material(ShaderProgram shader)
        {
            properties = new();
            shader = ShaderProgram.CreateShaderProgram(shader.VertexShader.FilePath, shader.FragmentShader.FilePath);
            GL.GetProgram(shader.ID, GetProgramParameterName.ActiveUniforms, out int paramCount);

            for (int i = 0; i < paramCount; i++)
            {
                object val = null;
                GL.GetActiveUniform(shader.ID, i, 16, out _, out _, out ActiveUniformType varType, out string varName);
                properties.Add(new(GraphicsUtils.UniformToCSType(varType), varName, val, shader));
            }

            this.shader = shader;
        }
        public ShaderProperty SetProperty(string propety, object value)
        {
            if (properties.Count < 1) return null;
            ShaderProperty prop = properties.Find(p => p.Name == propety);

            if (prop != null && prop.CSType == value.GetType())
            {
                prop.SetValue(value);
                return prop;
            }
            return null;
        }
        public object GetProperty(string propety)
        {
            ShaderProperty prop = properties.Find(p => p.Name == propety);
            if (prop != null) return prop.GetValue();
            return null;
        }
    }
}
