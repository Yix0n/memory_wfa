namespace Memory_wfa;

public class Memory
{
    public static (string, string, string)[] CardList = new[]
    {
        ("NameOne", "ImageOne", "ImageTwo"),
        ("NameTwo", "ImageData", "ImageTwo"),
        ("NameThree", "ImageData", "ImageTwo"),
        ("NameFour", "ImageData", "ImageTwo"),
        ("NameFive", "ImageData", "ImageTwo"),
        ("NameSix", "ImageData", "ImageTwo"),
        ("NameSeven", "ImageData", "ImageTwo"),
        ("NameEight", "ImageData", "ImageTwo")
    };
    
    Linear2DArray<Card> CardArray = new (4, 4);
    public Memory() {}

    public void GenerateNew()
    {
        List<Card> cardList = new List<Card>();

        foreach (var cardDetails in CardList)
        {
            cardList.Add(Card.Build(cardDetails.Item1, cardDetails.Item2, cardDetails.Item3));
            cardList.Add(Card.Build(cardDetails.Item1, cardDetails.Item2, cardDetails.Item3));
        }

        Random rng = new Random();

        {
            int n = cardList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (cardList[k], cardList[n]) = (cardList[n], cardList[k]);
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
    public string ImageOne { get; set; }
    public string ImageTwo { get; set; }

    private Card() {}
    
    public static Card Build(string Name, string ImageOne, string ImageTwo)
    {
        return new Card()
        {
            IsDiscovered = false,
            Name = Name,
            ImageOne = ImageOne, 
            ImageTwo = ImageTwo,
        };
    }
}