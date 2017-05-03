using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards.ParticleEngine.Particles
{
    class Particle
    {
        protected Vector2 position, origin;
        protected Color color;
        protected float lifeTime, size;
        protected float startLife;

        public float ColorModifier
        {
            get;
            set;
        }

        public float myLifeSpan
        {
            get { return lifeTime; }
        }

        public float mySize
        {
            get { return size; }
            set { size = value; }
        }

        public Particle(Vector2 position, float lifeTime, float size)
        {
            this.position = position;
            this.lifeTime = lifeTime;
            this.startLife = lifeTime;
            this.size = size;

            origin = new Vector2();
            color = new Color(Color.Red, lifeTime);
        }

        public virtual void Update(float time)
        {
            lifeTime -= time;
        }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
