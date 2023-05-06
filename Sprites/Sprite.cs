using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sakurario.Managers;
using sakurario.Models;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public float SnakeSpeed = 3f;
        public float Speed = 2f;
        public Vector2 Velocity;
        public bool isJump = false;
        public bool isFall = true;
        float _TotalSeconds = 0;
        float seconds = 0.8f;
        bool ToRight = false;
        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null) spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null) _animationManager.Draw(spriteBatch);
            else throw new Exception("This ain't right..!");
        }

        public virtual void Crawl(GameTime gameTime, Sprite obj)
        {
            if (ToRight == true && obj.Position.X < 1920)
            {
                ToRight = true;
                SnakeSpeed = 3f;
                Velocity.X = SnakeSpeed;
            }
            else
            {
                ToRight = false;
                SnakeSpeed = 3f;
                Velocity.X = -SnakeSpeed;
                if (obj.Position.X < 0) ToRight = true;
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
                    Velocity.Y = (float)(-Speed*(0.8));
                    Velocity.X = (float)(Speed/2);
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
                    Velocity.X = (float)(-Speed/2);
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

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime, Sprite player, Sprite obj)
        {
            if (isSnake) Crawl(gameTime, obj);
            SetAnimations();
            _animationManager.Update(gameTime);
            obj.Position += Velocity;
            obj.Velocity = Vector2.Zero;       
        }

        public virtual void Update(GameTime gameTime, Sprite player)
        {
            Move(gameTime);
            SetAnimations();
            _animationManager.Update(gameTime);
            Position += Velocity;
            Velocity = Vector2.Zero;
            if (player.Position.Y < 800) player.Velocity.Y += 7;
        }
        #endregion
    }
}
