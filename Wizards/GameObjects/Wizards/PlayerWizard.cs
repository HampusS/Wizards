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
        Keys shoot, moveUp, moveLeft, moveDown, moveRight;

        float speedLimit = Settings.WizardStartSpeedLimit;
        float speed = Settings.WizardStartSpeed;

        public PlayerWizard(Texture2D texture, Vector2 position, int radius)
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            this.color = Color.Purple;
        }

        public void SetKeyBindings(Keys shoot, Keys moveUp, Keys moveLeft, Keys moveDown, Keys moveRight, Color color)
        {
            this.shoot = shoot;
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

        public bool flagInputShot()
        {
            if (KeyMouseReader.keyState.IsKeyDown(shoot))
                return true;
            return false;
        }

        private void InputMove(float time)
        {
            bool thrusting = false;


            if (KeyMouseReader.keyState.IsKeyDown(moveUp))
                thrusting = true;
            else
                m_vAcceleration *= 0;


            switch (moveState)
            {
                case MoveState.Acceleration:
                    if (KeyMouseReader.keyState.IsKeyDown(moveLeft))
                        m_fAngle -= 0.03f;
                    else if (KeyMouseReader.keyState.IsKeyDown(moveRight))
                        m_fAngle += 0.03f;
                    m_vAcceleration.X = (float)Math.Cos(m_fAngle);
                    m_vAcceleration.Y = (float)Math.Sin(m_fAngle);

                    if (thrusting)
                        m_vAcceleration *= speed;
                    break;
                case MoveState.Impulse:
                    if (KeyMouseReader.keyState.IsKeyDown(moveLeft))
                        m_fAngle -= 0.05f;
                    else if (KeyMouseReader.keyState.IsKeyDown(moveRight))
                        m_fAngle += 0.05f;

                    if (thrusting && m_vVelocity.Length() < 120)
                        GiveImpulse(60);
                    break;
            }
        }

        void GiveImpulse(float strength)
        {
            Vector2 direction = new Vector2((float)Math.Cos(getAngle()), (float)Math.Sin(getAngle()));
            myVelocity += direction * strength;
        }
    }
}
