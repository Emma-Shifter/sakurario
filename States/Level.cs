using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sakurario.Controls;
using sakurario.Models;
using sakurario.Sprites;
using System.Collections.Generic;

namespace sakurario.States
{
    public class Level : State
    {
        readonly int _level;
        protected Texture2D platformTexture;
        protected Texture2D health_form;
        protected Texture2D healthTexture;
        protected Health health;
        protected Sprite player;
        protected float health_counter = 40f;
        protected float health_index = 900;
        protected float _TotalSeconds = 0;
        protected bool isInjured = false;
        protected Dictionary<string, Animation> mushroomAnimations;
        protected Dictionary<string, Animation> SMAnimations;
        protected Dictionary<string, Animation> SBAnimations;
        protected readonly List<Sprite> _platforms = new();
        protected readonly List<Sprite> _mushrooms = new();
        protected readonly List<Sprite> _smallSnakes = new();
        protected readonly List<Sprite> _bigSnakes = new();

        public Level(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, int level)
            : base(game, graphicsDevice, content, level)
        {
            _level = level;
            SMAnimations = new Dictionary<string, Animation>(){
                {"SMRight", new Animation(_content.Load<Texture2D>("Snakes/smallsnakestepright"), 4) },
                {"SMLeft", new Animation(_content.Load<Texture2D>("Snakes/smallsnakestepleft"), 4) },
            };
            SBAnimations = new Dictionary<string, Animation>(){
                {"SBRight", new Animation(_content.Load<Texture2D>("Snakes/bigsnakestepright"), 4) },
                {"SBLeft", new Animation(_content.Load<Texture2D>("Snakes/bigsnakestepleft"), 4) },
            };
            var animations = new Dictionary<string, Animation>(){
                {"WalkRight", new Animation(_content.Load<Texture2D>("Player/playerstepright"), 4) },
                {"WalkLeft", new Animation(_content.Load<Texture2D>("Player/playerstepleft"), 4) },
                {"JumpRight", new Animation(_content.Load<Texture2D>("Player/playerjumpright"), 3) },
                {"JumpLeft", new Animation(_content.Load<Texture2D>("Player/playerjumpleft"), 3) },
            };
            mushroomAnimations = new Dictionary<string, Animation>(){
                {"JumpMushroom", new Animation(_content.Load<Texture2D>("mushroomjump"), 7)},
            };
            background = _content.Load<Texture2D>("platform_bg");
            health_form = _content.Load<Texture2D>("health_form");
            healthTexture = _content.Load<Texture2D>("health");
            health = new Health(healthTexture)
            {
                Position = new Vector2(112, 50),
            };
            player = new Sprite(animations)
            {
                isPlayer = true,
                Size = new Point(90, 177),
                Position = new Vector2(100, 600),
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
            foreach (var list in new List<Sprite>[] { _platforms, _mushrooms, _smallSnakes, _bigSnakes })
            {
                foreach (var item in list) item.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            health.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(health_form, new Rectangle(106, 50, 228, 80), Color.White);
            spriteBatch.End();
        }

        public void CheckVictory()
        {
            if (_level == 1) _game.ChangeState(new Level2(_game, _graphicsDevice, _content));
            else if (_level == 2) _game.ChangeState(new Level3(_game, _graphicsDevice, _content));
            else if (_level == 3) _game.ChangeState(new Level4(_game, _graphicsDevice, _content));
            else if (_level == 4) _game.ChangeState(new Victory(_game, _graphicsDevice, _content));
        }
        public override void Update(GameTime gameTime)
        {
            if (_mushrooms.Count == 0) CheckVictory();
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
                if (Collide(item, player))
                {
                    player.Velocity.Y -= 7;
                }
                foreach (var snake in _smallSnakes)
                {
                    snake.Update(gameTime, snake, _platforms[0], _platforms[^1]);
                    if (Collide(snake, player))
                    {
                        isInjured = true;
                        _TotalSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                foreach (var snake in _bigSnakes)
                {
                    snake.Update(gameTime, snake, _platforms[0], _platforms[^1]);
                    if (Collide(snake, player))
                    {
                        _game.ChangeState(new Gameover(_game, _graphicsDevice, _content, _level));
                    }
                }
            }
            if (isInjured && health_counter > _TotalSeconds)
            {
                health_index--;
                health.Update(gameTime);
            }
            if (player.Position.Y > 1050 || health_index <= 0) _game.ChangeState(new Gameover(_game, _graphicsDevice, _content, _level));
            player.Update(gameTime, player, _platforms);
        }
    }
}
