using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards.GameObjects.Environment
{
    class PowerUp : GameObject
    {
        public enum PowerType
        {
            FireBall,
            FrostBall,
            ArcaneOrb,
        }
        public PowerType m_eType;

        public PowerUp(Texture2D texture, Vector2 position, int radius) 
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
        }

        public override void Update(float time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
