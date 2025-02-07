using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

public class TarjanStronglyConnectedComponentsSimplified
{
    private class NodeData
    {
        public int Id { get; set; }
        public int Lows { get; set; }
        public bool Visited { get; set; }
        public bool OnStack { get; set; }
    }
    
    private readonly AdjacencyListGraph _graph;
    private readonly Dictionary<Node, NodeData> _nodeData;
    private readonly Stack<Node> _stack;
    private readonly List<Node> _nodes;

    public readonly List<List<Node>> StronglyConnectedComponents;

    public TarjanStronglyConnectedComponentsSimplified(AdjacencyListGraph graph)
    {
        _graph = graph;
        _nodes = _graph.Nodes.ToList();
        _nodeData = new Dictionary<Node, NodeData>();
        
        _stack = new Stack<Node>();
        StronglyConnectedComponents = new();

        for (var index = 0; index < _nodes.Count; index++)
        {
            var node = _nodes[index];
            _nodeData.Add(node,new NodeData()
            {
                Id = index,
                Lows = index,
                Visited = false,
                OnStack = false,
            });
        }

        foreach (var currentNode in _nodes)
        {
            if (!_nodeData[currentNode].Visited)
            {
                Solve(currentNode);
            }
        }
    }

    private void Solve(Node node)
    {
        _stack.Push(node);
        _nodeData[node].OnStack = true;
        _nodeData[node].Visited = true;

        foreach (var neighbor in _graph.GetAdjacentVertices(node))
        {
            if (!_nodeData[neighbor].Visited)
            {
                Solve(neighbor);
            }

            if (_nodeData[neighbor].OnStack)
            {
                _nodeData[node].Lows = Math.Min(_nodeData[node].Lows, _nodeData[neighbor].Lows);
            }
        }

        if (_nodeData[node].Lows == _nodeData[node].Id)
        {
            var scc = new List<Node>();
            Node temp;
            do
            {
                temp = _stack.Pop();
                _nodeData[temp].OnStack = false;
                scc.Add(temp);
            } while (!temp.Equals(node));

            if (scc.Any())
            {
                StronglyConnectedComponents.Add(scc);
            }
        }
    }
}