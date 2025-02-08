namespace GraphAndAlgorithms.Model;

public class Edge: IEquatable<Edge>
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

    public bool Equals(Edge? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Source.Equals(other.Source) && Target.Equals(other.Target);// && Weight.Equals(other.Weight);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Edge)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Source, Target);//, Weight);
    }
}