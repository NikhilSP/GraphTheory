using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.GraphBuilder;

public class AdjacencyListGraphBuilder
{
    private readonly HashSet<Node> _nodes = new();
    private readonly AdjacencyListGraph _graph = new ();
    
    public AdjacencyListGraph  Get()
    {
        return _graph;
    }

    public void AddEdge(int sourceId,int targetId)
    {
        var source = BuildNode(sourceId);
        var target = BuildNode(targetId);
        
        _graph.AddEdge(new Edge(source,target));
    }

    private Node BuildNode(int id)
    {
        var node = _nodes.FirstOrDefault(x => x.Id == id);

        if (node is null)
        {
            node = new Node(id);
            _nodes.Add(node);
        }

        return node;
    }
}