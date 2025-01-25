namespace GraphAndAlgorithms;

public abstract class GraphBase
{
    protected GraphBase(int numVertices, bool directed = false)
    {
        NumVertices = numVertices;
        Directed = directed;
    }
    
    protected int NumVertices { get; }

    protected bool Directed { get; }

    public abstract void AddEdge(int v1, int v2, int weight=1);

    public abstract IEnumerable<int> GetAdjacentVertices(int v);

    public abstract int GetEdgeWeight(int v1, int v2);

    public abstract void RemoveEdge(int v1, int v2);
    
    public abstract void RemoveAllEdges(int v);
    
    public abstract void Clear();
}


