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
        var bfsA = new BFS(_graphA).Encode(startNodeA, new StringBuilder());
        var bfsB = new BFS(_graphB).Encode(startNodeB, new StringBuilder());

        return bfsA.ToString().Equals(bfsB.ToString());
    }
}