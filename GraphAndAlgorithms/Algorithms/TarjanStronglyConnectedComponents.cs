using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

public class TarjanStronglyConnectedComponents
{
    private readonly AdjacencyListGraph _graph;
    private readonly int[] _ids;
    private readonly int[] _lows;
    private readonly bool[] _visited;
    private readonly bool[] _onStack;
    private readonly Stack<Node> _stack;
    private readonly List<Node> _nodes;

    public readonly List<List<Node>> StronglyConnectedComponents;

    private int _id;

    public TarjanStronglyConnectedComponents(AdjacencyListGraph graph)
    {
        _graph = graph;
        _nodes = _graph.Nodes.ToList();

        _ids = new int[_nodes.Count];
        _lows = new int[_nodes.Count];
        _onStack = new bool[_nodes.Count];
        _visited = new bool[_nodes.Count];
        _stack = new Stack<Node>();

        StronglyConnectedComponents = new();

        Array.Fill(_onStack, false);

        for (var i = 0; i < _nodes.Count; i++)
        {
            if (!_visited[i])
            {
                Solve(i, _nodes[i]);
            }
        }
    }

    private void Solve(int nodeIndex, Node node)
    {
        _ids[nodeIndex] = _id;
        _lows[nodeIndex] = _id;
        _id++;

        _stack.Push(node);
        _onStack[nodeIndex] = true;
        _visited[nodeIndex] = true;

        foreach (var neighbor in _graph.GetAdjacentVertices(node))
        {
            var neighborIndex = _nodes.IndexOf(neighbor);

            if (!_visited[neighborIndex])
            {
                Solve(neighborIndex, neighbor);
            }

            if (_onStack[neighborIndex])
            {
                _lows[nodeIndex] = Math.Min(_lows[nodeIndex], _lows[neighborIndex]);
            }
        }

        if (_ids[nodeIndex] == _lows[nodeIndex])
        {
            var scc = new List<Node>();
            Node temp;
            do
            {
                temp = _stack.Pop();
                var tempIndex = _nodes.IndexOf(temp);
                _onStack[tempIndex] = false;
                scc.Add(temp);
            } while (!temp.Equals(node));

            if (scc.Any())
            {
                StronglyConnectedComponents.Add(scc);
            }
        }
    }
}