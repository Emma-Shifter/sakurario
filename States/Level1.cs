using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sakurario.Models;
using System.Collections.Generic;
using sakurario.Sprites;
using Microsoft.Xna.Framework.Input;

namespace sakurario.States
{
    public class Level1 : State
    {
        Texture2D background;
        Texture2D platformTexture;
        private Sprite player;
        private List<Sprite> _platforms = new List<Sprite>();
        private List<Sprite> _mushrooms = new List<Sprite>();
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

            var mushroomAnimations = new Dictionary<string, Animation>(){
                {"JumpMushroom", new Animation(_content.Load<Texture2D>("mushroomjump"), 7)},
            };

            platformTexture = _content.Load<Texture2D>("platform_blue");
                        
            for (var i = 0; i < 8; i++)
            {
                _platforms.Add(new Sprite(platformTexture)
                {
                    Position = new Vector2(i * 240, 900),
                    Size = new Point(240, 72),
                });
            }

            for (var i = 0; i < 8; i++)
            {
                _mushrooms.Add(new Sprite(mushroomAnimations)
                {
                    Position = new Vector2(50 + i * 240, 700),
                    Size = new Point(70, 70),
                });
            }

            player = new Sprite(animations)
            {
                isPlayer = true,
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
            foreach (var item in _platforms)
            {
                item.Draw(spriteBatch);
            }
            foreach (var item in _mushrooms)
            {
                item.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);          
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime, player);
            foreach(var item in _mushrooms)
            {
                item.Update(gameTime, player, item);
                if (Collide(item, player))
                {
                    _game.ChangeState(new Level2(_game, _graphicsDevice, _content));
                }
            }
            
            foreach (var item in _platforms)
            {
                if (Collide(item, player)) player.Velocity.Y -= 7;
            }
            
        }

        protected bool Collide(Sprite firstObj, Sprite secondObj)
        {
            Rectangle firstObjRect = new Rectangle((int)firstObj.Position.X,
                (int)firstObj.Position.Y, firstObj.Size.X, firstObj.Size.Y);
            Rectangle secondObjRect = new Rectangle((int)secondObj.Position.X,
                (int)secondObj.Position.Y, secondObj.Size.X, secondObj.Size.Y);

            return firstObjRect.Intersects(secondObjRect);
        }
    }
}
