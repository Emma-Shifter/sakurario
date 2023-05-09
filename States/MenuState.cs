using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Button = sakurario.Controls.Button;

namespace sakurario.States
{
    public class MenuState : State
    {
        private List<Component> _components;
        Texture2D background;
        Texture2D mainCharacter;
        int frameWidth = 540;
        int frameHeight = 540;
        Point currentFrame = new Point(0, 0);
        Point spriteSize = new Point(6, 1);
        int currentTime = 0;
        int period = 200;
        Vector2 position = new Vector2(1200, 200);

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content, 0)
        {
            var startButtonTexture = _content.Load<Texture2D>("Controls/start_button");
            var quitButtonTexture = _content.Load<Texture2D>("Controls/quit_button");
            background = _content.Load<Texture2D>("background");
            mainCharacter = _content.Load<Texture2D>("mainCharacter");

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
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1050), Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(mainCharacter, position, new Rectangle(currentFrame.X * frameWidth, currentFrame.Y * frameHeight,
                    frameWidth, frameHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new Level1(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime.Milliseconds;
            if (currentTime > period)
            {
                currentTime -= period;
                ++currentFrame.X;
                if (currentFrame.X >= spriteSize.X)
                {
                    currentFrame.X = 0;
                }
            }

            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
