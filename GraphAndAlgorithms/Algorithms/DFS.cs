using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

public class DFS
{
    private readonly AdjacencyListGraph _graph;
    private readonly HashSet<Node> _visited;
    private readonly Stack<Node> _stack;
    
    public DFS(AdjacencyListGraph graph, Node startNode)
    {
        _graph = graph;
        _visited = new HashSet<Node>();
        _stack = new Stack<Node>();
        Result = new List<int>();
        
        Traverse(startNode);
    }
    
    public  List<int> Result { get; }

    private void Traverse(Node node)
    {
        Console.WriteLine($"Node {node.Id}");
        
        Result.Add(node.Id);

        if (_visited.Add(node))
        {
            var neighbors = _graph.GetAdjacentVertices(node);

            foreach (var neighbor in neighbors)
            {
                _stack.Push(neighbor);
            }
        }

        if (_stack.Count > 0)
        {
            Traverse(_stack.Pop());
        }
    }
}