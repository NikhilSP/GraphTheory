using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

public abstract class NetworkFlowSolverBase
{
    protected static readonly long Inf = long.MaxValue / 2;
    
    protected readonly int NumberOfNodes, SourceIndex, SinkIndex;

    protected long MaxFlow;
    protected readonly long MinCost;

    protected readonly bool[] MinCut;
    
    protected  List<NetworkEdge>[] Graph;

    private bool[] _visited;

    
    protected NetworkFlowSolverBase(int numberOfNodes, int sourceIndex, int sinkIndex)
    {
        NumberOfNodes = numberOfNodes;
        SourceIndex = sourceIndex;
        SinkIndex = sinkIndex;
        InitializeGraph();
        MinCut = new bool[numberOfNodes];
        _visited = new bool[numberOfNodes];
    }

    private void InitializeGraph()
    {
        Graph = new List<NetworkEdge>[NumberOfNodes];
        for (int i = 0; i < NumberOfNodes; i++)
        {
            Graph[i] = new List<NetworkEdge>();
        }
    }
    
    public void AddEdge(int from, int to, long capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentException("Capacity < 0");
        }

        var e1 = new NetworkEdge(from, to, capacity);
        var e2 = new NetworkEdge(to, from, 0);
        e1.Residual = e2;
        e2.Residual = e1;
        Graph[from].Add(e1);
        Graph[to].Add(e2);
    }

    /** Cost variant of {@link #AddEdge(int, int, int)} for min-cost max-flow */
    public void AddEdge(int from, int to, long capacity, long cost)
    {
        var e1 = new NetworkEdge(from, to, capacity, cost);
        var e2 = new NetworkEdge(to, from, 0, -cost);
        e1.Residual = e2;
        e2.Residual = e1;
        Graph[from].Add(e1);
        Graph[to].Add(e2);
    }

    protected void Visit(int i)
    {
        _visited[i] = true;
    }

    protected bool Visited(int i)
    {
        return _visited[i];
    }

    protected void MarkAllNodesAsUnvisited()
    {
        for (int i = 0; i < _visited.Length; i++)
        {
            _visited[i] = false;
        }
    }

    public List<NetworkEdge>[] GetGraph()
    {
        return Graph;
    }

    public long GetMaxFlow()
    {
        return MaxFlow;
    }

    public long GetMinCost()
    {
        return MinCost;
    }

    public bool[] GetMinCut()
    {
        return MinCut;
    }

    public abstract void Solve();
}