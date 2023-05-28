using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xunit;
using sakurario.States;

namespace sakurario.Tests
{
    public class LevelTests
    {
        private Game1 game;

        public LevelTests()
        {
            game = new Game1();
            game.graphics.ApplyChanges();
        }

        private void Level_test(Level level)
        {
            Assert.NotNull(level);
            Assert.NotNull(level.player);
            Assert.NotNull(level.health);
            Assert.NotEmpty(level._platforms);
            Assert.NotEmpty(level._mushrooms);
        }

        private void small_snakes_test(Level level) => Assert.NotEmpty(level._smallSnakes);

        private void big_snakes_test(Level level) => Assert.NotEmpty(level._bigSnakes);

        [Fact]
        private void level1_test()
        {
            var level1 = new Level1(game, game.GraphicsDevice, game.Content);
            Level_test(level1);
        }

        [Fact]
        private void level2_test()
        {
            var level2 = new Level2(game, game.GraphicsDevice, game.Content);
            Level_test(level2);
            small_snakes_test(level2);
            big_snakes_test(level2);
        }

        [Fact]
        private void level3_test()
        {
            var level3 = new Level3(game, game.GraphicsDevice, game.Content);
            Level_test(level3);
            small_snakes_test(level3);
            big_snakes_test(level3);
        }

        [Fact]
        private void level4_test()
        {
            var level4 = new Level4(game, game.GraphicsDevice, game.Content);
            Level_test(level4);
            small_snakes_test(level4);
        }
    }
}
