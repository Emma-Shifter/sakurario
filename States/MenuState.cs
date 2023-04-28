using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sakurario.Controls;

namespace sakurario.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var startButtonTexture = _content.Load<Texture2D>("Controls/start_button");
            var quitButtonTexture = _content.Load<Texture2D>("Controls/quit_button");

            var startButton = new Button(startButtonTexture)
            {
                Position = new Vector2(390, 420),
            };
            startButton.Click += StartButton_Click;

            var quitButton = new Button(quitButtonTexture)
            {
                Position = new Vector2(410, 590),
            };
            quitButton.Click += QuitButton_Click;

            _components = new List<Component>()
            {
                startButton,
                quitButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
