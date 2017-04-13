using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards.Utilities
{
    class TextureManager
    {
        public static Texture2D square, circle, triangle;

        public static void LoadTextures(GraphicsDevice graphicsDevice)
        {
            square = createSolidRectangle(Settings.TileSize, Settings.TileSize, graphicsDevice);
            circle = createCircleText(graphicsDevice, Settings.circleRadius * 2);
            triangle = createSolidTriangle(Settings.TileSize, Settings.TileSize, graphicsDevice);
        }

        static Texture2D createCircleText(GraphicsDevice graphicsDevice, int radius)
        {
            Texture2D texture = new Texture2D(graphicsDevice, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = new Color(0.7f, 0.7f, 0.7f, 0.8f);
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }

        static Texture2D createSolidRectangle(int width, int height, GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];

            // Colour the entire texture.             
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new Color(0.2f, 0.2f, 0.2f, 0.3f);
            }
            for (int i = 0; i < width; i++)
            {
                data[i] = Color.White;
            }
            for (int i = 0; i < width * height - width; i += width)
            {
                data[i] = Color.White;
            }
            for (int i = width - 1; i < width * height; i += width)
            {
                data[i] = Color.White;
            }
            for (int i = width * height - width; i < width * height; i++)
            {
                data[i] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }

        static Texture2D createSolidTriangle(int width, int height, GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];
            
            // Colour the entire texture transparent.             
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Transparent;
            }

            // color left side
            bool skip = false;
            for (int i = width / 2; i < width * height; i += width)
            {
                if (skip == false)
                {
                    skip = true;
                    i--;
                }
                else
                    skip = false;
                data[i] = Color.White;
            }
            skip = false;
            // color right side
            for (int i = width / 2 - 1; i < width * height; i += width)
            {
                if (skip == false)
                {
                    skip = true;
                    i++;
                }
                else
                    skip = false;
                data[i] = Color.White;
            }
            // Color bottom row
            for (int i = width * height - width; i < width * height; i++)
            {
                data[i] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }
    }
}
