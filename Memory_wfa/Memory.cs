namespace Memory_wfa;

public class Memory
{
    public static (string, string)[] CardList = new[]
    {
        ("Name", "ImageData"),
        ("Name", "ImageData"),
        ("Name", "ImageData"),
        ("Name", "ImageData"),
        ("Name", "ImageData"),
        ("Name", "ImageData"),
        ("Name", "ImageData"),
        ("Name", "ImageData"),
    };
    
    Linear2DArray<Card> CardArray = new Linear2DArray<Card>(4, 4);
    public Memory() {}

    public void GenerateNew()
    {
        List<Card> cardList = new List<Card>();

        foreach (var cardDetails in CardList)
        {
            cardList.Add(Card.Build(cardDetails.Item1, cardDetails.Item2));
            cardList.Add(Card.Build(cardDetails.Item1, cardDetails.Item2));
        }

        Random rng = new Random();

        {
            int n = cardList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cardList[k];
                cardList[k] = cardList[n];
                cardList[n] = value;
            }
        }

        for (int i = 0; i < cardList.Count; i++)
        {
            int x = i / CardArray.Width;
            int y = i  % CardArray.Width;
            CardArray[x,y] = cardList[i];
        }
    }
}

public class Card
{
    public bool IsDiscovered { get; set; }
    public string Name { get; set; }
    public byte[] Image { get; set; }

    private Card() {}
    
    public static Card Build(string Name, string ImageRef)
    {
        return new Card()
        {
            IsDiscovered = false,
            Name = Name,
            Image = [], // ImageRef TODO
        };
    }
}