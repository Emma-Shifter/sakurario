using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sakurario.Controls;
using sakurario.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace sakurario.States
{
    public class Level : State
    {
        public Level(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, int level) 
            : base(game, graphicsDevice, content, level)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, List<Sprite> _platforms, 
            List<Sprite> _mushrooms, List<Sprite> _smallSnakes, List<Sprite> _bigSnakes,
            Sprite player, Texture2D background, Texture2D health_form, Health health)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1050), Color.White);
            foreach (var list in new List<Sprite>[] { _platforms, _mushrooms, _smallSnakes, _bigSnakes })
            {
                foreach (var item in list) item.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            health.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(health_form, new Rectangle(106, 50, 228, 80), Color.White);
            spriteBatch.End();
        }

        //public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
        //}

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
