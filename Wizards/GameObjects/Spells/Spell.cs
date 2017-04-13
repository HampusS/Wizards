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
        float speed = 200;
        bool alive = true;

        float lifetime = 4, lifecounter;

        public void KillMe()
        {
            lifecounter = 200;
        }

        public bool isAlive()
        {
            return alive;
        }

        public Spell(Texture2D texture, Vector2 position, int radius, Vector2 direction)
            : base(texture, position, radius)
        {
            this.texture = texture;
            this.position = position;
            this.radius = radius;
            this.m_vVelocity = direction * speed;
            color = Color.BlanchedAlmond;
            m_fFriction = 0;
            m_fMass = 1.5f;
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

        public void GiveImpulse(MovingObject targetObj)
        {
            Vector2 normal = targetObj.myPosition - position;
            normal.Normalize();
            targetObj.myVelocity += -Vector2.Reflect(m_vVelocity * m_fMass, normal);
            alive = false;
        }
    }
}
