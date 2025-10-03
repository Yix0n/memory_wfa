namespace Memory_wfa;

public class Memory
{
    public static List<Match> matches = new List<Match>
    {

    };

    Linear2DArray<Match> CardArray = new(4, 4);
    private static Random rng = new Random();
    public Memory() { }

    public void GenerateDeck()
    {
        List<Match> selected = new List<Match>();
        HashSet<string> candidates = new HashSet<string>();

        while (selected.Count < 8)
        {
            Match candidate = GetRandomMatch();
            if (candidates.Contains(candidate.Name)) continue;

            selected.Add(candidate);
            foreach (var match in candidate.ValidMatches) candidates.Add(match);
        }

        List<Match> partners = new List<Match>();

        foreach (var card in selected)
        {
            Match partner = GetMatchFrom(card.ValidMatches, selected, partners);
            partners.Add(partner);
        }

        var gameSet = selected.Concat(partners)
            .OrderBy(x => rng.Next()).ToList();
    }

    private static Match GetRandomMatch()
    {
        return matches[rng.Next(matches.Count)];
    }

    private static Match GetMatchFrom(List<string> validNames, List<Match> selected, List<Match> partners)
    {
        var available = matches.Where(m => validNames.Contains(m.Name) &&
                                           !selected.Contains(m) &&
                                           !partners.Contains(m))
                               .ToList();

        if (available.Count == 0)
            throw new InvalidOperationException("No Aviable Matches");

        return available[rng.Next(available.Count)];
    }
}

public class Match
{
    public string Name { get; set; } // np. "Trump"
    public List<string> ValidMatches { get; set; } // np. "Epstein"
    public Bitmap Image { get; set; } // Image from Resource

    public bool CanMatch(Match other)
    {
        return ValidMatches.Contains(other.Name);
    }
}