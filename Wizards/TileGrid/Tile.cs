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
        public enum MyType
        {
            Ground,
            Ice,
            Void,
        }
        public MyType myType = MyType.Void;
        Texture2D texture;
        Vector2 m_vPosition;
        int m_iSize;
        Color color;

        float m_fFriction;

        public bool isOccupied = false;
        public bool ContainsObstacle = false;

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

        public Tile(Texture2D texture, Vector2 position, int size, MyType type)
        {
            this.texture = texture;
            this.m_vPosition = position;
            this.m_iSize = size;
            this.myType = type;
            if (myType == MyType.Ground)
            {
                m_fFriction = 0.95f;
                color = Color.Green;
            }
            else if (myType == MyType.Ice)
            {
                m_fFriction = 0.98f;
                color = Color.CornflowerBlue;
            }
            else if (myType == MyType.Void)
            {
                m_fFriction = 0.85f;
                color = Color.Black;
            }
        }

        public void Update()
        {
            switch (myType)
            {
                case MyType.Ground:
                    break;
                case MyType.Ice:
                    break;
                case MyType.Void:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, m_vPosition, color);
        }
    }
}
