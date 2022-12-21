using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GiftCollectGame;

namespace GiftCollectGame
{
    public class Bar : DrawableGameComponent
    {
        public SpriteBatch spriteBatch;
        public Vector2 position { get; set; }
        public Vector2 speed { get; set; }
        public Texture2D tex { get; set; }
        public Bar(Game game, SpriteBatch spriteBatch, Vector2 position, Vector2 speed, Texture2D tex) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.speed = speed;
            this.tex = tex;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                position -= speed;
                if (position.X < 0)
                {
                    position = new Vector2(0, position.Y);
                }
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                position += speed;
                if (position.X >= Shared.Stage.X - tex.Width)
                {
                    position = new Vector2(Shared.Stage.X - tex.Width, position.Y);
                }
            }

            base.Update(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}

