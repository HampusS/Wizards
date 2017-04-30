using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GUI;
using Wizards.Utilities;

namespace Wizards.GameObjects
{
    class Wizard : MovingObject
    {
        public enum MoveState
        {
            Acceleration,
            Impulse,
        }
        public MoveState moveState = MoveState.Acceleration;

        public enum Taunts
        {
            Default,
            HowBout,
            Solid,
            Speed,
            Spray,
        }
        public Taunts myTaunts = Taunts.Default;

        // ANIMATION 
        AnimationController animations;

        public Vector2 m_vLeftFootPos, m_vRightFootPos;
        protected bool isWalking = false;

        protected float speed = Settings.WizardStartSpeed;

        protected int m_iHP;
        protected float m_fStartMass,
                        m_fStartInterval,
                        m_fStartStrength,
                        m_fStartSpeed,
                        m_fBoostTimer;

        public bool isBoosted = false;
        protected TauntText defaultTaunt,
            currentTaunt,
            howBoutDat,
            SolidAsARock,
            IFeelTheNeed,
            SprayAndPray;


        public bool DisplayTaunt
        {
            get;
            set;
        }

        public int myHP
        {
            get { return m_iHP; }
            set { m_iHP = value; }
        }

        public bool isInVoid
        {
            get;
            set;
        }

        public bool Walking()
        {
            if (isWalking || m_vVelocity.Length() > 5)
                return true;
            return false;
        }

        public Wizard(Texture2D texture, Vector2 position, int radius)
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            this.color = Color.Goldenrod;
            animations = new AnimationController(this);

            m_iHP = 3;
            SetPhysics(1, 0.8f);
            m_fStartSpeed = speed;
            m_fStartInterval = m_fInterval;
            m_fStartMass = m_fMass;
            m_fStartStrength = m_fStrength;
            defaultTaunt = new TauntText(TextureManager.font, Vector2.Zero, Color.White, "DEFAULT", 2, this);
            currentTaunt = defaultTaunt;
            howBoutDat = new TauntText(TextureManager.font, Vector2.Zero, Color.Red, "How 'bout dat", 2, this);
            SolidAsARock = new TauntText(TextureManager.font, Vector2.Zero, Color.SkyBlue, "Solid As A Rock!", 2, this);
            IFeelTheNeed = new TauntText(TextureManager.font, Vector2.Zero, Color.Yellow, "I Feel the Need..", 2, this);
            SprayAndPray = new TauntText(TextureManager.font, Vector2.Zero, Color.Red, "Spray 'n Pray", 2, this);
            DisplayTaunt = false;
        }

        public override void Update(float time)
        {
            animations.Update(time);
            if (DisplayTaunt)
            {
                HandleTaunts();
                currentTaunt.ResetTimer();
                DisplayTaunt = false;
            }

            currentTaunt.Update(time);
            if (isBoosted)
                m_fBoostTimer += time;
            if (m_fBoostTimer > 8)
            {
                ResetPowers();
                isBoosted = false;
                m_fBoostTimer = 0;
            }

            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentTaunt.Draw(spriteBatch);
            if (Walking())
            {
                spriteBatch.Draw(m_texture, m_vLeftFootPos, null, Color.DarkKhaki, 0, m_vOrigin, 0.4f, SpriteEffects.None, 0);
                spriteBatch.Draw(m_texture, m_vRightFootPos, null, Color.DarkKhaki, 0, m_vOrigin, 0.4f, SpriteEffects.None, 0);
            }
            base.Draw(spriteBatch);
            spriteBatch.Draw(tri, m_vPosition, null, Color.Black, m_fAngle + (float)(Math.PI / 2), triOrigin, 0.9f, SpriteEffects.None, 0);
        }

        private void HandleTaunts()
        {
            switch(myTaunts)
            {
                case Taunts.Default:
                    currentTaunt = defaultTaunt;
                    break;
                case Taunts.HowBout:
                    currentTaunt = howBoutDat;
                    break;
                case Taunts.Solid:
                    currentTaunt = SolidAsARock;
                    break;
                case Taunts.Speed:
                    currentTaunt = IFeelTheNeed;
                    break;
                case Taunts.Spray:
                    currentTaunt = SprayAndPray;
                    break;
            }
        }

        public void AddStrength()
        {
            if (m_fStrength < m_fStrengthLimit)
                m_fStrength += 0.4f;
        }

        public void BoostMass()
        {
            m_fMass = 80;
            if(isBoosted)
                m_fBoostTimer = 0;

            isBoosted = true;
        }

        public void BoostSpeed()
        {
            speed = Settings.WizardStartSpeedLimit;
            if (isBoosted)
                m_fBoostTimer = 0;
            isBoosted = true;
        }

        public void BoostFireRate()
        {
            m_fIntervalLimit *= 0.2f;
            if (isBoosted)
                m_fBoostTimer = 0;
            isBoosted = true;
        }

        public void ResetPowers()
        {
            m_fMass = m_fStartMass;
            m_fIntervalLimit = m_fStartInterval;
            speed = m_fStartSpeed;
        }

        public void ResetStrength()
        {
            m_fStrength = m_fStartStrength;
        }

    }
}
