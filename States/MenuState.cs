using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Button = sakurario.Controls.Button;

namespace sakurario.States
{
    public class MenuState : State
    {
        readonly Texture2D mainCharacter;
        readonly int frameWidth = 540;
        readonly int frameHeight = 540;
        Point currentFrame = new(0, 0);
        Point spriteSize = new(6, 1);
        int currentTime = 0;
        readonly int period = 200;
        Vector2 position = new(1200, 200);

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content, 0)
        {
            var startButtonTexture = _content.Load<Texture2D>("Controls/start_button");
            var quitButtonTexture = _content.Load<Texture2D>("Controls/quit_button");
            var rulesButtonTexture = _content.Load<Texture2D>("Controls/rules_button");
            background = _content.Load<Texture2D>("background");
            mainCharacter = _content.Load<Texture2D>("mainCharacter");

            var startButton = new Button(startButtonTexture) { Position = new Vector2(390, 420), };
            startButton.Click += StartButton_Click;

            var quitButton = new Button(quitButtonTexture) { Position = new Vector2(410, 590), };
            quitButton.Click += QuitButton_Click;

            var rulesButton = new Button(rulesButtonTexture) { Position = new Vector2(390, 760), };
            rulesButton.Click += RulesButton_Click;

            _components = new List<Component>() { startButton, quitButton, rulesButton, };
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

        public override void Update(GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime.Milliseconds;
            if (currentTime > period)
            {
                currentTime -= period;
                ++currentFrame.X;
                if (currentFrame.X >= spriteSize.X) currentFrame.X = 0;
            }
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
