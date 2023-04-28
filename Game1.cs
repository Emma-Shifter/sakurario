using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using System.Collections.Generic;
using sakurario.Controls;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace sakurario
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        private List<Component> _gameComponents;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1050;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("background");

            var quitButton = new Button(Content.Load<Texture2D>("Controls/quit_button"))
            {
                Position = new Vector2(410, 590),
            };
            quitButton.Click += QuitButton_Click;

            var startButton = new Button(Content.Load<Texture2D>("Controls/start_button"))
            {
                Position = new Vector2(390, 420),
            };
            startButton.Click += StartButton_Click;

            _gameComponents = new List<Component>()
            {
                startButton,
                quitButton,
            };
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }
        private void StartButton_Click(object sender, System.EventArgs e)
        {
            var index = 0;
        }
        protected override void Update(GameTime gameTime)
        {
            foreach (var component in _gameComponents)
                component.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1050), Color.White);
            foreach (var component in _gameComponents)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}