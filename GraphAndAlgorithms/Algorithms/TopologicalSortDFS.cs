using GraphAndAlgorithms.Graph;
using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Algorithms;
// Improvement : Kahns algorithm.
// Find node that have no dependency (they are not children of any other node)
// Add them to order at beginning and remove them from graph
// Continue this process 
public class TopologicalSortDFS
{
    private readonly AdjacencyListGraph _graph;

    private readonly Node[] _allNodes;

    private readonly bool[] _visitedNodes;

    public TopologicalSortDFS(AdjacencyListGraph graph)
    {
        _graph = graph;
        _allNodes = _graph.Nodes.ToArray();
        OrderedNodes = new Node[_allNodes.Length];
        _visitedNodes = new bool[_allNodes.Length];

        var counter = _allNodes.Length - 1;

        Sort(_allNodes[0], ref counter);
    }

    public readonly Node[] OrderedNodes;

    private void Sort(Node currentNode, ref int counter)
    {
        DFS(currentNode, ref counter);

        if (counter > -1 && !_visitedNodes.All(x => x))
        {
            for (int i = 0; i < _visitedNodes.Length; i++)
            {
                if (!_visitedNodes[i])
                {
                    Sort(_allNodes[i], ref counter);
                }
            }
        }
    }

    private void DFS(Node currentNode, ref int counter)
    {
        foreach (var child in _graph.GetAdjacentVertices(currentNode).OrderBy(x => x.Id))
        {
            DFS(child, ref counter);
        }

        var index = Array.FindIndex(_allNodes, x => x.Equals(currentNode));

        if (!_visitedNodes[index])
        {
            _visitedNodes[index] = true;
            OrderedNodes[counter] = currentNode;
            counter--;
        }
    }
}