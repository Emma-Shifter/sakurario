using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sakurario.Controls;
using sakurario.Models;
using System.Collections.Generic;
using Sprite = sakurario.Sprites.Sprite;

namespace sakurario.States
{
    public class Level2 : Level
    {
        public Level2(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content, 2)
        {
            platformTexture = _content.Load<Texture2D>("platform_pink");
            for (var i = 0; i < 7; i++)
                _platforms.Add(new Sprite(platformTexture) { Position = new Vector2(50 + i * 250, 900), Size = new Point(240, 72), });

            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(120, 835), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(540, 835), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(800, 835), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(1000, 835), Size = new Point(70, 70), });
            _mushrooms.Add(new Sprite(mushroomAnimations) { Position = new Vector2(1450, 835), Size = new Point(70, 70), });

            _smallSnakes.Add(new Sprite(SMAnimations) { isSnake = true, Position = new Vector2(0, 835), Size = new Point(90, 177), });
            _smallSnakes.Add(new Sprite(SMAnimations) { isSnake = true, Position = new Vector2(700, 835), Size = new Point(90, 177), });
            _bigSnakes.Add(new Sprite(SBAnimations) { isSnake = true, Size = new Point(90, 177), Position = new Vector2(1000, 810), });
        }
    }
}
