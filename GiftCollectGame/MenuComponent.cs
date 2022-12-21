using System.Collections.Generic;
using System.Linq;
using GiftCollectGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GiftCollectGame
{
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, highlightFont;
        private Color regularColor = Color.AliceBlue;
        private Color hightlightColor = Color.Yellow;

        private List<string> menuItems;
        public int selectIndex { get; set; }
        private Vector2 position;

        private KeyboardState oldState;
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regularFont, SpriteFont highlightFont, string[] menus) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.highlightFont = highlightFont;
            this.regularFont = regularFont;
            this.menuItems = menus.ToList<string>();
            this.position = new Vector2(Shared.Stage.X / 2, Shared.Stage.Y / 4);     //menu position
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 temPosition = position;
            spriteBatch.Begin();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectIndex == i)
                {
                    spriteBatch.DrawString(highlightFont, menuItems[i], temPosition, hightlightColor);
                    temPosition.Y += highlightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], temPosition, regularColor);
                    temPosition.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectIndex++;
                if (selectIndex == menuItems.Count)
                {
                    selectIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectIndex--;
                if (selectIndex == -1)
                {
                    selectIndex = menuItems.Count - 1;
                }
            }
            oldState = ks;
            base.Update(gameTime);
        }
    }
}
