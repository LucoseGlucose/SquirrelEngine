using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using OpenTK.Graphics.OpenGL;

namespace SquirrelEngine.Graphics
{
    internal class Texture
    {
        public int ID { get; private set; }
        private Image<Rgba32> image;

        public Texture(int ID, Image<Rgba32> image)
        {
            this.ID = ID;
            this.image = image;
        }
        public static Texture CreateTexture(string path)
        {
            int textureID = GL.GenTexture();
            Image<Rgba32> image = Image.Load<Rgba32>(path);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width,
                image.Height, 0, PixelFormat.Rgba, PixelType.Byte, GraphicsUtils.GetImagePixelData(image));
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture(textureID, image);
        }
        public byte[] GetTextureData()
        {
            return GraphicsUtils.GetImagePixelData(image);
        }
    }
}
