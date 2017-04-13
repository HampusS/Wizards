using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.Utilities;

namespace Wizards.GameObjects
{
    class PlayerWizard : Wizard
    {
        Keys shoot, switchSpellUp, switchSpellDown, moveUp, moveLeft, moveDown, moveRight;

        float speedLimit = Settings.WizardStartSpeedLimit;
        float speed = Settings.WizardStartSpeed;

        public PlayerWizard(Texture2D texture, Vector2 position, int radius)
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            this.color = Color.Purple;
            this.m_fFriction = 0.01f;
        }

        public void InitializeKeyBindings(Keys shoot, Keys switchSpellUp, Keys switchSpellDown,
            Keys moveUp, Keys moveLeft, Keys moveDown, Keys moveRight, Color color)
        {
            this.shoot = shoot;
            this.switchSpellUp = switchSpellUp;
            this.switchSpellDown = switchSpellDown;
            this.moveUp = moveUp;
            this.moveLeft = moveLeft;
            this.moveDown = moveDown;
            this.moveRight = moveRight;
            this.color = color;
        }

        public override void Update(float time)
        {
            InputMove(time);
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        // ---IMPROVE---MAKE IT TURN TO AN ANGLE THRU INPUT AND ADD THRUST TO ANGLE
        private void InputMove(float time)
        {
            if (KeyMouseReader.keyState.IsKeyDown(moveUp))
            {
                if (m_vAcceleration.Y > -speedLimit)
                    m_vAcceleration.Y -= speed / m_fMass;
            }
            else if (KeyMouseReader.keyState.IsKeyDown(moveDown))
            {
                if (m_vAcceleration.Y < speedLimit)
                    m_vAcceleration.Y += speed / m_fMass;
            }
            else // No input given - slow down movement Y-axis
            {
                m_vAcceleration.Y = 0;
            }



            if (KeyMouseReader.keyState.IsKeyDown(moveLeft))
            {
                if (m_vAcceleration.X > -speedLimit)
                    m_vAcceleration.X -= speed / m_fMass;
            }
            else if (KeyMouseReader.keyState.IsKeyDown(moveRight))
            {
                if (m_vAcceleration.X < speedLimit)
                    m_vAcceleration.X += speed / m_fMass;
            }
            else // No input given - slow down movement X-axis
            {
                m_vAcceleration.X = 0;
            }

        }
    }
}
