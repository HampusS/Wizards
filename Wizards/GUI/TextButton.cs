using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.Utilities;

namespace Wizards.GUI
{
    class TextButton : TextObject
    {
        Color startColor;
        bool grow;
        float lerpSize;
        float startSize;

        public bool isSelected
        {
            get;
            set;
        }

        public Color myColor
        {
            get { return m_cColor; }
            set { m_cColor = value; }
        }

        public TextButton(SpriteFont font, Vector2 position, Color color, string text, float size) 
            : base(font, position, color, text, size)
        {
            this.m_sfFont = font;
            this.m_vPosition = position;
            this.m_cColor = color;
            this.startColor = color;
            this.m_sText = text;
            this.m_fSize = size;
            startSize = size;
            origin = new Vector2(font.MeasureString(m_sText).X / 2, font.MeasureString(m_sText).Y / 2);
        }

        public void Update(float time)
        {
            if (lerpSize <= 0.5f && !grow)
                grow = true;
            else if (lerpSize >= 2.5f && grow)
                grow = false;

            if (grow)
                lerpSize += time * 5;
            else
                lerpSize -= time * 6;
            if (isSelected)
            {
                m_cColor = Color.LimeGreen;
                m_fSize = Calculate.LerpFloat(startSize, startSize + (startSize * 0.5f), lerpSize);
            }
            else
            {
                m_cColor = startColor;
                m_fSize = startSize;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
