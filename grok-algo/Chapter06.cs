namespace grok_algo;

internal class Chapter06
{
    private Node[] BreadthFirstSearch(IReadOnlyDictionary<Node, Node[]> graph, Node startNode, Node finishNode)
    {
        var checkedNodes = new List<Node>(); // опрошенные

        var queue = new Queue<NodeWithPath>();

        var startNodeWithPath = new NodeWithPath(startNode, new[] { startNode });

        queue.Enqueue(startNodeWithPath);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            if (checkedNodes.Contains(node.Node))
                continue;

            checkedNodes.Add(node.Node);
            if (node.Node == finishNode)
            {
                return node.Path;
            }

            if (!graph.ContainsKey(node.Node))
                continue;

            foreach (var newNode in graph[node.Node])
            {
                var newPath = new Node[node.Path.Length + 1];
                node.Path.CopyTo(newPath, 0);
                newPath[node.Path.Length] = newNode;

                queue.Enqueue(new NodeWithPath(newNode, newPath));
            }
        }

        return Array.Empty<Node>();
    }

    public void Test01()
    {
        var graph = new Dictionary<Node, Node[]>
        {
            { Node.N01, new[] { Node.N12 } },
            { Node.N02, new[] { Node.N12 } },
            { Node.N03, new[] { Node.N10 } },
            { Node.N04, new[] { Node.N07, Node.N09, Node.N13 } },
            { Node.N05, new[] { Node.N13, Node.N11 } },
            { Node.N06, new[] { Node.N11, Node.N14 } },
            { Node.N07, new[] { Node.N08 } },
            { Node.N08, new[] { Node.N14 } },
            { Node.N09, new[] { Node.N01, Node.N02, Node.N03 } },
            { Node.N10, new[] { Node.N13, Node.N04 } },
            { Node.N11, new[] { Node.N08 } },
            { Node.N12, new[] { Node.N05, Node.N04 } },
            { Node.N13, new[] { Node.N06 } },
            { Node.N14, new Node[] { } },
        };


        var path = BreadthFirstSearch(graph, Node.N09, Node.N14);
        Console.WriteLine(path.Length == 0
            ? "path not found"
            : $"path found! len = {path.Length}, path: {string.Join(" -> ", path)}");
    }


    private enum Node
    {
        N01,
        N02,
        N03,
        N04,
        N05,
        N06,
        N07,
        N08,
        N09,
        N10,
        N11,
        N12,
        N13,
        N14,
    }

    private class NodeWithPath
    {
        public Node Node { get; }
        public Node[] Path { get; }

        public NodeWithPath(Node node, Node[] path)
        {
            Node = node;
            Path = path;
        }
    }
}