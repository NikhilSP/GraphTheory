using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Graph;

public class AdjacencyListGraph
{
    private readonly bool _directed;
    private readonly Dictionary<Node, List<Edge>> _edgesByNode = new();
    private readonly HashSet<Edge> _edges = new();

    public AdjacencyListGraph(bool directed = false)
    {
        _directed = directed;
    }

    public void AddEdge(Edge edge)
    {
        if (_edges.Contains(edge))
        {
            return;
        }

        AddNode(edge.Source);
        _edgesByNode[edge.Source].Add(edge);

        if (!_directed)
        {
            AddNode(edge.Target);
            _edgesByNode[edge.Target].Add(edge);
        }
        
        _edges.Add(edge);
    }

    private void AddNode(Node node)
    {
        if (!_edgesByNode.ContainsKey(node))
        {
            _edgesByNode[node] = new List<Edge>();
        }
    }

    public IEnumerable<Node> GetAdjacentVertices(Node node)
    {
        if (_edgesByNode.TryGetValue(node, out var edges))
        {
            return edges.Select(x => x.Target);
        }

        return [];
    }

    public Edge? GetEdge(Node node1, Node node2)
    {
        if (_edgesByNode.TryGetValue(node1, out var edges))
        {
            return edges.First(x => x.Target == node2);
        }
        
        return null;
    }

    public void RemoveEdge(Edge edge)
    {
        if (_edgesByNode.ContainsKey(edge.Source))
        {
            _edgesByNode[edge.Source].Remove(edge);
        }

        if (!_directed)
        {
            _edgesByNode[edge.Target].Remove(edge);
        }

        _edges.Remove(edge);
    }

    public void RemoveAllEdges(Node node)
    {
        if (_edgesByNode.ContainsKey(node))
        {
            var edgesToBeRemoved = _edgesByNode[node].ToList();
            
            foreach (var edge in  edgesToBeRemoved)
            {
                RemoveEdge(edge);
            }
        }
    }

    public void Clear()
    {
        _edgesByNode.Clear();
        _edges.Clear();
    }
}