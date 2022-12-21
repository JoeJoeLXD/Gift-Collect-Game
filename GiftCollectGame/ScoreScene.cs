using System;
using GiftCollectGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GiftCollectGame
{
    public class ScoreScene : GameScene
    {
        private Texture2D textureScore;
        private SpriteBatch _spriteBatch;

        //Add score list
        private SpriteFont scoreFont;

        int top5;

        public ScoreScene(Game game) : base(game)
        {
            this.textureScore = g.Content.Load<Texture2D>("images/scoreSceneBG");   // picture
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(textureScore, Vector2.Zero, Color.White);

            //add font
            scoreFont = g.Content.Load<SpriteFont>("fonts/regularFont");

            _spriteBatch.DrawString(scoreFont, "Top 5 High Scores:", new Vector2(500, 10), Color.Red);
            //add top 5 high scores
            top5 = Math.Min(5, Shared.RankScores.Count);
            for (int i = 0; i < top5; i++)
            {
                string scorelist = $"No.{i + 1}:    Score: {Shared.RankScores[i]}";
                Vector2 playerPos = new Vector2(500, 40 + 30 * i);
                _spriteBatch.DrawString(scoreFont, scorelist, playerPos, Color.Yellow);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }

}

