using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sakurario.Sprites;

namespace sakurario.States
{
    public abstract class State
    {
        #region Fields

        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected Game1 _game;
        protected int _level;

        #endregion

        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, int level)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            _level = level;
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
        #endregion
    }
}
