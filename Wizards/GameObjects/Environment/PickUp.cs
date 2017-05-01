﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.Utilities;

namespace Wizards.GameObjects.Environment
{
    class PickUp : GameObject
    {
        public enum myType
        {
            ArcaneBoost,
            MassBoost,
            SpeedBoost,
            RapidFireBoost,
        }
        public myType m_mtType = myType.ArcaneBoost;

        Random rnd;

        protected float m_fLifeTime, m_fStartLife;

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

        private float LifePercent()
        {
            return (m_fLifeTime / m_fStartLife);
        }

        public override void Update(float time)
        {
            m_fLifeTime -= time;
            if (m_fLifeTime < 0)
                alive = false;
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_vPosition, null, color * LifePercent(), m_fAngle, m_vOrigin, m_fScale, SpriteEffects.None, 0);
        }
    }
}
