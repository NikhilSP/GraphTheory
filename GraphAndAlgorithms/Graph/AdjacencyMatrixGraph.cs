using GraphAndAlgorithms.Model;

namespace GraphAndAlgorithms.Graph;

public class AdjacencyMatrixGraph : GraphBase
{
    private readonly int[,] _adjMatrix;

    public AdjacencyMatrixGraph(int verticesCount, bool isDirected = false) : base(verticesCount, isDirected)
    {
        _adjMatrix = new int[verticesCount, verticesCount];
        GenerateEmptyMatrix(verticesCount);
    }

    private void GenerateEmptyMatrix(int verticesCount)
    {
        for (int row = 0; row < verticesCount; row++)
        {
            for (int col = 0; col < verticesCount; col++)
            {
                _adjMatrix[row, col] = 0;
            }
        }
    }

    public override void AddEdge(Node node1,Node node2, int weight = 1)
    {
        ThrowExceptionWhenInValid(node1);
        ThrowExceptionWhenInValid(node2);

        if (weight < 1)
        {
            throw new Exception("Weight cannot be less than 1");
        }

        _adjMatrix[node1.Id, node2.Id] = weight;

        if (!Directed)
        {
            _adjMatrix[node2.Id,node1.Id] = weight;
        }
    }

    public override IEnumerable<int> GetAdjacentVertices(Node node)
    {
        ThrowExceptionWhenInValid(node);

        return
            Enumerable.Range(0, NumVertices)
                .Select(j => _adjMatrix[node.Id, j])
                .Where(x => x != 0);
    }

    public override int GetEdgeWeight(Node node1, Node node2)
    {
        ThrowExceptionWhenInValid(node1);
        ThrowExceptionWhenInValid(node2);

        return _adjMatrix[node1.Id, node2.Id];
    }

    public override void RemoveEdge(Node node1, Node node2)
    {
        ThrowExceptionWhenInValid(node1);
        ThrowExceptionWhenInValid(node2);

        _adjMatrix[node1.Id, node2.Id] = 0;

        if (!Directed)
        {
            _adjMatrix[node2.Id, node1.Id] = 0;
        }
    }

    public override void RemoveAllEdges(Node node)
    {
        ThrowExceptionWhenInValid(node);

        for (int i = 0; i < NumVertices; i++)
        {
            _adjMatrix[node.Id, i] = 0;

            if (!Directed)
            {
                _adjMatrix[i, node.Id] = 0;
            }
        }
    }

    public override void Clear()
    {
        GenerateEmptyMatrix(NumVertices);
    }

    private void ThrowExceptionWhenInValid(Node node)
    {
        if (node.Id < 0 || node.Id >= NumVertices)
        {
            throw new Exception("node out of range");
        }
    }
}