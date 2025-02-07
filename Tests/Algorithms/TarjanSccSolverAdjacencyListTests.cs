using GraphAndAlgorithms.Algorithms;
using GraphAndAlgorithms.GraphBuilder;
using Xunit;

namespace Tests.Algorithms;

public class TarjanSccSolverAdjacencyListTests
{
    [Fact]
    public void Test()
    {
        var graphBuilder = new AdjacencyListGraphBuilder(isDirected:true);
        
        graphBuilder.AddEdge(5,2);
        graphBuilder.AddEdge(5,4);
        graphBuilder.AddEdge(5,7);
        
        graphBuilder.AddEdge(4,3);
        
        graphBuilder.AddEdge(3,7);
        
        graphBuilder.AddEdge(7,4);
        
        graphBuilder.AddEdge(0,6);
        graphBuilder.AddEdge(0,2);
        
        graphBuilder.AddEdge(6,0);
        graphBuilder.AddEdge(6,1);
        
        graphBuilder.AddEdge(2,1);
        
        graphBuilder.AddEdge(1,4);
        graphBuilder.AddEdge(1,5);

        var _graph = graphBuilder.Get();

        var scc = new TarjanStronglyConnectedComponentsSimplified(_graph);
    }
}