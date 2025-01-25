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

    public override void AddEdge(int v1, int v2, int weight = 1)
    {
        if (!IsValidNode(v1) || !IsValidNode(v2))
        {
            throw new Exception("Vertices out of range");
        }

        if (weight < 1)
        {
            throw new Exception("Weight cannot be less than 1");
        }

        _adjMatrix[v1, v2] = weight;

        if (!Directed)
        {
            _adjMatrix[v2, v1] = weight;
        }
    }

    public override IEnumerable<int> GetAdjacentVertices(int v)
    {
        if (!IsValidNode(v))
        {
            throw new Exception("Vertex out of range");
        }

        return
            Enumerable.Range(0, NumVertices)
                .Select(j => _adjMatrix[v, j])
                .Where(x => x != 0);
    }

    public override int GetEdgeWeight(int v1, int v2)
    {
        if (IsValidNode(v1) && IsValidNode(v2))
        {
            return _adjMatrix[v1, v2];
        }
        throw new Exception("Vertices out of range");
    }

    public override void RemoveEdge(int v1, int v2)
    {
        if (IsValidNode(v1) && IsValidNode(v2))
        {
            _adjMatrix[v1, v2] = 0;

            if (Directed)
            {
                _adjMatrix[v2, v1] = 0;
            }
        }
        else
        {
            throw new Exception("Vertices out of range");
        }
    }

    public override void RemoveAllEdges(int v)
    {
        if (IsValidNode(v))
        {
            for (int i = 0; i < NumVertices; i++)
            {
                _adjMatrix[v, i] = 0;

                if (Directed)
                {
                    _adjMatrix[i, v] = 0;
                }
            }
        }
        else
        {
            throw new Exception("Vertex out of range");
        }
    }

    public override void Clear()
    {
        GenerateEmptyMatrix(NumVertices);
    }

    private bool IsValidNode(int v)
    {
        return v > 0 && v < NumVertices;
    }
}