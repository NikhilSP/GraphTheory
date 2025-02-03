using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

public class LCAEulerianPath
{
    private readonly AdjacencyListGraph _graph;
    private readonly Node _root;
    private readonly int[] _lastPosition;
    private readonly int[] _depth;
    private readonly Node[] _pathNodes;

    public LCAEulerianPath(AdjacencyListGraph graph, Node root)
    {
        _graph = graph;
        _root = root;
        var nodes = _graph.Nodes.ToArray();

        _lastPosition = new int[nodes.Length];
        _depth = new int[2 * nodes.Length - 1];
        _pathNodes = new Node[2 * nodes.Length - 1];

        Compute(root, 0);
    }

    private int _tourIndex = 0;
    
    public void Compute(Node currentNode,int currentDepth)
    {
        Visit(currentNode, currentDepth);
        
        foreach (var child in _graph.GetAdjacentVertices(currentNode).OrderBy(x=>x.Id))
        {
            Compute(child, currentDepth+1 );
            Visit(currentNode, currentDepth);
        }
    }
    
    public void Visit(Node currentNode,int currentDepth)
    {
        _pathNodes[_tourIndex] = currentNode;
        _depth[_tourIndex] = currentDepth;
        _tourIndex+=1;
    }
}