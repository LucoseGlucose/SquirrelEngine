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
        public ShaderProgram Shader { get; private set; }
        public List<KeyValuePair<string, Texture>> Textures { get; private set; }

        public Material(string vertLoc, string fragLoc)
        {
            CreateNew(vertLoc, fragLoc);
        }
        private void CreateNew(string vertLoc, string fragLoc)
        {
            properties = new();
            Shader = ShaderProgram.CreateShaderProgram(vertLoc, fragLoc);
            GL.GetProgram(Shader.ID, GetProgramParameterName.ActiveUniforms, out int paramCount);

            for (int i = 0; i < paramCount; i++)
            {
                object val = null;
                GL.GetActiveUniform(Shader.ID, i, 16, out _, out _, out ActiveUniformType varType, out string varName);
                properties.Add(new(GraphicsUtils.UniformToCSType(varType), varName, val, Shader));
            }

            Textures = new();
        }
        public ShaderProperty SetProperty(string property, object value)
        {
            if (properties.Count < 1) return null;
            ShaderProperty prop = properties.Find(p => p.Name == property);

            if (prop != null && prop.CSType == value.GetType())
            {
                if (prop.CSType == typeof(Texture))
                {
                    if (!Textures.Any(tex => tex.Key == property)) Textures.Add(new(property, (Texture)value));
                    else Textures[Textures.IndexOf(Textures.Find(tex => tex.Key == property))] = new(property, (Texture)value);

                    prop.SetValue(Textures.IndexOf(Textures.Find(tex => tex.Key == property)));
                }
                else prop.SetValue(value);

                return prop;
            }
            return null;
        }
        public object GetProperty(string property)
        {
            ShaderProperty prop = properties.Find(p => p.Name == property);
            if (prop != null) return prop.GetValue();
            return null;
        }
    }
}
