using GraphAndAlgorithms.Algorithms;
using GraphAndAlgorithms.GraphBuilder;
using Shouldly;
using Xunit;

namespace Tests.Algorithms;

public class DijkstraTests
{
    [Fact]
    public void ShouldCalculateDistanceCorrectly()
    {
        var graphBuilder = new AdjacencyListGraphBuilder();
        
        graphBuilder.AddEdge(1,2, weight:2);
        graphBuilder.AddEdge(1,4,weight:8);
        graphBuilder.AddEdge(2,4, weight:5);
        graphBuilder.AddEdge(2,5,weight:6);
        graphBuilder.AddEdge(4,5,weight:3);
        graphBuilder.AddEdge(4,6,weight:2);
        graphBuilder.AddEdge(5,6,weight:1);
        graphBuilder.AddEdge(6,3,weight:3);
        graphBuilder.AddEdge(5,3,weight:9);

        var _graph = graphBuilder.Get();

        var dijkstraShortestPath = new DijkstraShortestPath(_graph);
        var start = _graph.Edges.First().Source;
       
        start.Id.ShouldBe(1);
        
        dijkstraShortestPath.FindShortestPathToAllNodes(start);
    }
}