using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sakurario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sakurario.Sprites;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.Design.AxImporter;

namespace sakurario.States
{
    public class Level1 : State
    {
        Texture2D background;
        Texture2D platformTexture;
        private Sprite player;
        Point platformSize;
        private List<Sprite> _platforms = new List<Sprite>();
        public Level1(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            background = _content.Load<Texture2D>("platform_bg");
            var animations = new Dictionary<string, Animation>(){
                {"WalkRight", new Animation(_content.Load<Texture2D>("Player/playerstepright"), 4) },
                {"WalkLeft", new Animation(_content.Load<Texture2D>("Player/playerstepleft"), 4) },
                {"JumpRight", new Animation(_content.Load<Texture2D>("Player/playerjumpright"), 3) },
                {"JumpLeft", new Animation(_content.Load<Texture2D>("Player/playerjumpleft"), 3) },
            };

            platformTexture = _content.Load<Texture2D>("platform_blue");

            
            for (var i = 0; i < 8; i++)
            {
                _platforms.Add(new Sprite(platformTexture)
                {
                    Position = new Vector2(i * 240, 900),
                });
            }

            player = new Sprite(animations)
            {
                Size = new Point(90, 177),
                Position = new Vector2(100, 100),
                Input = new Input()
                {
                    Right = Keys.D,
                    Left = Keys.A,
                    Up = Keys.W,
                    ArrowLeft = Keys.Left,
                    ArrowRight = Keys.Right,
                }
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1050), Color.White);
            player.Draw(spriteBatch);
            foreach (var item in _platforms)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime, player);
            foreach (var item in _platforms)
            {
                if (Collide(item)) player.Velocity.Y -= 7;
            }
            
        }

        protected bool Collide(Sprite platform)
        {
            Rectangle groundRect = new Rectangle((int)platform.Position.X,
                (int)platform.Position.Y, 240, 72);
            Rectangle playerRect = new Rectangle((int)player.Position.X,
                (int)player.Position.Y, player.Size.X, player.Size.Y);

            return groundRect.Intersects(playerRect);
        }
    }
}
