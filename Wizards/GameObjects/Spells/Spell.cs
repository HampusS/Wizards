using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GameObjects;
using Wizards.ParticleEngine.Emitters;
using Wizards.Utilities;

namespace Wizards.Interfaces
{
    class Spell : MovingObject
    {
        public GameObject m_gOwner;
        float speed = 200;

        ArcaneEmitter emitter;

        public GameObject getMyParent()
        {
            return m_gOwner;
        }

        public Spell(Texture2D texture, Vector2 position, int radius, GameObject parent)
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            this.m_gOwner = parent;
            this.m_fAngle = parent.myAngle;
            this.m_vVelocity = new Vector2((float)Math.Cos(m_fAngle), (float)Math.Sin(m_fAngle)) * speed;
            m_vPosition += new Vector2((float)Math.Cos(m_fAngle), (float)Math.Sin(m_fAngle)) * (parent.getRadius() + radius);

            //Speed at an angle, Fix

            //if (Vector2.Dot(Vector2.Normalize(parent.myVelocity), Vector2.Normalize(m_vVelocity)) >= 0.9f)
            //{
            //    this.m_vVelocity = parent.myVelocity + new Vector2((float)Math.Cos(m_fAngle), (float)Math.Sin(m_fAngle)) * speed;
            //}

            color = Color.MediumPurple;
            m_fFriction = 1;
            m_fMass = 50f;
            scaleModifier = Math.Min(m_iRadius, defaultRadius) / Math.Max(m_iRadius, defaultRadius);
            m_fScale *= scaleModifier;
            emitter = new ArcaneEmitter(TextureManager.smooth, m_vPosition, m_iRadius, this);
        }

        public override void Update(float time)
        {
            emitter.Update(time);
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            emitter.Draw(spriteBatch);
        }
    }
}
