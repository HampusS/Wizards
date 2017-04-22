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
        GameObject m_gOwner;
        float speed = 200;

        float lifetime = 4, lifecounter;

        public void KillMe()
        {
            lifecounter = 200;
        }

        public GameObject getMyParent()
        {
            return m_gOwner;
        }

        public Spell(Texture2D texture, Vector2 position, int radius, Vector2 target, GameObject parent)
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            this.m_gOwner = parent;
            Vector2 direction = target - position;
            direction.Normalize();
            m_fAngle = (float)Math.Atan2(direction.Y, direction.X);
            this.m_vVelocity = direction * speed;
            color = Color.BlanchedAlmond;
            m_fFriction = 1;
            m_fMass = 15f;
            TurnToVector(target);
        }

        public Spell(Texture2D texture, Vector2 position, int radius, GameObject parent)
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            this.m_gOwner = parent;
            this.m_fAngle = parent.getAngle();
            this.m_vVelocity = new Vector2((float)Math.Cos(m_fAngle), (float)Math.Sin(m_fAngle)) * speed;
            color = Color.BlanchedAlmond;
            m_fFriction = 1;
            m_fMass = 15f;
        }

        public override void Update(float time)
        {
            lifecounter += time;
            base.Update(time);
            if (lifecounter > lifetime)
                alive = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
