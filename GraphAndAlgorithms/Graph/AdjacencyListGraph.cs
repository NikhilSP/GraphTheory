using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Graph;

public class AdjacencyListGraph
{
    private readonly bool _directed;
    private readonly Dictionary<Node, List<Edge>> _edgesByNode = new();
    private readonly HashSet<Edge> _edges = new();

    public IEnumerable<Edge> Edges => _edges;
    public IEnumerable<Node> Nodes => _edgesByNode.Keys;
    public IEnumerable<Node> LeafNodes => _edgesByNode.Where(x=>x.Value.Count==1).Keys;
    
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
    
    public IReadOnlyList<Edge> GetEdges(Node node1)
    {
        return _edgesByNode.GetValueOrDefault(node1) ?? [];
    }

    public void RemoveNodes(Node[] nodes)
    {
        foreach (var node in nodes)
        {
            RemoveNode(node);
        }
    }

    public void RemoveNode(Node node)
    {
        foreach (var edge in GetEdges(node))
        {
            RemoveEdge(edge);
        }

        _edgesByNode.Remove(node);
    }

    public void RemoveEdge(Edge edge)
    {
        if (_edgesByNode.TryGetValue(edge.Source, out var value))
        {
            value.Remove(edge);
        }
        else
        {
            return;
        }

        if (!_directed)
        {
            _edgesByNode[edge.Target].Remove(edge);
        }

        _edges.Remove(edge);
    }

    public void RemoveAllEdges(Node node)
    {
        if (_edgesByNode.TryGetValue(node, out var value))
        {
            var edgesToBeRemoved = value.ToList();
            
            foreach (var edge in  edgesToBeRemoved)
            {
                RemoveEdge(edge);
            }
        }
    }

    public void PruneNodes()
    {
        var singlesNodes = _edgesByNode.Where(x => !x.Value.Any()).Select(x=>x.Key);

        foreach (var singlesNode in singlesNodes)
        {
            _edgesByNode.Remove(singlesNode);
        }
        
    }

    public void Clear()
    {
        _edgesByNode.Clear();
        _edges.Clear();
    }
}