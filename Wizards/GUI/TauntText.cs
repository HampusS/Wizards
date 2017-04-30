using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GameObjects;

namespace Wizards.GUI
{
    class TauntText : TextObject
    {
        float timer;

        public bool DisplayMe;
        Wizard m_wAgent;

        public TauntText(SpriteFont font, Vector2 position, Color color, string text, float size, Wizard agent)
            : base(font, position, color, text, size)
        {
            this.m_sfFont = font;
            this.m_vPosition = position;
            this.m_cColor = color;
            this.m_sText = text;
            this.m_fSize = size;
            this.m_wAgent = agent;
            timer = 1;
            origin = new Vector2(font.MeasureString(m_sText).X / 2, font.MeasureString(m_sText).Y / 2);
        }

        public void Update(float time)
        {
            timer += time;
            if (timer < 1)
            {
                DisplayMe = true;
                m_vPosition = m_wAgent.myPosition;
                m_vPosition.Y += m_wAgent.getRadius() * 2;
            }
            else
                DisplayMe = false;
        }

        public void ResetTimer()
        {
            timer = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (DisplayMe)
                spriteBatch.DrawString(m_sfFont, m_sText + myAppendedText, m_vPosition, m_cColor, 0, origin, m_fSize, SpriteEffects.None, 1);
        }
    }
}
