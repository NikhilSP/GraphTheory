namespace GraphAndAlgorithms.Model;

public class Edge
{
    public Node Source { get; }
    
    public Node Target { get; }
    
    public double Weight { get; }

    public Edge(Node source, Node target, double weight=1)
    {
        Source = source;
        Target = target;
        Weight = weight;
    }
}