using System.Text;
using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

// AHU -> AHO, HopCroft, Ullman
public class IsoMorphismAHU
{
    private readonly HashSet<Node> _visited = new ();
    
    private readonly AdjacencyListGraph _graphB;
    private readonly AdjacencyListGraph _graphA;

    public IsoMorphismAHU(AdjacencyListGraph graphA,AdjacencyListGraph graphB)
    {
        _graphA = graphA;
        _graphB = graphB;
    }
    
    private bool AreSameGraphs(Node startNodeA,Node startNodeB)
    {
        return Encode(_graphA,startNodeA).Equals(Encode(_graphB,startNodeB));
    }
    
    private string Encode(AdjacencyListGraph graph, Node node)
    {
        var labels = new List<string>();
        
        if (_visited.Add(node))
        {
            var neighbors = graph.GetAdjacentVertices(node);
           
            foreach (var neighbor in neighbors.OrderBy(x => x.Id))
            {
                labels.Add(Encode(graph,neighbor));
            }
        }

        var label = "";
        
        foreach (var l in labels)
        {
            label += l;
        }

        return "("+label+")";
    } 
}