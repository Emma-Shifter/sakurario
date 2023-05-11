using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sakurario.Managers;
using sakurario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Point = Microsoft.Xna.Framework.Point;

namespace sakurario.Sprites
{
    public class Sprite
    {
        #region Fields

        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Dictionary<string, Animation> _mushroomAnimations;
        protected Vector2 _position;
        protected Texture2D _texture;

        #endregion

        #region Properties
        public Input Input;
        public Point Size;
        public bool isPlayer = false;
        public bool isSnake = false;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }
        public float Speed = 2f;
        public float SnakeSpeed = 0.8f;
        public Vector2 Velocity;
        public bool isJump = false;
        public bool isFall = true;
        float _TotalSeconds = 0;
        readonly float seconds = 0.8f;
        bool ToRight = false;
        readonly int width;
        readonly int height;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }
        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null) spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null) _animationManager.Draw(spriteBatch);
            else throw new Exception("This ain't right..!");
        }

        public virtual void Crawl(GameTime gameTime, Sprite obj, Sprite startPlatform, Sprite endPlatform)
        {
            if (ToRight == true && obj.Position.X < endPlatform.Position.X)
            {
                ToRight = true;
                Velocity.X = SnakeSpeed;
            }
            else
            {
                ToRight = false;
                Velocity.X = -SnakeSpeed;
                if (obj.Position.X < startPlatform.Position.X) ToRight = true;
            }
        }

        public virtual void Move(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                Speed = 2f;
                Velocity.X = -Speed;
                isJump = true;
            }

            if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Speed = 2f;
                Velocity.X = Speed;
                isJump = true;
            }

            if ((Keyboard.GetState().IsKeyDown(Input.Up) && Keyboard.GetState().IsKeyDown(Input.ArrowRight)))
            {
                Speed = 6f;
                if (isJump && seconds > _TotalSeconds)
                {
                    _TotalSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Velocity.Y = (float)(-Speed * (0.8));
                    Velocity.X = (float)(Speed / 2);
                }
                else
                {
                    isJump = false;
                    _TotalSeconds = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Input.Up) && Keyboard.GetState().IsKeyDown(Input.ArrowLeft))
            {
                Speed = 6f;
                if (isJump && seconds > _TotalSeconds)
                {
                    _TotalSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Velocity.Y = (float)(-Speed * (0.8));
                    Velocity.X = -Speed / 2;
                }
                else
                {
                    isJump = false;
                    _TotalSeconds = 0;
                }
            }
        }

        protected virtual void SetAnimations()
        {
            if (_animations.ContainsKey("JumpMushroom")) _animationManager.Play(_animations["JumpMushroom"]);
            else if (_animations.ContainsKey("SMLeft") && (ToRight == false)) _animationManager.Play(_animations["SMLeft"]);
            else if (_animations.ContainsKey("SMRight") && (ToRight == true)) _animationManager.Play(_animations["SMRight"]);
            else if (_animations.ContainsKey("SBLeft") && (ToRight == false)) _animationManager.Play(_animations["SBLeft"]);
            else if (_animations.ContainsKey("SBRight") && (ToRight == true)) _animationManager.Play(_animations["SBRight"]);
            else if (_animations.ContainsKey("WalkRight") && Velocity.X > 0) _animationManager.Play(_animations["WalkRight"]);
            else if (_animations.ContainsKey("WalkLeft") && (Velocity.X < 0)) _animationManager.Play(_animations["WalkLeft"]);
            else if (_animations.ContainsKey("JumpRight") && Keyboard.GetState().IsKeyDown(Input.Up) && Keyboard.GetState().IsKeyDown(Input.ArrowRight))
                _animationManager.Play(_animations["JumpRight"]);
            else if (_animations.ContainsKey("JumpLeft") && Keyboard.GetState().IsKeyDown(Input.Up) && Keyboard.GetState().IsKeyDown(Input.ArrowLeft))
                _animationManager.Play(_animations["JumpLeft"]);
            else _animationManager.Stop();
        }
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }
        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }
        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }
        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }
        public Sprite(Dictionary<string, Animation> animations)
        {
            width = 90;
            height = 177;
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            width = texture.Width;
            height = texture.Height;
        }

        public virtual void Update(GameTime gameTime, Sprite obj, Sprite item)
        {
            SetAnimations();
            _animationManager.Update(gameTime);
        }

        public virtual void Update(GameTime gameTime, Sprite obj, Sprite startPlatform, Sprite endPlatform)
        {
            if (isSnake) Crawl(gameTime, obj, startPlatform, endPlatform);
            SetAnimations();
            _animationManager.Update(gameTime);
            obj.Position += Velocity;
            obj.Velocity = Vector2.Zero;
        }

        public virtual void Update(GameTime gameTime, Sprite player, List<Sprite> sprites)
        {
            Move(gameTime);
            foreach (var sprite in sprites)
            {
                if (sprite == this) continue;
                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) || (this.Velocity.X < 0 & this.IsTouchingRight(sprite))) this.Velocity.X = 0;
                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) this.Velocity.Y = 0;
                if (this.Velocity.Y < 0 & this.IsTouchingBottom(sprite)) this.Velocity.Y = 7;

            }
            SetAnimations();
            _animationManager.Update(gameTime);
            Position += Velocity;
            Velocity = Vector2.Zero;
            if (player.Position.Y < 1050) player.Velocity.Y += 7;
        }
        #endregion
    }
}
