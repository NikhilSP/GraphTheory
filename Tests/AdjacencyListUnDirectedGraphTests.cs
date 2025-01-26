using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.GraphBuilder;
using GraphAndAlgorithms.Model;
using Shouldly;
using Xunit;

namespace Tests;

public class AdjacencyListUnDirectedGraphTests
{
    [Fact]
    public void ShouldAddEdge()
    {
        // Arrange
        var testedGraph = new AdjacencyListGraph();
        var edgeSource = new Node(1);
        var edgeTarget = new Node(2);

        var edge = new Edge(edgeSource, edgeTarget, 3);
        
        // Act
        testedGraph.AddEdge(edge);
        
        // Assert
        testedGraph.Edges.Count().ShouldBe(1);
        testedGraph.Nodes.Count().ShouldBe(2);
        
        testedGraph.Nodes.Contains(edgeSource).ShouldBeTrue();
        testedGraph.Nodes.Contains(edgeTarget).ShouldBeTrue();
    }


    [Fact]
    public void ShouldRemoveAllEdges()
    {
        // Arrange
        var graphBuilder = new AdjacencyListGraphBuilder();
        
        graphBuilder.AddEdge(1,2);
        graphBuilder.AddEdge(1,3);
        graphBuilder.AddEdge(1,4);
        graphBuilder.AddEdge(5,1);

        var testGraph = graphBuilder.Get();
        
        testGraph.Nodes.Count().ShouldBe(5);
        testGraph.Edges.Count().ShouldBe(4);

        testGraph.RemoveAllEdges(testGraph.Edges.First().Source);
        
        testGraph.Nodes.Count().ShouldBe(5);
        testGraph.Edges.Count().ShouldBe(0);
        
        testGraph.PruneNodes();
        
        testGraph.Nodes.Count().ShouldBe(0);
        testGraph.Edges.Count().ShouldBe(0);
    }

    private Edge GetEdgeFromIds(int sourceId, int targetId)
    {
        return new Edge(new Node(sourceId), new Node(targetId));
    }
    
}