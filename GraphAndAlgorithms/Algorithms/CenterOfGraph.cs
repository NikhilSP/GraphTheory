using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

// Finds center of graph by dropping leaf nodes
public class CenterOfGraph
{
    private readonly AdjacencyListGraph _graph;

    public CenterOfGraph(AdjacencyListGraph graph)
    {
        _graph = graph.Clone();
    }

    private Node[]? FindCenterNodes()
    {
        return RemoveLeafNodes();
    }

    private Node[]? RemoveLeafNodes()
    {
        var leafNodes = _graph.LeafNodes.ToArray();

        if (_graph.Nodes.Count() > 2 && leafNodes.Length == 0)
        {
            return null;
        }

        _graph.RemoveNodes(leafNodes);

        if (_graph.Nodes.Count() <= 2)
        {
            return _graph.Nodes.ToArray();
        }

        return RemoveLeafNodes();
    }
}