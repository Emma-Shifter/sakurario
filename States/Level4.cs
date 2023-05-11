using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprite = sakurario.Sprites.Sprite;

namespace sakurario.States
{
    public class Level4 : Level
    {
        public Level4(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content, 4)
        {
            platformTexture = _content.Load<Texture2D>("platform_pink");
            player.Position = new Vector2(1200, 600);
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(70, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(320, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(600, 600), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(850, 600), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(1100, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(1100, 300), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(1350, 900), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(110, 300), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(360, 300), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(610, 300), Size = new Point(240, 72), });
            _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(600, 900), Size = new Point(240, 72), });

            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(300, 835), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(820, 535), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(130, 235), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(630, 235), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(1200, 835), Size = new Point(70, 70), });

            _smallSnakes.Add(new Sprite(SMAnimations) { isSnake = true, Position = new Vector2(150, 835), Size = new Point(90, 177), });
            _smallSnakes.Add(new Sprite(SMAnimations) { isSnake = true, Position = new Vector2(590, 235), Size = new Point(90, 177), });
        }
    }
}
