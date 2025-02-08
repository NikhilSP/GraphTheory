namespace GraphAndAlgorithms.Model;

public class NetworkEdge
{
    public int From { get; set; }
    public int To { get; set; }
    public NetworkEdge Residual { get; set; }
    public long Flow { get; set; }
    public long Cost { get; set; }
    public long Capacity { get; }
    public long OriginalCost { get; } // Added to match the Java code

    public NetworkEdge(int from, int to, long capacity) : this(from, to, capacity, 0) { }

    public NetworkEdge(int from, int to, long capacity, long cost)
    {
        From = from;
        To = to;
        Capacity = capacity;
        OriginalCost = Cost = cost; // Initialize OriginalCost and Cost
    }

    public bool IsResidual()
    {
        return Capacity == 0;
    }

    public long RemainingCapacity()
    {
        return Capacity - Flow;
    }

    public void Augment(long bottleNeck)
    {
        Flow += bottleNeck;
        Residual.Flow -= bottleNeck;
    }

    public string ToString(int s, int t)
    {
        string u = (From == s) ? "s" : ((From == t) ? "t" : From.ToString());
        string v = (To == s) ? "s" : ((To == t) ? "t" : To.ToString());
        return $"Edge {u} -> {v} | flow = {Flow} | capacity = {Capacity} | is residual: {IsResidual()}";
    }
}