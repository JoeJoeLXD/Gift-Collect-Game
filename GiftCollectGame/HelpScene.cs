using GiftCollectGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;

namespace GiftCollectGame
{
    public class HelpScene : GameScene
    {
        private Texture2D textureH;
        private SpriteFont myFont;
        private string help = "Instructions:\nPress Left and Right Keys to move the bar left and right\n" +
            "Use the bar to bounce the santa to the sky\nCollect the gifts in the sky to get more score\n" +
            "If the santa fall out of screen then game over";

        public HelpScene(Game game) : base(game)
        {
            this.textureH = g.Content.Load<Texture2D>("images/background");   // picture
            myFont = g.Content.Load<SpriteFont>("fonts/regularFont");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(textureH, Vector2.Zero, Color.White);
            spriteBatch.DrawString(myFont, help, new Vector2(100, 200), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
