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
        protected Vector2 m_vPosition, 
            m_vOrigin;
        protected int m_iRadius;
        protected Color color;
        protected float m_fInterval = 0;
        protected float m_fIntervalLimit = 1.5f;
        protected float m_fAngle;
        protected float m_fRestitution = 1;

        protected float m_fScale = 1;

        protected bool alive = true;
        protected Texture2D tri;
        protected Vector2 triOrigin;

        public bool CanShoot;

        protected float m_fStrength, m_fStrengthLimit;
        protected float m_fMass;

        protected float defaultRadius = Settings.circleRadius;
        protected float scaleModifier;

        public float myIntervalLimit
        {
            get { return m_fIntervalLimit; }
            set { m_fIntervalLimit = value; }
        }

        public float myMass
        {
            get { return m_fMass; }
            set { m_fMass = value; }
        }

        public float myStrength
        {
            get { return m_fStrength; }
            private set { m_fStrength = value; }
        }

        public float myAngle
        {
            get { return m_fAngle; }
            set { m_fAngle = value; }
        }

        public float getRestitution()
        {
            return m_fRestitution;
        }

        public int getRadius()
        {
            return m_iRadius;
        }

        public bool isAlive
        {
            get { return alive; }
            set { alive = value; }
        }

        public float mySize
        {
            get { return m_fScale; }
            set { m_fScale = value; }
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
            triOrigin = new Vector2(TextureManager.hat.Width / 2, 0);
            m_fStrength = 0.1f;
            m_fStrengthLimit = 1;
        }

        public virtual void Update(float time)
        {
            m_fInterval += time;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_vPosition, null, color, m_fAngle, m_vOrigin, m_fScale, SpriteEffects.None, 0);
        }

        public virtual bool isInRange(Vector2 target, int tileRange)
        {
            if (Vector2.Distance(m_vPosition, target) < Settings.TileSize * tileRange)
                return true;
            return false;
        }

        public virtual bool isReadyToShoot()
        {
            if (m_fInterval > m_fIntervalLimit)
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
    }
}
