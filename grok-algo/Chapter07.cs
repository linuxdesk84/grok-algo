namespace grok_algo;

internal class Chapter07
{
    private (int, Node[]) DijkstraAlgorithm(IReadOnlyDictionary<Node, WeightedEdge[]> weightedGraph, Node start, Node finish)
    {
        var processedNodes = new List<Node>();

        var pathCosts = new Dictionary<Node, PathCost>
        {
            { start, new PathCost(start, start, 0) },
        };


        var queue = new Queue<Node>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();
            var pathCostOfCurrentNode = pathCosts[currentNode];

            if (processedNodes.Contains(currentNode) || currentNode == finish)
                continue;

            processedNodes.Add(currentNode);

            if (!weightedGraph.ContainsKey(currentNode))
                continue;

            var edges = weightedGraph[currentNode];
            Array.Sort(edges, (edge1, edge2) => edge1.Weight - edge2.Weight);

            foreach (var weightedEdge in edges)
            {
                var nodeTo = weightedEdge.NodeTo;
                queue.Enqueue(nodeTo);

                var newPathCost = new PathCost(nodeTo, currentNode,
                    pathCostOfCurrentNode.Cost + weightedEdge.Weight);


                if (!pathCosts.ContainsKey(nodeTo))
                {
                    pathCosts[nodeTo] = newPathCost;
                    continue;
                }

                var currentCostOfNodeTo = pathCosts[nodeTo];
                if (newPathCost.Cost < currentCostOfNodeTo.Cost)
                {
                    currentCostOfNodeTo.Update(currentNode, newPathCost.Cost);
                }
            }
        }

            
        // result
        var path = new Stack<Node>();
        path.Push(finish);

        var currentFinish = finish;
        while (pathCosts.ContainsKey(currentFinish) && currentFinish != start)
        {
            var parent = pathCosts[currentFinish].Parent;
            path.Push(parent);

            currentFinish = parent;
        }

        return currentFinish != start 
            ? (0, Array.Empty<Node>() )
            : (pathCosts[finish].Cost, path.ToArray());
    }

    class PathCost
    {
        public Node Node { get; }
        public Node Parent { get; private set; }
        public int Cost { get; private set; }

        public PathCost(Node node, Node parent, int cost)
        {
            Node = node;
            Parent = parent;
            Cost = cost;
        }

        public void Update(Node newParent, int newCost)
        {
            Parent = newParent;
            Cost = newCost;
        }

        public override string ToString()
        {
            return $"Cost={Cost}, Node={Node}, Parent={Parent}";
        }
    }

    public void Test01()
    {
        var weightedGraph = new Dictionary<Node, WeightedEdge[]>()
        {
            { Node.N01, new[] { new WeightedEdge(5 , Node.N01, Node.N02), new WeightedEdge(0 , Node.N01, Node.N03) } },
            { Node.N02, new[] { new WeightedEdge(15, Node.N02, Node.N04), new WeightedEdge(20, Node.N02, Node.N05) } },
            { Node.N03, new[] { new WeightedEdge(35, Node.N03, Node.N05), new WeightedEdge(30, Node.N03, Node.N04) } },
            { Node.N04, new[] { new WeightedEdge(20, Node.N04, Node.N06) } },
            { Node.N05, new[] { new WeightedEdge(10, Node.N05, Node.N06) } },
        };

        var res = DijkstraAlgorithm(weightedGraph, Node.N01, Node.N06);
        Console.WriteLine(res.Item2.Length == 1
            ? "path not found!"
            : $"path found! cost={res.Item1}, path: {string.Join("->", res.Item2)}");
    }

    private enum Node
    {
        N01,
        N02,
        N03,
        N04,
        N05,
        N06,
    }

    class WeightedEdge
    {
        private Node NodeFrom { get; }
        public Node NodeTo { get; }
        public int Weight { get; }
            
        public WeightedEdge(int weight, Node nodeFrom, Node nodeTo)
        {
            Weight = weight;

            NodeFrom = nodeFrom;
            NodeTo = nodeTo;
        }

        public override string ToString()
        {
            return $"{Weight}: {NodeFrom} -> {NodeTo}";
        }
    }
}