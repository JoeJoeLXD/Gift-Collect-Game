/*  
 * Programmed by: XiangDong Li,Yingqi Xu
 * Revision History:
 *  4-dec-2022: project created
 *  11-dec-2022: project complete
 */
using System;
using System.Collections.Generic;
using GiftCollectGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1;

namespace GiftCollectGame
{
    public class PlayScene : GameScene
    {
        //bar
        private Texture2D barTex;
        private Vector2 barSpeed;
        private Vector2 barInitPos;

        //santa
        private Texture2D santaTex;
        private Vector2 santaSpeed;
        private Vector2 santaInitPos;

        //gift
        public Texture2D giftTex;

        //background
        public Texture2D backTex;

        //explosion
        Explosion exp;

        //gift
        private List<Gift> gifts;

        //sounds
        SoundEffect dingSound;
        SoundEffect clickSound;
        SoundEffect applause;

        //score & font
        private SpriteFont font;
        string scoreRecord;
        int scoreCount = 0;
        int giftCount = 30;

        Vector2 scoreCountPos;
        Vector2 scoreDrawPoint = new Vector2(0.1f, 0.5f);

        public PlayScene(Game game) : base(game)
        {
            //load sounds
            dingSound = g.Content.Load<SoundEffect>("sound/ding");
            clickSound = g.Content.Load<SoundEffect>("sound/hit");
            applause = g.Content.Load<SoundEffect>("sound/applause");

            //add santa
            santaTex = g.Content.Load<Texture2D>("images/santa");
            santaSpeed = new Vector2(4, -3);
            santaInitPos = new Vector2(Shared.Stage.X / 2 - santaTex.Width / 2, Shared.Stage.Y / 2 - santaTex.Height / 2 + 100);

            //add bar
            barTex = g.Content.Load<Texture2D>("images/fireBar");
            barSpeed = new Vector2(4, 0);
            barInitPos = new Vector2(Shared.Stage.X / 2 - barTex.Width / 2, Shared.Stage.Y - barTex.Height);

            //add gift
            gifts = new List<Gift>(); // gift list
            for (int i = 0; i < 3; i++)  //row 3
            {
                for (int j = 0; j < 10; j++)  //col 10
                {
                    Gift g = new Gift(i, j);
                    g.Rectangle = new Rectangle(10 + j * 80, i * 80, 80, 80);  //each size initial
                    gifts.Add(g);
                }
            }
            giftTex = g.Content.Load<Texture2D>("images/gift");

            //add backgroud
            backTex = g.Content.Load<Texture2D>("images/sky");

            //add backgroud music
            Song backgroundMusic = g.Content.Load<Song>("sound/music");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);

            //score position
            scoreCountPos = new Vector2(scoreDrawPoint.X * 600, scoreDrawPoint.Y * 800);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            scoreRecord = scoreCount.ToString();

            //draw background
            spriteBatch.Draw(backTex, Vector2.Zero, Color.White);

            //draw santa
            spriteBatch.Draw(santaTex, santaInitPos, Color.White);

            //draw bar
            spriteBatch.Draw(barTex, barInitPos, Color.White);

            //load font
            font = g.Content.Load<SpriteFont>("fonts/regularFont");

            spriteBatch.DrawString(font, "Current Score: " + scoreRecord, scoreCountPos, Color.Yellow);

            //add gift
            foreach (var item in gifts)
            {
                if (item.IsAlive)
                {
                    spriteBatch.Draw(giftTex, item.Rectangle, Color.White);

                }
            }

            spriteBatch.End();
            base.Draw(gameTime);

        }
        public override async void Update(GameTime gameTime)
        {
            //Get the rectangle of each 
            Rectangle santaRect = new Rectangle((int)santaInitPos.X, (int)santaInitPos.Y, santaTex.Width, santaTex.Height);
            Rectangle barRect = new Rectangle((int)barInitPos.X, (int)barInitPos.Y, barTex.Width, barTex.Height);

            santaInitPos += santaSpeed;  //santa move

            //top wall
            if (santaInitPos.Y <= 0)
            {
                santaSpeed = new Vector2(santaSpeed.X, -santaSpeed.Y);
                dingSound.Play();
            }

            //bottom wall
            if (santaInitPos.Y > Shared.Stage.Y - santaTex.Height / 2)
            {
                applause.Play();
                this.Enabled = false;
                Shared.AddScores(scoreCount);
                string[] button = { "YES", "NO" };
                var result = MessageBox.Show($"play times: {Shared.RankScores.Count} ", "One More Try?", button);
                var select = await result;

                //game over
                if (select == 0)
                {
                    GameInitialize();
                }
                else
                {
                    Shared.Status = 1;

                    GameInitialize();
                }

            }

            //right wall
            if (santaInitPos.X > Shared.Stage.X - santaTex.Width)
            {
                santaSpeed = new Vector2(-santaSpeed.X, santaSpeed.Y);
                dingSound.Play();
            }

            //left wall
            if (santaInitPos.X <= 0)
            {
                santaSpeed = new Vector2(-santaSpeed.X, santaSpeed.Y);
                dingSound.Play();
            }

            //scoreCount
            foreach (var p in gifts)
            {
                if (p.IsAlive)
                {
                    if (p.Rectangle.Intersects(santaRect))
                    {
                        p.IsAlive = false;
                        santaSpeed = new Vector2(santaSpeed.X, -santaSpeed.Y);
                        giftCount--;
                        scoreCount++;
                    }
                }
            }

            //game ends
            if (giftCount == 0)
            {
                applause.Play();
                this.Enabled = false;
                Shared.AddScores(scoreCount);
                string[] button = { "Retry", " Quit" };
                var result = MessageBox.Show($"play times: {Shared.RankScores.Count} ", "Game End", button);
                var select = await result;
                if (select == 0)
                {
                    GameInitialize();
                }
                else
                {
                    Shared.Status = 1;
                    GameInitialize();
                }
            }

            //bar move
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                barInitPos -= barSpeed;
                if (barInitPos.X < 0)
                {
                    barInitPos = new Vector2(0, barInitPos.Y);
                }
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                barInitPos += barSpeed;
                if (barInitPos.X >= Shared.Stage.X - barTex.Width)
                {

                    barInitPos = new Vector2(Shared.Stage.X - barTex.Width, barInitPos.Y);
                }
            }


            //collision between bar and santa
            if (barRect.Intersects(santaRect))
            {
                santaSpeed = new Vector2(santaSpeed.X, -Math.Abs(santaSpeed.Y));
                clickSound.Play();
                Texture2D expTex = g.Content.Load<Texture2D>("images/explosion");
                exp = new Explosion(g, spriteBatch, expTex, Vector2.Zero, 0);
                g.Components.Add(exp);
                exp.position = new Vector2(santaRect.X, barRect.Y - 40);

                exp.Draw(gameTime);
                exp.show();
            }

            base.Update(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        private void GameInitialize()
        {
            this.Enabled = true;

            giftCount = 30;
            for (int i = 0; i < gifts.Count; i++)
            {
                gifts[i].IsAlive = true;

            }

            santaInitPos = new Vector2(Shared.Stage.X / 2 - santaTex.Width / 2, Shared.Stage.Y / 2 - santaTex.Height / 2 + 100);
            barInitPos = new Vector2(Shared.Stage.X / 2 - barTex.Width / 2, Shared.Stage.Y - barTex.Height);

            scoreCount = 0;
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}
