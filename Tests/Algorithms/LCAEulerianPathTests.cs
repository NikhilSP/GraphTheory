using GraphAndAlgorithms.Algorithms;
using GraphAndAlgorithms.GraphBuilder;
using Shouldly;
using Xunit;

namespace Tests.Algorithms;

public class LCAEulerianPathTests
{
    [Fact]
    public void ShouldCalculateDistanceCorrectly()
    {
        var graphBuilder = new AdjacencyListGraphBuilder(isDirected:true);
        
        graphBuilder.AddEdge(0,1);
        graphBuilder.AddEdge(1,3);
        graphBuilder.AddEdge(0,2);
        graphBuilder.AddEdge(2,4);
        graphBuilder.AddEdge(2,5);
        graphBuilder.AddEdge(4,6);
       

        var graph = graphBuilder.Get();
        var start = graph.Edges.First().Source;
        start.Id.ShouldBe(0);
        
        var dijkstraShortestPath = new LCAEulerianPath(graph,start);
    }
}