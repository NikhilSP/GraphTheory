namespace GraphAndAlgorithms.Model;

public class Node : IEquatable<Node>
{
    public int Id { get; }

    public Node(int id)
    {
        Id = id;
    }

    public bool Equals(Node? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Node)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}