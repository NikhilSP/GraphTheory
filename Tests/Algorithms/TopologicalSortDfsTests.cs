using GraphAndAlgorithms.Algorithms;
using GraphAndAlgorithms.GraphBuilder;
using Shouldly;
using Xunit;

namespace Tests.Algorithms;

public class TopologicalSortDfsTests
{
    [Fact]
    public void ShouldCalculateDistanceCorrectly()
    {
        var graphBuilder = new AdjacencyListGraphBuilder(isDirected:true);
        
        graphBuilder.AddEdge(1,3);
        graphBuilder.AddEdge(1,2);
        graphBuilder.AddEdge(3,2);
        graphBuilder.AddEdge(2,4);
        graphBuilder.AddEdge(2,5);
        graphBuilder.AddEdge(4,6);
        graphBuilder.AddEdge(6,5);
       

        var graph = graphBuilder.Get();
        var start = graph.Edges.First().Source;
        start.Id.ShouldBe(1);
        
        var topologicalSortDFS = new TopologicalSortDFS(graph);
    }
}