using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;

public class DijkstraShortestPath
{
    private readonly AdjacencyListGraph _graph;
    
    private HashSet<Node> _visitedNodes;
    

    public DijkstraShortestPath(AdjacencyListGraph graph)
    {
        _graph = graph;
        NodesCostData = new();
        _visitedNodes = new();
    }
    
    public Dictionary<Node, (double Cost, Node PreviousNode)> NodesCostData;

    public void FindShortestPathToAllNodes(Node start)
    {
        NodesCostData.Add(start, (Cost: 0, PreviousNode: start));

        var currentNode = start;
        var currentCost = 0.0;
        
        while (true)
        {
            AddCostOfNeighborNodes(currentNode,currentCost);
            _visitedNodes.Add(currentNode);

            var unVisitedNodes =
                NodesCostData
                    .Where(x => !_visitedNodes.Contains(x.Key)).ToList();

            if (!unVisitedNodes.Any())
            {
                break;
            }

            var unVisitedNodeWithMinCost = unVisitedNodes.MinBy(x => x.Value.Cost);
            currentNode = unVisitedNodeWithMinCost.Key;
            currentCost = unVisitedNodeWithMinCost.Value.Cost;
        }
    }

    private void AddCostOfNeighborNodes(Node currentNode, double costToReachCurrentNode)
    {
        foreach (var edge in _graph.GetEdges(currentNode))
        {
            var costToReachTarget = edge.Weight + costToReachCurrentNode;
            
            if (NodesCostData.TryGetValue(edge.Target, out var nodeData))
            {
                if (nodeData.Cost > costToReachTarget)
                {
                    NodesCostData[edge.Target] = (Cost: costToReachTarget, PreviousNode: currentNode);
                }
            }
            else
            {
                NodesCostData.Add(edge.Target, (Cost: costToReachTarget, PreviousNode: currentNode));
            }
        }
    }
}