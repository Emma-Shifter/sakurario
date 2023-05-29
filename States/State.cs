using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using sakurario.Sprites;
using System;
using System.Collections.Generic;

namespace sakurario.States
{
    public abstract class State
    {
        #region Fields
        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected Game1 _game;
        protected int Level;
        protected List<Component> _components;
        protected Texture2D background;
        SoundEffect SoundForButtons;
        #endregion

        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, int level)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            Level = level;
            SoundForButtons = _content.Load<SoundEffect>("Sounds/for_buttons");
        }
        public abstract void Update(GameTime gameTime);
        protected static bool Collide(Sprite firstObj, Sprite secondObj)
        {
            Rectangle firstObjRect = new((int)firstObj.Position.X,
                (int)firstObj.Position.Y, firstObj.Size.X, firstObj.Size.Y);
            Rectangle secondObjRect = new((int)secondObj.Position.X,
                (int)secondObj.Position.Y, secondObj.Size.X, secondObj.Size.Y);
            return firstObjRect.Intersects(secondObjRect);
        }
        protected void RulesButton_Click(object sender, EventArgs e)
        {
            SoundForButtons.Play();
            _game.ChangeState(new Gamerules(_game, _graphicsDevice, _content));
        }
        public virtual void StartButton_Click(object sender, EventArgs e)
        {
            SoundForButtons.Play();
            _game.ChangeState(new Level1(_game, _graphicsDevice, _content));
        } 
        public virtual void QuitButton_Click(object sender, EventArgs e)
        {
            SoundForButtons.Play();
            _game.Exit();
        }
        
        #endregion
    }
}
