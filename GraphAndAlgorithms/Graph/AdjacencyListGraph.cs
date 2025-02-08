using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Graph;

public class AdjacencyListGraph
{
    private readonly bool _directed;
    private readonly Dictionary<Node, List<Edge>> _edgesByNode = new();
    private readonly HashSet<Edge> _edges = new();

    public IEnumerable<Edge> Edges => _edges;
    public IEnumerable<Node> Nodes =>  _edges.SelectMany(x => new[] { x.Source, x.Target }).ToHashSet();
    public IEnumerable<Node> LeafNodes => !_directed ? NodesWithOutDegree(1) : NodesWithOutDegree(0); // check for directed graph
    
    public AdjacencyListGraph(bool directed = false)
    {
        _directed = directed;
    }
    
    public IEnumerable<Node> NodesWithOutDegree(int degree)
    {
        return _edgesByNode.Where(x => x.Value.Count == degree).Select(x => x.Key);
    }
    
    public IEnumerable<Node> NodesWithInDegree(int degree)
    {
        var inDegreeCounts = new Dictionary<Node, int>();
        foreach (var edge in _edges)
        {
            inDegreeCounts.TryAdd(edge.Target, 0);
            inDegreeCounts[edge.Target]++;
        }

        return inDegreeCounts.Where(x => x.Value == degree).Select(x => x.Key);
    }

    public void AddEdge(Edge edge)
    {
        if (ContainsEdge(edge))
        {
            return;
        }

        AddNode(edge.Source);
        _edgesByNode[edge.Source].Add(edge);

        if (!_directed)
        {
            AddNode(edge.Target);
            _edgesByNode[edge.Target].Add(new Edge(edge.Target, edge.Source, edge.Weight));
        }
        
        _edges.Add(edge);
    }

    private void AddNode(Node node)
    {
        if (!ContainsNode(node))
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
            return edges.First(x => x.Target.Equals(node2));
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
            RemoveAllEdges(node);
        }
    }
    

    public void RemoveEdge(Edge edge)
    {
        if (_edgesByNode.TryGetValue(edge.Source, out var value))
        {
            value.Remove(edge);
            _edges.Remove(edge);
        }
        else
        {
            return;
        }

        if (!_directed)
        {
            if (_edgesByNode.TryGetValue(edge.Target, out var edges))
            {
                var edgeToBeRemoved = GetEdge(edge.Target, edge.Source);

                if (edgeToBeRemoved is not null)
                {
                    edges.Remove(edgeToBeRemoved);
                }
            }
        }
    }

    public void RemoveAllEdges(Node node)
    {
        if (_edgesByNode.TryGetValue(node, out var value))
        {
            var edgesToBeRemoved = value.ToList();
            
            foreach (var edge in  edgesToBeRemoved)
            {
                if (!_directed)
                {
                    _edgesByNode[edge.Target].Remove(GetEdge(edge.Target,node)!);
                }
                _edges.Remove(edge);
            }
        }

        _edgesByNode.Remove(node);
    }

    public void PruneNodes()
    {
        var singlesNodes = _edgesByNode
            .Where(x => x.Value.Count == 0)
            .Select(x=>x.Key);

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
    
    public bool ContainsEdge(Edge edge)
    {
        return _edges.Contains(edge);
    }

    public bool ContainsEdge(Node source, Node target)
    {
        return GetEdge(source, target) != null;
    }
    
    public bool ContainsNode(Node node)
    {
        return _edgesByNode.ContainsKey(node);
    }
    
    public AdjacencyListGraph Clone()
    {
        var clone = new AdjacencyListGraph();

        foreach (var edge in Edges)
        {
            clone.AddEdge(edge);
        }

        return clone;
    }
    
    // The properties of edge and node are get only. So using deep clone can be overkill
    public AdjacencyListGraph DeepClone()
    {
        var clone = new AdjacencyListGraph(_directed);

        foreach (var edge in Edges)
        {
            clone.AddEdge(new Edge(edge.Source,edge.Target,edge.Weight));
        }

        return clone;
    }
}