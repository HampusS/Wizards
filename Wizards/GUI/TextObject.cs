using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards.GUI
{
    class TextObject
    {
        protected float m_fSize;
        protected string m_sText;
        protected Color m_cColor;
        protected Vector2 m_vPosition;
        protected SpriteFont m_sfFont;

        protected Vector2 origin;

        public string myAppendedText
        {
            get;
            set;
        }

        public float mySize
        {
            get { return m_fSize; }
            set { m_fSize = value; }
        }

        public Vector2 myPosition
        {
            get { return m_vPosition; }
            set { m_vPosition = value; }
        }

        public TextObject(SpriteFont font, Vector2 position, Color color, string text, float size)
        {
            this.m_sfFont = font;
            this.m_vPosition = position;
            this.m_cColor = color;
            this.m_sText = text;
            this.m_fSize = size;
            origin = Vector2.Zero;
        }

        public void RefreshOrigin()
        {
            origin = new Vector2(m_sfFont.MeasureString(m_sText + myAppendedText).X / 2, m_sfFont.MeasureString(m_sText + myAppendedText).Y / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(m_sfFont, m_sText + myAppendedText, m_vPosition, m_cColor, 0, origin, m_fSize, SpriteEffects.None, 1);
        }
    }
}
