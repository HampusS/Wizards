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

        protected float scale = 1, maxScale = 1.0f, minScale = 0.9f;
        protected float lerpTime;
        protected bool grow;
        protected bool alive = true;


        public bool isAlive
        {
            get { return alive; }
            set { alive = value; }
        }

        public int myRadius()
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

        public virtual bool CheckCircleCollision(GameObject targetObj)
        {
            float totalRadius = m_iRadius + targetObj.myRadius();
            Vector2 deltaPos = targetObj.myPosition - m_vPosition;
            return totalRadius * totalRadius > (deltaPos.X * deltaPos.X) + (deltaPos.Y * deltaPos.Y);
        }

        public GameObject(Texture2D texture, Vector2 position, int radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            color = Color.Blue;
            m_vOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public virtual void Update(float time)
        {
            m_fInterval += time;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_vPosition, null, color, m_fAngle, m_vOrigin, scale, SpriteEffects.None, 0);
        }

        public virtual bool isInRange(Vector2 target, int tileRange)
        {
            if (Vector2.Distance(m_vPosition, target) < Settings.TileSize * tileRange)
            {
                TurnToVector(target);
                return true;
            }
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
            m_fAngle = (float)Math.Atan2(temp.Y, temp.X) + (float)(Math.PI / 2);
        }

        public float LerpSize(float value1, float value2, float amount)
        {
            return (float)(value1 * (1.0 - amount)) + (value2 * amount);
        }

        public void AnimateMe(float time)
        {
            if (lerpTime < 0 && grow == false)
                grow = true;
            else if (lerpTime > 1 && grow == true)
                grow = false;

            if (grow)
                lerpTime += time * 8;
            else
                lerpTime -= time * 4;

            scale = LerpSize(minScale, maxScale, lerpTime);
        }
    }
}
