using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquirrelEngine.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp.Processing;

namespace SquirrelEngine.Graphics
{
    internal class Texture
    {
        public int ID { get; private set; }
        private Image<Rgba32>[] images;
        public TextureTarget textureType = TextureTarget.Texture2D;

        public Texture(int ID, Image<Rgba32>[] images, TextureTarget textureType)
        {
            this.ID = ID;
            this.images = images;
            this.textureType = textureType;
        }
        public static Texture CreateTexture(string path)
        {
            int textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            using Image<Rgba32> image = Image.Load<Rgba32>(path);
            image.Mutate(img => img.Flip(FlipMode.Horizontal));
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width,
                image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, GraphicsUtils.GetImagePixelData(image));
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture(textureID, new Image<Rgba32>[1] { image }, TextureTarget.Texture2D);
        }
        public static Texture CreateCubemap(string[] paths)
        {
            int textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.TextureCubeMap, textureID);

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);

            for (int i = 0; i < 6; i++)
            {
                using Image<Rgba32> image = Image.Load<Rgba32>(paths[i]);
                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgba, image.Width,
                    image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, GraphicsUtils.GetImagePixelData(image));
                GL.GenerateMipmap(GenerateMipmapTarget.TextureCubeMap);
            }

            return new Texture(textureID, null, TextureTarget.TextureCubeMap);
        }
        public byte[] GetTextureData()
        {
            return GraphicsUtils.GetImagePixelData(images[0]);
        }
    }
}
