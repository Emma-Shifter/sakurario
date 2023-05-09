using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sakurario.Controls;
using sakurario.Models;
using sakurario.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace sakurario.States
{
    public class Level4 : State
    {
        Texture2D background;
        Texture2D platformTexture;
        Texture2D healthTexture;
        Texture2D health_form;
        Health health;
        float health_counter = 40f;
        float health_index = 216;
        float _TotalSeconds = 0;
        bool isInjured = false;
        private Sprite player;
        private List<Sprite> _platforms = new List<Sprite>();
        private List<Sprite> _mushrooms = new List<Sprite>();
        private List<Sprite> _smallSnakes = new List<Sprite>();
        private List<Sprite> _bigSnakes = new List<Sprite>();
        public Level4(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) 
            : base(game, graphicsDevice, content, 4)
        {
            background = _content.Load<Texture2D>("platform_bg");
            platformTexture = _content.Load<Texture2D>("platform_pink");
            health_form = _content.Load<Texture2D>("health_form");
            healthTexture = _content.Load<Texture2D>("health");
            health = new Health(healthTexture)
            {
                Position = new Vector2(112, 50),
            };
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

            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(70, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(320, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(600, 700), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(850, 700), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(1100, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(1350, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(110, 500), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(360, 500), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(610, 500), Size = new Point(240, 72), });

            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(300, 835), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(820, 635), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(130, 435), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(630, 435), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(1200, 835), Size = new Point(70, 70), });

            _smallSnakes.Add(new Sprite(SMAnimations) { isSnake = true, Position = new Vector2(150, 435), Size = new Point(90, 177), });
            _bigSnakes.Add(new Sprite(SBAnimations) { isSnake = true, Position = new Vector2(590, 410), Size = new Point(90, 177), });
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
            health.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(health_form, new Rectangle(106, 50, 228, 80), Color.White);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (_mushrooms.Count == 0) _game.ChangeState(new Level4(_game, _graphicsDevice, _content));
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
                        isInjured = true;
                        _TotalSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                foreach (var snake in _bigSnakes)
                {
                    snake.Update(gameTime, snake, _platforms[0], _platforms[_platforms.Count - 1]);
                    if (Collide(snake, player))
                    {
                        _game.ChangeState(new Gameover(_game, _graphicsDevice, _content, 4));
                    }
                }
            }
            if (isInjured && health_counter > _TotalSeconds)
            {
                health_index--;
                health.Update(gameTime);
            }
            if (player.Position.Y > 1050 || health_index <= 0) _game.ChangeState(new Gameover(_game, _graphicsDevice, _content, 4));
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
