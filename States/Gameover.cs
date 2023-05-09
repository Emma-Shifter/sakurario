using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sakurario.Controls;
using System;
using System.Collections.Generic;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace sakurario.States
{
    public class Gameover : State
    {
        private List<Component> _components;
        Texture2D background;
        private int _check;
        public Gameover(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, int level) 
            : base(game, graphicsDevice, content, -1)
        {
            _check = level;
            background = _content.Load<Texture2D>("gameover");
            var startButtonTexture = _content.Load<Texture2D>("Controls/start_button");
            var startButton = new Button(startButtonTexture) { Position = new Vector2(730, 600), };
            startButton.Click += StartButton_Click;
            _components = new List<Component>() { startButton, };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1050), Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (_check == 1) _game.ChangeState(new Level1(_game, _graphicsDevice, _content));
            else if (_check == 2) _game.ChangeState(new Level2(_game, _graphicsDevice, _content));
            else if (_check == 3) _game.ChangeState(new Level3(_game, _graphicsDevice, _content));
            else if (_check == 4) _game.ChangeState(new Level4(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
