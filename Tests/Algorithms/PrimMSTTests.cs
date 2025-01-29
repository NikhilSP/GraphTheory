using GraphAndAlgorithms.Algorithms;
using GraphAndAlgorithms.GraphBuilder;
using Shouldly;
using Xunit;

namespace Tests.Algorithms;

public class PrimMSTTests
{
    [Fact]
    public void ShouldFindMSTCorrectly()
    {
        var graphBuilder = new AdjacencyListGraphBuilder();
        
        graphBuilder.AddEdge(0,1, weight:10);
        graphBuilder.AddEdge(0,2,weight:6);
        graphBuilder.AddEdge(0,3, weight:5);
        graphBuilder.AddEdge(1,3,weight:15);
        graphBuilder.AddEdge(2,3,weight:4);

        var graph = graphBuilder.Get();
        var edges = graph.Edges.ToArray();
        
        var primMST = new PrimMST(graph);
        var foundMSTEdges = primMST.FindMST(edges[0].Source);
        
        edges.Length.ShouldBe(5);
        foundMSTEdges.Count.ShouldBe(3);
        
        foundMSTEdges[0].ShouldBe(edges[4]);
        foundMSTEdges[1].ShouldBe(edges[2]);
        foundMSTEdges[1].ShouldBe(edges[0]);
    }
}