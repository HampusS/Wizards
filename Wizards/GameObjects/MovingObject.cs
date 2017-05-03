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
    class MovingObject : TimedObject
    {
        protected Vector2 m_vVelocity, m_vAcceleration;

        protected float m_fFriction = Settings.DefaultFriction;

        public Vector2 myVelocity
        {
            get { return m_vVelocity; }
            set { m_vVelocity = value; }
        }

        public float myFriction
        {
            get { return m_fFriction; }
            set { m_fFriction = value; }
        }

        public MovingObject(Texture2D texture, Vector2 position, int radius)
            : base(texture, position, radius)
        {
            m_vAcceleration = Vector2.Zero;
            this.m_fMass = 10;
            this.m_fRestitution = 1;
            this.m_fFriction = 0;
        }

        public override void Update(float time)
        {
            m_vPosition += time * (m_vVelocity + (m_vAcceleration * time) / 2);
            m_vVelocity += (m_vAcceleration * time);
            m_vVelocity *= m_fFriction;
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void SetPhysics(float mass, float restitution)
        {
            this.m_fMass = mass;
            this.m_fRestitution = restitution;
        }
    }
}
