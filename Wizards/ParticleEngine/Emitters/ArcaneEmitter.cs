using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GameObjects;
using Wizards.GameObjects.Environment;
using Wizards.ParticleEngine.Particles;

namespace Wizards.ParticleEngine.Emitters
{
    class ArcaneEmitter : Emitter
    {
        Texture2D texture;
        Random rand;
        float size;
        TimedObject agent;

        public ArcaneEmitter(Texture2D texture, Vector2 position, float size, TimedObject agent)
            : base(position)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.agent = agent;
            rand = new Random();
            timer = 1;
            timeLimit = 0.03f;
        }

        public override void Update(float time)
        {
            timer += time;
            RemoveParticles(time);
            EmitParticles();
            position = agent.myPosition;
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].ColorModifier = agent.LifePercent();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override Particle GenerateParticle()
        {
            return new ArcaneParticle(texture, new Vector2(position.X + random.Next(-5, 5), position.Y + random.Next(-5, 5)), agent.myLifeTime * 0.05f, size);
        }

        protected override void EmitParticles()
        {
            if (timer > timeLimit)
            {
                particles.Add(GenerateParticle());
                timer = 0;
            }
        }
    }
}
