using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.ParticleEngine.Particles;

namespace Wizards.ParticleEngine.Emitters
{
    class Emitter
    {
        protected List<Particle> particles = new List<Particle>();
        protected Vector2 position;
        protected Random random;
        protected bool alive = true;
        protected int nrParticles = 1;

        protected float timer, timeLimit = 0.07f;

        public bool IsAlive
        {
            get { return alive; }
        }

        public Vector2 myPosition
        {
            get { return position; }
            set { position = value; }
        }

        public Emitter(Vector2 position)
        {
            this.position = position;
            random = new Random();
        }

        public virtual void Update(float time)
        {
            timer += time;
            RemoveParticles(time);

            if (timer > timeLimit)
            {
                timer = 0;
                EmitParticles();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }
        }


        protected virtual Particle GenerateParticle()
        {
            return new Particle(position, 50 + random.Next(50), 16);
        }

        protected virtual void EmitParticles()
        {
            for (int i = 0; i < nrParticles; i++)
            {
                particles.Add(GenerateParticle());
            }
        }

        protected void RemoveParticles(float time)
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                particles[i].Update(time);
                if (particles[i].myLifeSpan <= 0)
                    particles.RemoveAt(i);
            }
        }
    }
}
