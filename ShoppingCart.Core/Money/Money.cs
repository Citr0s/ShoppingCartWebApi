namespace ShoppingCart.Core.Money
{
    public class Money
    {
        public string InFull { get; set; }
        public float InPounds { get; set; }
        public int InPence { get; set; }

        public static Money From(int priceInPence)
        {
            var inPounds = (float) priceInPence / 100;

            return new Money
            {
                InPence = priceInPence,
                InPounds = inPounds,
                InFull = inPounds.ToString("C")
            };
        }
    }
}