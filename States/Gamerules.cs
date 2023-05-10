using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sakurario.Controls;
using System;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace sakurario.States
{
    public class Gamerules : State
    {
        private List<Component> _components;
        Texture2D background;
        Texture2D mainCharacterStep;
        Texture2D mainCharacterJump;
        Point stepCurrentFrame = new(0, 0);
        Point stepspriteSize = new(4, 1);
        Point jumpCurrentFrame = new(0, 0);
        Point jumpspriteSize = new(4, 1);
        int stepcurrentTime = 0;
        int jumpcurrentTime = 0;
        int period = 200;
        Vector2 stepPosition = new(1400, 400);
        Vector2 jumpPosition = new(300, 450);

        public Gamerules(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) 
            : base(game, graphicsDevice, content, 0)
        {
            background = _content.Load<Texture2D>("gamerules");
            var quitButtonTexture = _content.Load<Texture2D>("Controls/quit_button");
            var quitButton = new Button(quitButtonTexture) { Position = new Vector2(730, 600), };
            quitButton.Click += QuitButton_Click;
            _components = new List<Component>() { quitButton, };
            mainCharacterStep = _content.Load<Texture2D>("Player/playerstepright");
            mainCharacterJump = _content.Load<Texture2D>("Player/jumpforrules");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1050), Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(mainCharacterStep, stepPosition, new Rectangle(stepCurrentFrame.X * 90, stepCurrentFrame.Y * 177,
                    90, 177), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            spriteBatch.Draw(mainCharacterJump, jumpPosition, new Rectangle(stepCurrentFrame.X * 90, stepCurrentFrame.Y * 177,
                    90, 177), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            stepcurrentTime += gameTime.ElapsedGameTime.Milliseconds;
            jumpcurrentTime += gameTime.ElapsedGameTime.Milliseconds;
            if (jumpcurrentTime > period)
            {
                jumpcurrentTime -= period;
                ++jumpCurrentFrame.X;
                if (jumpCurrentFrame.X >= jumpspriteSize.X)
                {
                    jumpCurrentFrame.X = 0;
                }

                if (jumpCurrentFrame.X == 1)
                {
                    jumpPosition.Y = 350;
                }
                else jumpPosition.Y = 450;
            }
            if (stepcurrentTime > period)
            {
                stepcurrentTime -= period;
                ++stepCurrentFrame.X;
                if (stepCurrentFrame.X >= stepspriteSize.X)
                {
                    stepCurrentFrame.X = 0;
                }
            }
            foreach (var component in _components)
                component.Update(gameTime);
        }
        private void QuitButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
    }
}
