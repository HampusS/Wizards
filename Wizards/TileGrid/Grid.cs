using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.Utilities;

namespace Wizards.TileGrid
{
    class Grid
    {
        public int width, height;
        Texture2D texture;
        Tile[,] grid;
        int size;

        public Grid(Texture2D texture, int size, int columns, int rows)
        {
            this.texture = texture;
            this.size = size;
            width = columns;
            height = rows;

            CreateTileGrid();
        }

        public void CreateTileGrid()
        {
            grid = new Tile[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = new Tile(texture, new Vector2(i * size, j * size), size);
                }
            }
        }

        public Vector2 ReturnTileCenter(Vector2 pos)
        {
            int tempX, tempY;
            tempX = (int)pos.X / size;
            tempY = (int)pos.Y / size;
            if (tempX < width && tempX >= 0 && tempY < height && tempY >= 0)
                if (grid[tempX, tempY].ContainsVector(pos))
                return grid[tempX, tempY].myCenter();
            return Vector2.Zero;
        }

        public void DrawOccupiedTile(Vector2 pos, Vector2 futurePos)
        {
            int tempX, tempY;
            tempX = (int)pos.X / size;
            tempY = (int)pos.Y / size;
            if (tempX < width && tempX >= 0 && tempY < height && tempY >= 0)
            {
                if (grid[tempX, tempY].ContainsVector(pos))
                    grid[tempX, tempY].myColor = Color.Red;
                if(!grid[tempX, tempY].ContainsVector(futurePos))
                    grid[tempX, tempY].myColor = Color.DarkGreen;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
