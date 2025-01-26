using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

public class BFS
{
    private readonly AdjacencyListGraph _graph;
    private readonly HashSet<Node> _visited;
    private readonly Queue<Node> _queue;
    
    public BFS(AdjacencyListGraph graph, Node startNode)
    {
        _graph = graph;
        _visited = new HashSet<Node>();
        _queue = new Queue<Node>();
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
                _queue.Enqueue(neighbor);
            }
        }

        if (_queue.Count > 0)
        {
            Traverse(_queue.Dequeue());
        }
    }
}