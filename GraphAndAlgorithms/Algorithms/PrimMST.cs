using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

public class PrimMST
{
    private readonly AdjacencyListGraph _graph;

    private HashSet<Node> _visitedNodes = new();
    private HashSet<Edge> _visitedEdges = new();
    private PriorityQueue<Edge, double> _priorityQueue = new();

    public PrimMST(AdjacencyListGraph graph)
    {
        _graph = graph;
    }

    public IReadOnlyList<Edge> FindMST(Node start)
    {
        var result = new List<Edge>();

        AddEdgesToQueue(start);

        while (_priorityQueue.Count > 0 && result.Count < _graph.Nodes.Count())
        {
            var currentEdge = _priorityQueue.Dequeue();

            if (!_visitedNodes.Contains(currentEdge.Source))
            {
                result.Add(currentEdge);
                AddEdgesToQueue(currentEdge.Source);
            }
            else if (!_visitedNodes.Contains(currentEdge.Target))
            {
                result.Add(currentEdge);
                AddEdgesToQueue(currentEdge.Target);
            }
        }


        return result;
    }

    private void AddEdgesToQueue(Node start)
    {
        _visitedNodes.Add(start);
        var edges = _graph.GetEdges(start);

        foreach (var edge in edges)
        {
            if (_visitedEdges.Add(edge))
            {
                _priorityQueue.Enqueue(edge, edge.Weight);
            }
           
        }
    }
}