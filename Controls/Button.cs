using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace sakurario.Controls
{
    public class Button : Component
    {
        #region Fields
        private MouseState _currentMouse;
        private bool _isHovering;
        private MouseState _previousMouse;
        private readonly Texture2D _texture;
        #endregion

        #region Properties
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        public string Text { get; set; }
        #endregion

        #region Methods
        public Button(Texture2D texture)
        {
            _texture = texture;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = new Color(18, 218, 225);
            if (_isHovering) color = new Color(254, 3, 249);
            spriteBatch.Draw(_texture, Rectangle, color);
        }
        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
        #endregion
    }
}
