using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sakurario.Models;
using System;
using System.Collections.Generic;
using sakurario.Sprites;
using Microsoft.Xna.Framework.Input;

namespace sakurario.States
{
    public class Level3 : State
    {
        Texture2D background;
        Texture2D platformTexture;
        private Sprite player;
        private List<Sprite> _platforms = new List<Sprite>();
        private List<Sprite> _mushrooms = new List<Sprite>();
        private List<Sprite> _smallSnakes = new List<Sprite>();
        private List<Sprite> _bigSnakes = new List<Sprite>();
        public Level3(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) 
            : base(game, graphicsDevice, content, 3)
        {
            background = _content.Load<Texture2D>("platform_bg");
            platformTexture = _content.Load<Texture2D>("platform_pink");

            var SMAnimations = new Dictionary<string, Animation>(){
                {"SMRight", new Animation(_content.Load<Texture2D>("Snakes/smallsnakestepright"), 4) },
                {"SMLeft", new Animation(_content.Load<Texture2D>("Snakes/smallsnakestepleft"), 4) },
            };
            var SBAnimations = new Dictionary<string, Animation>(){
                {"SBRight", new Animation(_content.Load<Texture2D>("Snakes/bigsnakestepright"), 4) },
                {"SBLeft", new Animation(_content.Load<Texture2D>("Snakes/bigsnakestepleft"), 4) },
            };
            var animations = new Dictionary<string, Animation>(){
                {"WalkRight", new Animation(_content.Load<Texture2D>("Player/playerstepright"), 4) },
                {"WalkLeft", new Animation(_content.Load<Texture2D>("Player/playerstepleft"), 4) },
                {"JumpRight", new Animation(_content.Load<Texture2D>("Player/playerjumpright"), 3) },
                {"JumpLeft", new Animation(_content.Load<Texture2D>("Player/playerjumpleft"), 3) },
            };
            var mushroomAnimations = new Dictionary<string, Animation>(){
                {"JumpMushroom", new Animation(_content.Load<Texture2D>("mushroomjump"), 7)},
            };
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

            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(50, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(300, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(550, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(1050, 700), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(1300, 700), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(1550, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(800, 900), Size = new Point(240, 72), });

            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(120, 835), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(420, 835), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(1200, 635), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(1600, 835), Size = new Point(70, 70), });

            _smallSnakes.Add(new Sprite(SMAnimations) { isSnake = true, Position = new Vector2(70, 835), Size = new Point(90, 177), });
            _bigSnakes.Add(new Sprite(SBAnimations) { isSnake = true, Position = new Vector2(400, 810), Size = new Point(90, 177), });
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 1920, 1050), Color.White);
            foreach (var item in _platforms)
                item.Draw(spriteBatch);
            foreach (var item in _mushrooms)
                item.Draw(spriteBatch);
            foreach (var item in _smallSnakes)
                item.Draw(spriteBatch);
            foreach (var item in _bigSnakes)
                item.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (_mushrooms.Count == 0) _game.ChangeState(new Level3(_game, _graphicsDevice, _content));
            foreach (var item in _mushrooms)
            {
                item.Update(gameTime, player, item);
                if (Collide(item, player))
                {
                    _mushrooms.Remove(item);
                    break;
                }
            }
            foreach (var item in _platforms)
            {
                if (Collide(item, player)) player.Velocity.Y -= 7;
                foreach (var snake in _smallSnakes)
                {
                    snake.Update(gameTime, snake, _platforms[0], _platforms[_platforms.Count - 1]);
                    if (Collide(snake, player))
                    {
                        _game.ChangeState(new Gameover(_game, _graphicsDevice, _content, 3));
                    }
                }
                foreach (var snake in _bigSnakes)
                {
                    snake.Update(gameTime, snake, _platforms[0], _platforms[_platforms.Count - 1]);
                    if (Collide(snake, player))
                    {
                        _game.ChangeState(new Gameover(_game, _graphicsDevice, _content, 3));
                    }
                }
            }
            if (player.Position.Y > 1050) _game.ChangeState(new Gameover(_game, _graphicsDevice, _content, 3));
            player.Update(gameTime, player);
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
