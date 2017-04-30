using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GameObjects;
using Wizards.GameObjects.Environment;
using Wizards.Utilities;
using static Wizards.TileGrid.Tile;

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
        Vector2 m_vCenter;

        public Grid(Texture2D texture, int size, int columns, int rows)
        {
            this.texture = texture;
            this.size = size;
            width = columns;
            height = rows;
            rnd = new Random();
            CreateTileGrid();
            m_vCenter = getGridCenter();
        }

        public void CreateTileGrid()
        {
            grid = new Tile[width, height];
            // SET grid voided tiles
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = new Tile(texture, new Vector2(i * size, j * size), size, MyType.Void);
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
                    if (influence > 0.085f)
                    {
                        grid[i, j] = new Tile(texture, new Vector2(i * size, j * size), size, MyType.Ground);
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
                    if (influence > 0.085f)
                    {
                        if (influence < 0.12f)
                            grid[i, j] = new Tile(texture, new Vector2(i * size, j * size), size, MyType.Ice);
                        else if (rnd.Next(0, 15) == 3)
                        {
                            grid[i, j] = new Tile(texture, new Vector2(i * size, j * size), size, MyType.Ice);
                            grid[i + 1, j] = new Tile(texture, new Vector2((i + 1) * size, j * size), size, MyType.Ice);
                            grid[i - 1, j] = new Tile(texture, new Vector2((i - 1) * size, j * size), size, MyType.Ice);
                            grid[i, j + 1] = new Tile(texture, new Vector2(i * size, (j + 1) * size), size, MyType.Ice);
                            grid[i, j - 1] = new Tile(texture, new Vector2(i * size, (j - 1) * size), size, MyType.Ice);
                            grid[i + 1, j + 1] = new Tile(texture, new Vector2((i + 1) * size, (j + 1) * size), size, MyType.Ice);
                            grid[i + 1, j - 1] = new Tile(texture, new Vector2((i + 1) * size, (j - 1) * size), size, MyType.Ice);
                            grid[i - 1, j + 1] = new Tile(texture, new Vector2((i - 1) * size, (j + 1) * size), size, MyType.Ice);
                            grid[i - 1, j - 1] = new Tile(texture, new Vector2((i - 1) * size, (j - 1) * size), size, MyType.Ice);

                        }
                    }
                }
            }

            // Prepare Occupied tiles for obstacles
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    float distance = Vector2.Distance(new Vector2(grid[i, j].getCenter().X / size, grid[i, j].getCenter().Y / size),
                        new Vector2(getGridCenter().X / size, getGridCenter().Y / size));

                    double influence = Math.Pow(fallOff, distance);
                    if (influence > 0.085f)
                    {
                        if (rnd.Next(0, 100) > 96)
                        {
                            grid[i, j].ContainsObstacle = true;
                        }
                    }
                }
            }
        }

        public void SetObstacles(List<Obstacle> obstacles)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (grid[i, j].ContainsObstacle)
                    {
                        obstacles.Add(new Obstacle(TextureManager.circle, grid[i,j].getCenter(), rnd.Next((int)(Settings.circleRadius * 0.7f), (int)(Settings.circleRadius * 1.5f))));
                        grid[i, j].isOccupied = true;
                    }
                }
            }
        }

        public Vector2 getGridCenter()
        {
            return grid[(int)width / 2, (int)height / 2].getCenter();
        }

        public Vector2 RandomizePosAwayFromCenter(int tileRange)
        {
            int length = tileRange * size;
            Vector2 direction = new Vector2((float)Math.Cos(rnd.Next(360)), (float)Math.Sin(rnd.Next(360)));
            direction.Normalize();
            Vector2 result = ReturnTileCenter(m_vCenter + direction * length);
            if (CheckOccupied(result))
                result = RandomizePosAwayFromCenter(tileRange);
            return result;
        }

        public bool CheckOccupied(Vector2 pos)
        {
            int tempX, tempY;
            tempX = (int)pos.X / size;
            tempY = (int)pos.Y / size;
            if (tempX < width && tempX >= 0 && tempY < height && tempY >= 0)
                if (grid[tempX, tempY].isOccupied)
                    return true;
            return false;
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

        public void UpdatePlayerTile(Wizard obj)
        {
            int tempX, tempY;
            tempX = (int)obj.myPosition.X / size;
            tempY = (int)obj.myPosition.Y / size;
            if (tempX < width && tempX >= 0 && tempY < height && tempY >= 0)
            {
                if (grid[tempX, tempY].ContainsVector(obj.myPosition))
                {
                    obj.myFriction = grid[tempX, tempY].myFriction;
                    if (grid[tempX, tempY].myType == MyType.Ground)
                        obj.moveState = Wizard.MoveState.Impulse;
                    else if (grid[tempX, tempY].myType == MyType.Ice)
                        obj.moveState = Wizard.MoveState.Acceleration;
                    else if (grid[tempX, tempY].myType == MyType.Void)
                        obj.isInVoid = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (grid[i, j].myType != MyType.Void)
                        grid[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}
