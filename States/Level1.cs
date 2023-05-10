using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sakurario.Models;
using sakurario.Sprites;
using System.Collections.Generic;

namespace sakurario.States
{
    public class Level1 : State
    {
        readonly Texture2D background;
        readonly Texture2D platformTexture;
        readonly Texture2D health_form;
        readonly Texture2D health;
        private readonly Sprite player;
        private readonly List<Sprite> _platforms = new();
        private readonly List<Sprite> _mushrooms = new();
        public Level1(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content, 1)
        {
            background = _content.Load<Texture2D>("platform_bg");
            platformTexture = _content.Load<Texture2D>("platform_blue");
            health_form = _content.Load<Texture2D>("health_form");
            health = _content.Load<Texture2D>("health");
            var animations = new Dictionary<string, Animation>(){
                {"WalkRight", new Animation(_content.Load<Texture2D>("Player/playerstepright"), 4) },
                {"WalkLeft", new Animation(_content.Load<Texture2D>("Player/playerstepleft"), 4) },
                {"JumpRight", new Animation(_content.Load<Texture2D>("Player/playerjumpright"), 3) },
                {"JumpLeft", new Animation(_content.Load<Texture2D>("Player/playerjumpleft"), 3) },
            };
            var mushroomAnimations = new Dictionary<string, Animation>(){
                {"JumpMushroom", new Animation(_content.Load<Texture2D>("mushroomjump"), 7)},
            };

            for (var i = 0; i < 7; i++)
                _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(50 + i * 250, 900), Size = new Point(240, 72), });

            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(120, 820), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(540, 820), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(800, 820), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(1000, 820), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(1450, 820), Size = new Point(70, 70), });

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
            foreach (var list in new List<Sprite>[] { _platforms, _mushrooms })
            {
                foreach (var item in list) item.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            spriteBatch.Draw(health, new Rectangle(112, 50, 216, 80), Color.White);
            spriteBatch.Draw(health_form, new Rectangle(106, 50, 228, 80), Color.White);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (_mushrooms.Count == 0) _game.ChangeState(new Level2(_game, _graphicsDevice, _content));
            player.Update(gameTime, player, _platforms);
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
                if (Collide(item, player)) player.Velocity.Y -= 7;
            if (player.Position.Y > 1050) _game.ChangeState(new Gameover(_game, _graphicsDevice, _content, 1));
        }
    }
}
