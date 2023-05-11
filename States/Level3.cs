using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sakurario.Sprites;

namespace sakurario.States
{
    public class Level3 : Level
    {
        public Level3(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content, 3)
        {
            platformTexture = _content.Load<Texture2D>("platform_pink");

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

            _smallSnakes.Add(new Sprite(SMAnimations) { isSnake = true, Position = new Vector2(500, 835), Size = new Point(90, 177), });
            _bigSnakes.Add(new Sprite(SBAnimations) { isSnake = true, Position = new Vector2(800, 810), Size = new Point(90, 177), });
        }
    }
}
