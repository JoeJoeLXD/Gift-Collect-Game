using GiftCollectGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GiftCollectGame
{
    public class StartScene : GameScene
    {
        //menu
        public MenuComponent Menu { get; set; }
        private SpriteBatch spriteBatch;
        public Game1 g;
        public Texture2D textureHome;
        string[] menus = {"Start/Continue Game",
             "Help",
             "Score",
             "About",
             "Exit"};

        private Texture2D Home;

        public StartScene(Game game) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            //load font .......from game
            SpriteFont regularFont = game.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont highlightFont = g.Content.Load<SpriteFont>("fonts/highlightFont");
            Menu = new MenuComponent(game, spriteBatch, regularFont, highlightFont, menus);

            this.Home = g.Content.Load<Texture2D>("images/background");  //change background picture
            this.Components.Add(Menu);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Home, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
