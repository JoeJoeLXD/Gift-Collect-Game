using GiftCollectGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;

namespace GiftCollectGame
{
    public class AboutScene : GameScene
    {
        private Texture2D textureAbout;
        private SpriteFont myFont;
        private string credits = "Credits:\nXiangdong Li\nYingqi Xu";
        public AboutScene(Game game) : base(game)
        {
            this.textureAbout = g.Content.Load<Texture2D>("images/background");
            myFont = g.Content.Load<SpriteFont>("fonts/regularFont");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(textureAbout, Vector2.Zero, Color.White);
            spriteBatch.DrawString(myFont, credits, new Vector2(300, 200), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
