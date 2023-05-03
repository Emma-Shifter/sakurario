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
        protected Vector2 _position;
        protected Texture2D _texture;

        #endregion

        #region Properties
        public Input Input;
        public Point Size;
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
        public Vector2 Velocity;
        public bool isJump = false;
        public bool isFall = true;
        float _TotalSeconds = 0;
        float seconds = 0.8f;

        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null) spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null) _animationManager.Draw(spriteBatch);
            else throw new Exception("This ain't right..!");
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
            if (Velocity.X > 0) _animationManager.Play(_animations["WalkRight"]);
            else if (Velocity.X < 0) _animationManager.Play(_animations["WalkLeft"]);
            else if (Keyboard.GetState().IsKeyDown(Input.Up) && Keyboard.GetState().IsKeyDown(Input.ArrowRight))
                _animationManager.Play(_animations["JumpRight"]);
            else if (Keyboard.GetState().IsKeyDown(Input.Up) && Keyboard.GetState().IsKeyDown(Input.ArrowLeft))
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

        public virtual void Update(GameTime gameTime, Sprite player)
        {
            Move(gameTime);
            SetAnimations();
            _animationManager.Update(gameTime);
            Position += Velocity;
            Velocity = Vector2.Zero;
            if (player.Position.Y < 800)
            {
                player.Velocity.Y += 7;
            }              
        }

        #endregion
    }
}
