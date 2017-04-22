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
        Vector2 m_vPosition;
        int m_iSize;
        Color color;

        float m_fFriction;

        public float myFriction
        {
            get { return m_fFriction; }
            set { m_fFriction = value; }
        }

        public Color myColor
        {
            get { return color; }
            set { color = value; }
        }

        public Rectangle HitBox()
        {
            return new Rectangle((int)m_vPosition.X, (int)m_vPosition.Y, m_iSize, m_iSize);
        }

        public Vector2 myPosition
        {
            get { return m_vPosition; }
            set { m_vPosition = value; }
        }

        public Vector2 getCenter()
        {
            return new Vector2(m_vPosition.X + (m_iSize / 2), m_vPosition.Y + (m_iSize / 2)); 
        }

        public bool ContainsVector(Vector2 vect)
        {
            return HitBox().Contains(vect) ? true : false;
        }

        public Tile(Texture2D texture, Vector2 position, int size)
        {
            this.texture = texture;
            this.m_vPosition = position;
            this.m_iSize = size;
            color = Color.DarkGreen;
            m_fFriction = 1;
        }

        public Tile(Texture2D texture, Vector2 position, int size, Color color, float friction)
        {
            this.texture = texture;
            this.m_vPosition = position;
            this.m_iSize = size;
            this.color = color;
            this.m_fFriction = friction;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, m_vPosition, color);
        }
    }
}
