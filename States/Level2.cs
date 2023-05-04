using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sakurario.Models;
using System.Collections.Generic;
using sakurario.Sprites;
using Microsoft.Xna.Framework.Input;

namespace sakurario.States
{
    public class Level2 : State
    {
        Texture2D background;
        public Level2(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) 
            : base(game, graphicsDevice, content)
        {
            background = _content.Load<Texture2D>("platform_bg"); 

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1050), Color.White);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
