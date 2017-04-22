using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.Utilities;

namespace Wizards.GameObjects
{
    class Wizard : MovingObject
    {
        // ANIMATION & SPELL SELECTION
        // add randomized powerup behaviors with a timer 

        public Wizard(Texture2D texture, Vector2 position, int radius)
            : base(texture, position, radius)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.m_iRadius = radius;
            this.color = Color.Goldenrod;
        }

        public override void Update(float time)
        {
            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void DrawOutOfBoundsArrow(SpriteBatch spriteBatch, GameWindow window)
        {
            if (myPosition.X < 0 || myPosition.X > window.ClientBounds.Width
                || myPosition.Y < 0 || myPosition.Y > window.ClientBounds.Height)
            {
                Vector2 arrowPos;

                if (myPosition.X < tri.Width)
                    arrowPos.X = tri.Width;
                else if (myPosition.X > window.ClientBounds.Width - tri.Width)
                    arrowPos.X = window.ClientBounds.Width - tri.Width;
                else
                    arrowPos.X = myPosition.X;

                if (myPosition.Y < tri.Height)
                    arrowPos.Y = tri.Height;
                else if (myPosition.Y > window.ClientBounds.Height - tri.Height)
                    arrowPos.Y = window.ClientBounds.Height - tri.Height;
                else
                    arrowPos.Y = myPosition.Y;

                spriteBatch.Draw(tri, arrowPos, null, color, m_fAngle + (float)(Math.PI / 2), m_vOrigin, 1, SpriteEffects.None, 0);
            }
        }
    }
}
