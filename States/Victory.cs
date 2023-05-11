using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sakurario.Controls;
using System.Collections.Generic;

namespace sakurario.States
{
    public class Victory : State
    {
        public Victory(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content, 0)
        {
            background = _content.Load<Texture2D>("victory");
            var quitButtonTexture = _content.Load<Texture2D>("Controls/quit_button");
            var quitButton = new Button(quitButtonTexture) { Position = new Vector2(730, 600), };
            quitButton.Click += QuitButton_Click;
            _components = new List<Component>() { quitButton, };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1050), Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
