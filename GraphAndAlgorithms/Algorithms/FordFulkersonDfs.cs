using GraphAndAlgorithms.Model;
using static System.Math;

namespace GraphAndAlgorithms.Algorithms;

public class FordFulkersonDfs : NetworkFlowSolverBase
{
    public FordFulkersonDfs(int numberOfNodes, int sourceIndex, int sinkIndex) : base(numberOfNodes, sourceIndex,
        sinkIndex)
    {
    }

    public override void Solve()
    {
        // Find max flow by adding all augmenting path flows.
        for (long f = Dfs(SourceIndex, Inf); f != 0; f = Dfs(SourceIndex, Inf))
        {
            MarkAllNodesAsUnvisited();
            MaxFlow += f;
        }

        // Find min cut.
        for (int i = 0; i < NumberOfNodes; i++)
        {
            if (Visited(i))
            {
                MinCut[i] = true;
            }
        }
    }

    private long Dfs(int node, long flow)
    {
        // At sink node, return augmented path flow.
        if (node == SinkIndex)
        {
            return flow;
        }

        List<NetworkEdge> edges = Graph[node];
        Visit(node);

        foreach (NetworkEdge edge in edges)
        {
            long rcap = edge.RemainingCapacity();
            if (rcap > 0 && !Visited(edge.To))
            {
                long bottleNeck = Dfs(edge.To, Min(flow, rcap));

                // Augment flow with bottle neck value
                if (bottleNeck > 0)
                {
                    edge.Augment(bottleNeck);
                    return bottleNeck;
                }
            }
        }

        return 0;
    }
}

