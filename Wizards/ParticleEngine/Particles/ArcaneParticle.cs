using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.Utilities;

namespace Wizards.ParticleEngine.Particles
{
    class ArcaneParticle : Particle
    {
        protected Texture2D texture;
        protected float rotate;

        public ArcaneParticle(Texture2D texture, Vector2 position, float lifeTime, float size)
            : base(position, lifeTime, size)
        {
            this.texture = texture;
            this.position = position;
            this.lifeTime = lifeTime;
            this.size = size;

            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            color = new Color(Color.MediumPurple, lifeTime);
            ColorModifier = 1;
        }

        public override void Update(float time)
        {
            rotate += time * time;
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color * ColorModifier, rotate, origin, size * lifeTime, SpriteEffects.None, 0);
        }
    }
}
