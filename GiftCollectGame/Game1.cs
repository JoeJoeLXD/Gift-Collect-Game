/*  
 * Programmed by: XiangDong Li,Yingqi Xu
 * Revision History:
 *  4-dec-2022: project created
 *  11-dec-2022: project complete
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GiftCollectGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private StartScene startScene;
        private HelpScene helpScene;
        private PlayScene playScene;
        private AboutScene aboutScene;
        private ScoreScene scoreScene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        private void hideAllScenes()
        {
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    GameScene gs = (GameScene)item;
                    gs.hide();
                }
            }
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //set the global value
            Shared.Stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //add start startScene
            startScene = new StartScene(this);
            this.Components.Add(startScene);
            startScene.show();

            //help scene
            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            //Play scene
            playScene = new PlayScene(this);
            this.Components.Add(playScene);

            //About scene
            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            //Score Scene
            scoreScene = new ScoreScene(this);
            this.Components.Add(scoreScene);
        }

        protected override void Update(GameTime gameTime)
        {
            int selectIndex = 0;

            //get the keyboard state
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                selectIndex = startScene.Menu.selectIndex;
                if (selectIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    playScene.show();
                }
                else if (selectIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.show();
                }
                else if (selectIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    scoreScene.show();
                }
                else if (selectIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    aboutScene.show();
                }
                else if (selectIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            if (helpScene.Enabled || playScene.Enabled || aboutScene.Enabled || scoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }
            }
            if (Shared.Status == 1)
            {
                hideAllScenes();
                startScene.show();
                Shared.Status = 0;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}