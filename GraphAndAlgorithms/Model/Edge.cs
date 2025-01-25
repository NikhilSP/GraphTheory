namespace GraphAndAlgorithms.Model;

public class Edge
{
    public Node Source { get; }
    
    public Node Target { get; }

    public Edge(Node source, Node target)
    {
        Source = source;
        Target = target;
    }
}