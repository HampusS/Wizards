using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards.GameObjects
{
    class GameObject
    {
        protected Texture2D texture;
        protected Vector2 position, origin;
        protected int radius;
        protected Color color;

        public int myRadius()
        {
            return radius; 
        }

        public Vector2 myPosition
        {
            get { return position; }
            set { position = value; }
        }

        public Color myColor
        {
            get { return color; }
            set { color = value; }
        }

        public virtual bool CheckCircleCollision(GameObject targetObj)
        {
            float totalRadius = radius + targetObj.myRadius();
            Vector2 deltaPos = targetObj.myPosition - position;
            return totalRadius * totalRadius > (deltaPos.X * deltaPos.X) + (deltaPos.Y * deltaPos.Y);
        }

        public GameObject(Texture2D texture, Vector2 position, int radius)
        {
            this.texture = texture;
            this.position = position;
            this.radius = radius;
            color = Color.Blue;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public virtual void Update(float time)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0, origin, 1, SpriteEffects.None, 0);
        }
    }
}
