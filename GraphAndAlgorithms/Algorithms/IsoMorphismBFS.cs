using System.Text;
using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

public class IsoMorphismBFS
{
    private readonly AdjacencyListGraph _graphB;
    private readonly AdjacencyListGraph _graphA;

    public IsoMorphismBFS(AdjacencyListGraph graphA,AdjacencyListGraph graphB)
    {
        _graphB = graphB;
        _graphA = graphA.Clone();
    }

    private bool AreSameGraphs(Node startNodeA,Node startNodeB)
    {
        var bfsA = new BFS(_graphA);
        var bfsB = new BFS(_graphB);
        
        bfsA.Traverse(startNodeA);
        bfsB.Traverse(startNodeB);
        
        return bfsA.Result.SequenceEqual(bfsB.Result);
    }
}