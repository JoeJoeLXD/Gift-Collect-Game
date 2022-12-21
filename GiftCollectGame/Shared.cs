using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GiftCollectGame
{
    //global class to store the stage 
    public class Shared
    {
        public static Vector2 Stage;

        //high scores
        public static List<int> RankScores = new List<int>();

        public static void AddScores(int score)
        {
            RankScores.Add(score);
            RankScores.Sort();
            RankScores.Reverse();
        }
        public static int Status = 0;
    }
}
