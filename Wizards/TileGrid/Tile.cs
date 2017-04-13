using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards.TileGrid
{
    class Tile
    {
        Texture2D texture;
        Vector2 position;
        int size;
        Color color;

        public Color myColor
        {
            get { return color; }
            set { color = value; }
        }

        public Rectangle HitBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, size, size);
        }

        public Vector2 myPosition
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 myCenter()
        {
            return new Vector2(position.X + (size / 2), position.Y + (size / 2)); 
        }

        public bool ContainsVector(Vector2 vect)
        {
            return HitBox().Contains(vect) ? true : false;
        }

        public Tile(Texture2D texture, Vector2 position, int size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            color = Color.DarkGreen;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }
}
