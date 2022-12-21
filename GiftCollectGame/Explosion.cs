using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace GiftCollectGame
{
    public class Explosion : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Vector2 position { get; set; }
        private Vector2 dimension; // 64x64
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;

        private const int ROWS = 5;
        private const int COLS = 5;
        Game g;

        public Explosion(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.delay = delay;
            this.dimension = new Vector2(tex.Width / COLS, tex.Height / ROWS);
            //hide the animation
            hide();
            //create frames
            createFrames();
            this.g = game;

        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y,
                        (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

        public void restart()
        {
            frameIndex = -1;
            show();
        }

        public void hide()
        {
            this.Visible = false;
            this.Enabled = false;
        }
        public void show()
        {
            this.Visible = true;
            this.Enabled = true;
        }


        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > ROWS * COLS - 1)
                {
                    frameIndex = -1;
                    //hide();
                    g.Components.Remove(this);
                }

                delayCounter = 0;
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                //v4
                //frames.ElementAt(frameIndex);
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.White);

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
