using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

public class BellmanFord
{
    public BellmanFord(AdjacencyListGraph graph)
    {
        var graph1 = graph;

        var nodesCostData = new Dictionary<Node, double>();
        
        foreach (var node in graph1.Nodes)
        {
            nodesCostData.Add(node,double.MaxValue);
        }

        nodesCostData[nodesCostData.First().Key] = 0;
        
        IsNegativeCycleBellmanFord(graph1, nodesCostData);
    }
    
    static bool IsNegativeCycleBellmanFord(AdjacencyListGraph graph, Dictionary<Node, double> nodesCostData)
    {
        var nodesCount = graph.Nodes.Count();

        for (var i = 1; i <= nodesCount - 1; i++)
        {
            foreach (var edge in graph.Edges)
            {
                var u = edge.Source;
                var v = edge.Target;
                var weight = edge.Weight;

                if (nodesCostData[u] + weight < nodesCostData[v])
                    nodesCostData[v] = nodesCostData[u] + weight;
            }
        }

        foreach (var edge in graph.Edges)
        {
            var u = edge.Source;
            var v = edge.Target;
            var weight = edge.Weight;

            if (nodesCostData[u] + weight < nodesCostData[v])
                return true;
        }

        return false;
    }
}