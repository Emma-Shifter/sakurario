using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sakurario.Controls
{
    public class Health : Component
    {
        private readonly Texture2D _texture;
        public Vector2 Position;
        public int width = 216;
        public int height = 80;
        public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, width, height);
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
