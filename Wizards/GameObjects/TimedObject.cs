using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards.GameObjects
{
    class TimedObject : GameObject
    {
        protected Random rnd;
        protected float m_fLifeTime, m_fStartLife;

        public float myLifeTime
        {
            get { return m_fLifeTime; }
        }

        public TimedObject(Texture2D texture, Vector2 position, int radius)
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            m_fLifeTime = 8;
            m_fStartLife = m_fLifeTime;
        }

        public override void Update(float time)
        {
            m_fLifeTime -= time;

            if (m_fLifeTime < 0)
                alive = false;

            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public float LifePercent()
        {
            return (m_fLifeTime / m_fStartLife);
        }
    }
}
