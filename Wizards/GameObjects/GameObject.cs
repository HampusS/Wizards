using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.Utilities;

namespace Wizards.GameObjects
{
    class GameObject
    {
        protected Texture2D m_texture;
        protected Vector2 m_vPosition, m_vOrigin;
        protected int m_iRadius;
        protected Color color;
        protected float m_fInterval = 2;
        protected float m_fAngle;
        protected float m_fRestitution = 1;

        protected float scale = 1, maxScale = 1.0f, minScale = 0.85f;
        protected float lerpTime;
        protected bool grow;
        protected bool alive = true;
        protected Texture2D tri;
        protected Vector2 triOrigin;

        public float getAngle()
        {
            return m_fAngle;
        }

        public bool isAlive
        {
            get { return alive; }
            set { alive = value; }
        }

        public float getRestitution()
        {
            return m_fRestitution;
        }

        public int getRadius()
        {
            return m_iRadius;
        }

        public Vector2 myPosition
        {
            get { return m_vPosition; }
            set { m_vPosition = value; }
        }

        public Color myColor
        {
            get { return color; }
            set { color = value; }
        }

        public GameObject(Texture2D texture, Vector2 position, int radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            color = Color.SkyBlue;
            m_vOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
            tri = TextureManager.triangle;
            triOrigin = new Vector2(tri.Width / 2, tri.Height / 2);
        }

        public virtual void Update(float time)
        {
            m_fInterval += time;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_vPosition, null, color, m_fAngle, m_vOrigin, scale, SpriteEffects.None, 0);
            spriteBatch.Draw(tri, m_vPosition, null, Color.Black, m_fAngle + (float)(Math.PI / 2), triOrigin, 0.9f, SpriteEffects.None, 0);
        }

        public virtual bool isInRange(Vector2 target, int tileRange)
        {
            if (Vector2.Distance(m_vPosition, target) < Settings.TileSize * tileRange)
                return true;
            return false;
        }

        public bool isReadyToShoot()
        {
            if (m_fInterval > 2)
            {
                m_fInterval = 0;
                return true;
            }
            return false;
        }

        public virtual void TurnToVector(Vector2 target)
        {
            Vector2 temp = target - m_vPosition;
            m_fAngle = (float)Math.Atan2(temp.Y, temp.X);
        }

        public float LerpFloat(float value1, float value2, float amount)
        {
            return (value1 * (1.0f - amount)) + (value2 * amount);
        }

        public void ResetScale()
        {
            if (scale < 1)
                scale += 0.01f;
        }

        public void AnimateMe(float time)
        {
            if (lerpTime < 0 && grow == false)
                grow = true;
            else if (lerpTime > 1 && grow == true)
                grow = false;

            if (grow)
                lerpTime += time * 1;
            else
                lerpTime -= time * 1;

            scale = LerpFloat(minScale, maxScale, lerpTime);
        }
    }
}
