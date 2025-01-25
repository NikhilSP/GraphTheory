using GraphAndAlgorithms.Model;

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

    public abstract void AddEdge(Node node1,Node node2, int weight=1);

    public abstract IEnumerable<int> GetAdjacentVertices(Node node);

    public abstract int GetEdgeWeight(Node node1, Node node2);

    public abstract void RemoveEdge(Node node1, Node node2);
    
    public abstract void RemoveAllEdges(Node node);
    
    public abstract void Clear();
}


