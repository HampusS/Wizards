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
    class MovingObject : GameObject
    {
        protected Vector2 m_vVelocity, m_vAcceleration, m_vFuturePosition;

        protected float m_fMass, m_fRestitution;
        protected float m_fFriction = Settings.DefaultFriction;

        public Vector2 myVelocity
        {
            get { return m_vVelocity; }
            set { m_vVelocity = value; }
        }

        public Vector2 myFuturePosition
        {
            get { return m_vFuturePosition; }
        }

        public override bool CheckCircleCollision(GameObject targetObj)
        {
            float totalRadius = m_iRadius + targetObj.myRadius();
            Vector2 deltaPos = targetObj.myPosition - m_vFuturePosition;
            return totalRadius * totalRadius > (deltaPos.X * deltaPos.X) + (deltaPos.Y * deltaPos.Y);
        }

        public MovingObject(Texture2D texture, Vector2 position, int radius)
            : base(texture, position, radius)
        {
            m_vAcceleration = Vector2.Zero;
            m_vFuturePosition = position;
            this.m_fMass = 10;
            this.m_fRestitution = 1;
        }

        public void SolveToStaticCircleCollision(GameObject targetObj)
        {
            float totalRadius = m_iRadius + targetObj.myRadius();
            Vector2 normal = targetObj.myPosition - m_vPosition;
            normal.Normalize();

            m_vPosition = targetObj.myPosition + (-normal * (totalRadius + 0.0001f));
            m_vVelocity = Vector2.Reflect(m_vVelocity * m_fRestitution, -normal);
        }

        public override void Update(float time)
        {
            m_vPosition += time * (m_vVelocity + (m_vAcceleration * time) / 2);
            m_vVelocity += (m_vAcceleration * time) - (m_fFriction * m_vVelocity);
            m_vFuturePosition = m_vPosition + (m_vVelocity * time) * 2;
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void InitializePhysics(float mass, float restitution)
        {
            this.m_fMass = mass;
            this.m_fRestitution = restitution;
        }
    }
}
