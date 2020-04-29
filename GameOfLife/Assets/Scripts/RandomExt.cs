namespace EmanuelTavares.GameOfLife.Utils
{
    public static class RandomExt
    {
        private static readonly System.Random Random = new System.Random();

        public static System.Random GetRandom()
        {
            return Random;
        }

        public static bool GetNextBool()
        {
            return Random.Next(2) == 0;
        }
    }
}