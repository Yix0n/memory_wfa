using Memory_wfa.Properties;
using System.Linq;

namespace Memory_wfa;

public static class Memory
{
    public static List<Match> matches = new List<Match>
    {
        new Match("trump", new List<string>{"epstein"}, Properties.Resources.trump),
        new Match("epstein", new List<string>{"trump"}, Properties.Resources.epstein),
        new Match("jew", new List<string>{"money"}, Properties.Resources.jew),
        new Match("money", new List<string>{"jew"}, Properties.Resources.money),
        // new Match("woman", new List<string>{"money"}, Properties.Resources.woman),
        new Match("blackman", new List<string>{"kfc"}, Properties.Resources.blackman),
        new Match("kfc", new List<string>{"blackman"}, Properties.Resources.kfc),
        new Match("george", new List<string>{"pluca"}, Properties.Resources.george),
        new Match("pluca", new List<string>{"george"}, Properties.Resources.pluca),
        new Match("osama", new List<string>{"wtc"}, Properties.Resources.osama),
        new Match("wtc", new List<string>{"osama"}, Properties.Resources.wtc),
        new Match("hajto", new List<string>{"babapasy"}, Properties.Resources.hajto),
        new Match("babapasy", new List<string>{"hajto"}, Properties.Resources.babapasy),
        new Match("papaj", new List<string>{"kremowka"}, Properties.Resources.jp2),
        new Match("kremowka", new List<string>{"papaj"}, Properties.Resources.kremowka),
        new Match("diddy", new List<string>{"babyoil"}, Properties.Resources.diddy),
        new Match("babyoil", new List<string>{"diddy"}, Properties.Resources.babyoil),
    };

    static Linear2DArray<Match> CardArray = new(4, 4);
    private static Random rng = new Random();

    public static void GenerateDeck()
    {
        List<Match> selected = new List<Match>();
        HashSet<string> candidates = new HashSet<string>();

        while (selected.Count < 8)
        {
            Match candidate = GetRandomMatch();
            if (candidates.Contains(candidate.Name)) continue;

            selected.Add(candidate);
            foreach (var match in candidate.ValidMatches) candidates.Add(match);
            candidates.Add(candidate.Name);
        }

        List<Match> partners = new List<Match>();

        foreach (var card in selected)
        {
            Match partner = GetMatchFrom(card.ValidMatches, selected, partners);
            partners.Add(partner);
        }

        var gameSet = selected.Concat(partners).ToList();

        Shuffle(gameSet);

        for (int i = 0; i < 16; i++)
        {
            CardArray[i] = gameSet.ToList()[i];
        }
    }

    private static Match GetStaticMatch(int index)
    {
        return matches[index];
    }

    public static Match GetMatch(int index)
    {
        return CardArray[index];
    }

    private static Match GetRandomMatch()
    {
        return matches[rng.Next(matches.Count)];
    }

    private static Match GetMatchFrom(List<string> validNames, List<Match> selected, List<Match> partners)
    {
        var available = matches.Where(m => validNames.Contains(m.Name) &&
                                           !selected.Any(s => s.Name == m.Name) &&
                                           !partners.Any(p => p.Name == m.Name))
            .ToList();

        if (available.Count == 0)
            throw new InvalidOperationException($"No Aviable Matches for {string.Join(", ", validNames)}");

        return available[rng.Next(available.Count)];
    }

    private static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public class Match
{
    public string Name { get; set; } // np. "Trump"
    public List<string> ValidMatches { get; set; } // np. "Epstein"
    public Bitmap Image { get; set; } // Image from Resource

    public Match(string name,  List<string> validMatches, Bitmap image)
    {
        Name = name;
        ValidMatches = validMatches;
        Image = image;
    }
    
    public bool CanMatch(Match other)
    {
        return ValidMatches.Contains(other.Name);
    }

    public Match Copy()
    {
        return new Match(Name, ValidMatches, Image);
    }
}