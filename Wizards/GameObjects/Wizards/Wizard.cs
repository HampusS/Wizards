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
        float interval = 2;
        Texture2D tri;
        Vector2 triOrigin;
        protected float myAngle;

        public Wizard(Texture2D texture, Vector2 position, int radius)
            : base(texture, position, radius)
        {
            this.texture = texture;
            this.position = position;
            this.radius = radius;
            this.color = Color.Goldenrod;
            tri = TextureManager.triangle;
            triOrigin = new Vector2(tri.Width / 2, tri.Height / 2);
        }

        public override void Update(float time)
        {
            interval += time;

            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            myAngle = (float)Math.Atan2(m_vVelocity.Y, m_vVelocity.X) + (float)(Math.PI / 2);

            spriteBatch.Draw(tri, position, null, Color.Black, myAngle, triOrigin, 1, SpriteEffects.None, 0);
            Vector2 temp = position + Vector2.Normalize(m_vVelocity) * radius;
            spriteBatch.Draw(texture, temp, null, Color.Red, 0, origin, 0.4f, SpriteEffects.None, 0);

        }

        public virtual bool isInSpellRange(Vector2 target)
        {
            if (Vector2.Distance(position, target) < Settings.RangeCheck)
            {
                Vector2 temp = target - position;
                myAngle = (float)Math.Atan2(temp.Y, temp.X) + (float)(Math.PI / 2);

                if (interval > 2)
                {
                    interval = 0;
                    return true;
                }
            }
            return false;
        }

        public void DrawOutOfBoundsArrow(SpriteBatch spriteBatch, GameWindow window)
        {
            if (myPosition.X > 0 && myPosition.X < window.ClientBounds.Width
                && myPosition.Y > 0 && myPosition.Y < window.ClientBounds.Height)
            {
                // DO NOTHING
            }
            else
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

                float angle = (float)Math.Atan2(arrowPos.Y - myPosition.Y, arrowPos.X - myPosition.X) - (float)(Math.PI / 2);

                spriteBatch.Draw(tri, arrowPos, null, color, angle, origin, 1, SpriteEffects.None, 0);
            }
        }
    }
}
