using GraphAndAlgorithms.Algorithms;
using GraphAndAlgorithms.Model;
using Shouldly;
using Xunit;

namespace Tests.Algorithms;

public class FordFulkersonDfsTests
{
    // Testing graph from:
    // http://crypto.cs.mcgill.ca/~crepeau/COMP251/KeyNoteSlides/07demo-maxflowCS-C.pdf
    [Fact]
    public void Test_3()
    {
        var numberOfNodes= 6;
        var sourceIndex = 0;
        var sinkIndex = 1;

        var testedSolver = new FordFulkersonDfs(numberOfNodes, sourceIndex, sinkIndex);

        // Source edges
        testedSolver.AddEdge(sourceIndex, 2, 10);
        testedSolver.AddEdge(sourceIndex, 3, 10);

        // Sink edges
        testedSolver.AddEdge(4, sinkIndex, 10);
        testedSolver.AddEdge(5, sinkIndex, 10);

        // Middle edges
        testedSolver.AddEdge(2, 3, 2);
        testedSolver.AddEdge(2, 4, 4);
        testedSolver.AddEdge(2, 5, 8);
        testedSolver.AddEdge(3, 5, 9);
        testedSolver.AddEdge(5, 4, 6);

        testedSolver.Solve();
        
        testedSolver.GetMaxFlow().ShouldBe(19);
    }
    
    [Fact]
    public void Test_1()
    {
        var numberOfNodes= 4;
        var sourceIndex = 0;
        var sinkIndex = 3;

        var testedSolver = new FordFulkersonDfs(numberOfNodes, sourceIndex, sinkIndex);

        testedSolver.AddEdge(sourceIndex, 1, 10);
        testedSolver.AddEdge(sourceIndex, 2, 10);

        testedSolver.AddEdge(2, 1, 5);
        testedSolver.AddEdge(1, sinkIndex, 10);
        testedSolver.AddEdge(2, sinkIndex, 10);

        testedSolver.Solve();
        
        testedSolver.GetMaxFlow().ShouldBe(19);
    }
}