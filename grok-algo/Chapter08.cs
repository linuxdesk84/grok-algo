namespace grok_algo;

internal class Chapter08
{
    public Station[] GreedyAlgorithm(Dictionary<Station, HashSet<State>> stationsAndTheirCoverage, HashSet<State> uncoveredStates)
    {
        var coveringStations = new List<Station>();

        while (uncoveredStates.Count > 0)
        {
            var maxCoverage = 0;
            Station? bestStation = null;
            State[]? coveringStates = null;

            foreach (var station in stationsAndTheirCoverage)
            {
                if (coveringStations.Contains(station.Key))
                    continue;

                var stationCoverage = station.Value.Select(x => x).ToHashSet();
                stationCoverage.IntersectWith(uncoveredStates);

                if (maxCoverage < stationCoverage.Count)
                {
                    maxCoverage = stationCoverage.Count;
                    coveringStates = stationCoverage.Select(x => x).ToArray();
                    bestStation = station.Key;
                }
            }

            if (bestStation == null || coveringStates == null)
                break;

            coveringStations.Add(bestStation.Value);
            uncoveredStates.ExceptWith(coveringStates);
        }

        return uncoveredStates.Count == 0
            ? coveringStations.ToArray()
            : Array.Empty<Station>();
    }

    private void Test01Run(Dictionary<Station, HashSet<State>> stationsAndTheirCoverage, HashSet<State> uncoveredStates)
    {
        var res = GreedyAlgorithm(stationsAndTheirCoverage, uncoveredStates);

        Console.WriteLine(res.Length > 0
            ? $"set of covering stations: {string.Join(',', res)}"
            : "set of covering stations not found!");
    }

    public void Test01()
    {
        var stationsAndTheirCoverage = new Dictionary<Station, HashSet<State>>
        {
            { Station.KOne, new HashSet<State> { State.id, State.nv, State.ut } },
            { Station.KTwo, new HashSet<State> { State.wa, State.id, State.mt } },
            { Station.KThree, new HashSet<State> { State.or, State.nv, State.ca } },
            { Station.KFour, new HashSet<State> { State.nv, State.ut } },
            { Station.KFive, new HashSet<State> { State.ca, State.az } },
        };

        var uncoveredStates = stationsAndTheirCoverage
            .Select(station => station.Value)
            .SelectMany(state => state)
            .ToHashSet();

        Test01Run(stationsAndTheirCoverage, uncoveredStates);

        uncoveredStates.Add(State.uncovered);
        Test01Run(stationsAndTheirCoverage, uncoveredStates);
    }

    internal enum Station
    {
        KOne,
        KTwo,
        KThree,
        KFour,
        KFive,
    }

    internal enum State
    {
        mt,
        wa,
        or,
        id,
        nv,
        ut,
        ca,
        az,
        uncovered,
    }
}