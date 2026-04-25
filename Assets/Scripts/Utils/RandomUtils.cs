namespace Assets.Scripts.Utils
{
    public static class RandomUtils
    {
        private static readonly System.Random _random = new();

        private static readonly int _minChanceValue = 0;
        private static readonly int _maxChanceValue = 100;

        public static bool IsSuccess(int chance)
        {
            return _random.Next(_minChanceValue, _maxChanceValue) < chance;
        }

        public static int GetValue(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue + 1);
        }
    }
}
