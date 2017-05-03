using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.ParticleEngine.Emitters;
using Wizards.Utilities;

namespace Wizards.GameObjects.Environment
{
    class PickUp : TimedObject
    {
        public enum myType
        {
            ArcaneBoost,
            MassBoost,
            SpeedBoost,
            RapidFireBoost,
        }
        public myType m_mtType = myType.ArcaneBoost;

        ArcaneEmitter emitter;

        public PickUp(Texture2D texture, Vector2 position, int radius, int lifeTime, myType type)
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            this.m_fLifeTime = lifeTime;
            this.m_fStartLife = lifeTime;
            this.m_mtType = type;
            rnd = new Random();
            InitializeOnType();

            scaleModifier = (defaultRadius * 2) / m_texture.Width;
            m_fScale *= scaleModifier;
            m_vOrigin = new Vector2(m_texture.Width / 2, m_texture.Height / 2);
            emitter = new ArcaneEmitter(TextureManager.smooth, m_vPosition, m_iRadius, this);
        }

        private void InitializeOnType()
        {
            switch (m_mtType)
            {
                case myType.ArcaneBoost:
                    color = Color.MediumPurple;
                    if (rnd.Next(0, 3) == 1)
                        CanShoot = true;
                    else
                        CanShoot = false;
                    break;
                case myType.MassBoost:
                    color = Color.DeepSkyBlue;
                    m_texture = TextureManager.solid;
                    break;
                case myType.RapidFireBoost:
                    color = Color.OrangeRed;
                    m_texture = TextureManager.dmg;
                    break;
                case myType.SpeedBoost:
                    color = Color.Yellow;
                    m_texture = TextureManager.flash;
                    break;
            }
        }

        public override void Update(float time)
        {
            switch (m_mtType)
            {
                case myType.ArcaneBoost:
                    emitter.Update(time);
                    break;
            }
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (m_mtType)
            {
                case myType.ArcaneBoost:
                    emitter.Draw(spriteBatch);
                    break;
                case myType.MassBoost:
                    spriteBatch.Draw(m_texture, m_vPosition, null, color * LifePercent(), m_fAngle, m_vOrigin, m_fScale, SpriteEffects.None, 0);
                    break;
                case myType.RapidFireBoost:
                    spriteBatch.Draw(m_texture, m_vPosition, null, color * LifePercent(), m_fAngle, m_vOrigin, m_fScale, SpriteEffects.None, 0);
                    break;
                case myType.SpeedBoost:
                    spriteBatch.Draw(m_texture, m_vPosition, null, color * LifePercent(), m_fAngle, m_vOrigin, m_fScale, SpriteEffects.None, 0);
                    break;
            }
        }
    }
}
