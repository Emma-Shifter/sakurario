using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace sakurario.Controls
{
    public class Health : Component
    {
        private Texture2D _texture;
        public Vector2 Position;
        public int width = 216;
        public int height = 80;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }
        public Health(Texture2D texture)
        {
            _texture = texture;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = new Color(56, 212, 53);
            spriteBatch.Draw(_texture, Rectangle, color);
        }

        public override void Update(GameTime gameTime)
        {
            width--;
        }
    }
}
