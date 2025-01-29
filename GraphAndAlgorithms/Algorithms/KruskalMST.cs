using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

public class KruskalMST
{
    private readonly AdjacencyListGraph _graph;

    private readonly Dictionary<Node, NodeData> _nodeData = new();

    private readonly int _nodesCount;

    public KruskalMST(AdjacencyListGraph graph)
    {
        _graph = graph;
        var nodes = _graph.Nodes.ToArray();
        _nodesCount = nodes.Length;

        foreach (var node in nodes)
        {
            _nodeData.Add(node, new NodeData(node, 0));
        }
    }

    public IReadOnlyList<Edge> FindMST()
    {
        var result = new List<Edge>();

        var sortedEdges = _graph.Edges.OrderBy(x => x.Weight).ToArray();

        var i = 0;
        
        while (result.Count < _nodesCount - 1)
        {
            var edge = sortedEdges[i];

            var sourceParent = Find(edge.Source);
            var targetParent = Find(edge.Target);

            if (!sourceParent.Equals(targetParent))
            {
                result.Add(edge);
                Union(sourceParent, targetParent);
            }

            i++;
        }

        return result;
    }

    private Node Find(Node node)
    {
        if (!_nodeData[node].Parent.Equals(node))
        {
            _nodeData[node].Parent = Find(_nodeData[node].Parent);
        }

        return _nodeData[node].Parent;
    }

    private void Union(Node sourceRoot, Node targetRoot)
    {
        if (_nodeData[sourceRoot].Rank < _nodeData[targetRoot].Rank)
        {
            _nodeData[sourceRoot].Parent = targetRoot;
        }
        else if (_nodeData[sourceRoot].Rank > _nodeData[targetRoot].Rank)
        {
            _nodeData[targetRoot].Parent = sourceRoot;
        }
        else
        {
            _nodeData[targetRoot].Parent = sourceRoot;
            _nodeData[sourceRoot].Rank++;
        }
    }


    private class NodeData(Node parent, int rank)
    {
        public Node Parent { get; set; } = parent;
        public int Rank { get; set; } = rank;
    }
}