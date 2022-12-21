using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GiftCollectGame
{
    public class Gift
    {
        public SpriteBatch spriteBatch { get; set; }
        public bool IsAlive { get; set; } = true;
        public Texture2D texGift { get; set; }
        public Vector2 position { get; set; }

        public int Row { get; set; }
        public int Col { get; set; }
        public Gift(int x, int y)
        {
            Row = x;
            Col = y;
        }

        public Rectangle Rectangle { get; set; }

    }
}

