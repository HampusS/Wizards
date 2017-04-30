using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GameObjects;

namespace Wizards.Interfaces
{
    class Spell : MovingObject
    {
        public Wizard m_gOwner;
        float speed = 200;

        float lifetime = 7, lifecounter;

        public void KillMe()
        {
            lifecounter = 200;
        }

        public Wizard getMyParent()
        {
            return m_gOwner;
        }

        public Spell(Texture2D texture, Vector2 position, int radius, Vector2 target)
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            this.m_gOwner = null;
            TurnToVector(target);
            this.m_vVelocity = new Vector2((float)Math.Cos(m_fAngle), (float)Math.Sin(m_fAngle)) * speed;
            color = Color.MediumPurple;
            m_fFriction = 1;
            m_fMass = 50f;
            scaleModifier = m_iRadius / defaultRadius;
            m_fScale *= scaleModifier;
        }

        public Spell(Texture2D texture, Vector2 position, int radius, Wizard parent)
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            this.m_gOwner = parent;
            this.m_fAngle = parent.myAngle;
            this.m_vVelocity = new Vector2((float)Math.Cos(m_fAngle), (float)Math.Sin(m_fAngle)) * speed;
            m_vPosition += new Vector2((float)Math.Cos(m_fAngle), (float)Math.Sin(m_fAngle)) * (parent.getRadius() + radius);

            if (Vector2.Dot(Vector2.Normalize(parent.myVelocity), Vector2.Normalize(m_vVelocity)) >= 0.9f)
            {
                this.m_vVelocity = parent.myVelocity + new Vector2((float)Math.Cos(m_fAngle), (float)Math.Sin(m_fAngle)) * speed;
            }

            color = Color.MediumPurple;
            m_fFriction = 1;
            m_fMass = 50f;
            scaleModifier = m_iRadius / defaultRadius;
            m_fScale *= scaleModifier;
        }

        public override void Update(float time)
        {
            lifecounter += time;
            if (lifecounter > lifetime)
                alive = false;
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
