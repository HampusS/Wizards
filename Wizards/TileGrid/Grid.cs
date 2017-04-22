using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GameObjects;
using Wizards.Utilities;

namespace Wizards.TileGrid
{
    class Grid
    {
        public int width, height;
        Texture2D texture;
        Tile[,] grid;
        int size;
        float fallOff = 0.8f;
        Random rnd;

        public Grid(Texture2D texture, int size, int columns, int rows)
        {
            this.texture = texture;
            this.size = size;
            width = columns;
            height = rows;
            rnd = new Random();
            CreateTileGrid();
        }

        public void CreateTileGrid()
        {
            grid = new Tile[width, height];
            // SET grid voided tiles
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = new Tile(texture, new Vector2(i * size, j * size), size, Color.Black, 0.99f);
                }
            }
            Random rnd = new Random();

            // SET Grass
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    float distance = Vector2.Distance(new Vector2(grid[i, j].getCenter().X / size, grid[i, j].getCenter().Y / size),
                        new Vector2(getGridCenter().X / size, getGridCenter().Y / size));

                    double influence = Math.Pow(fallOff, distance);
                    if (influence > 0.065f)
                    {
                        grid[i, j] = new Tile(texture, new Vector2(i * size, j * size), size, Color.DarkGreen, 0.95f);
                    }
                }
            }

            // SET Ice
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    float distance = Vector2.Distance(new Vector2(grid[i, j].getCenter().X / size, grid[i, j].getCenter().Y / size),
                        new Vector2(getGridCenter().X / size, getGridCenter().Y / size));

                    double influence = Math.Pow(fallOff, distance);
                    if (influence > 0.065f)
                    {
                        if (influence < 0.09f)
                            grid[i, j] = new Tile(texture, new Vector2(i * size, j * size), size, Color.LightBlue, 0.98f);
                        else if (rnd.Next(0, 15) == 3)
                        {
                            grid[i, j] = new Tile(texture, new Vector2(i * size, j * size), size, Color.LightBlue, 0.98f);
                            grid[i + 1, j] = new Tile(texture, new Vector2((i + 1) * size, j * size), size, Color.LightBlue, 0.98f);
                            grid[i - 1, j] = new Tile(texture, new Vector2((i - 1) * size, j * size), size, Color.LightBlue, 0.98f);
                            grid[i, j + 1] = new Tile(texture, new Vector2(i * size, (j + 1) * size), size, Color.LightBlue, 0.98f);
                            grid[i, j - 1] = new Tile(texture, new Vector2(i * size, (j - 1) * size), size, Color.LightBlue, 0.98f);
                            grid[i + 1, j + 1] = new Tile(texture, new Vector2((i + 1) * size, (j + 1) * size), size, Color.LightBlue, 0.98f);
                            grid[i + 1, j - 1] = new Tile(texture, new Vector2((i + 1) * size, (j - 1) * size), size, Color.LightBlue, 0.98f);
                            grid[i - 1, j + 1] = new Tile(texture, new Vector2((i - 1) * size, (j + 1) * size), size, Color.LightBlue, 0.98f);
                            grid[i - 1, j - 1] = new Tile(texture, new Vector2((i - 1) * size, (j - 1) * size), size, Color.LightBlue, 0.98f);

                        }
                    }
                }
            }
        }

        public Vector2 getGridCenter()
        {
            int tempX, tempY;
            tempX = (int)width / 2;
            tempY = (int)height / 2;
            return grid[tempX - 1, tempY].getCenter();
        }

        public Vector2 RandomizePosFromCenter(int tileRange)
        {
            int length = tileRange * size;
            Vector2 direction = new Vector2((float)Math.Cos(rnd.Next(360)), (float)Math.Sin(rnd.Next(360)));
            direction.Normalize();
            return getGridCenter() + direction * length;
        }

        public Vector2 ReturnTileCenter(Vector2 pos)
        {
            int tempX, tempY;
            tempX = (int)pos.X / size;
            tempY = (int)pos.Y / size;
            if (tempX < width && tempX >= 0 && tempY < height && tempY >= 0)
                if (grid[tempX, tempY].ContainsVector(pos))
                    return grid[tempX, tempY].getCenter();
            return Vector2.Zero;
        }

        public void SetFrictionToObject(MovingObject obj)
        {
            int tempX, tempY;
            tempX = (int)obj.myPosition.X / size;
            tempY = (int)obj.myPosition.Y / size;
            if (tempX < width && tempX >= 0 && tempY < height && tempY >= 0)
            {
                if (grid[tempX, tempY].ContainsVector(obj.myPosition))
                {
                    obj.myFriction = grid[tempX, tempY].myFriction;
                    if (obj.myFriction < 0.97f)
                        obj.moveState = MovingObject.MoveState.Impulse;
                    else
                        obj.moveState = MovingObject.MoveState.Acceleration;
                }
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
