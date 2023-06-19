using UnityEngine;

public class Map : MonoBehaviour
{
    public const int W = 10; // largura
    public const int H = 20; // altura
    public static readonly Transform[,] Grid = new Transform[W, H];

    public static Vector2 RoundVec2(Vector2 vector2) // arredonda um vector2
    {
        return new Vector2(Mathf.Round(vector2.x),
            Mathf.Round(vector2.y));
    }

    public static bool IsInsideBorder(Vector2 pos) // verifica se a peça está dentro das bordas do mapa
    {
        return ((int) pos.x >= 0 &&
                (int) pos.x < W &&
                (int) pos.y >= 0);
    }

    private static void DeleteRow(int y) // deleta uma linha y
    {
        for (var x = 0; x < W; ++x)
        {
            Destroy(Grid[x, y].gameObject);
            Grid[x, y] = null;
        }
    }

    private static void DecreaseRow(int y) // desce as peças de uma linha y
    {
        for (var x = 0; x < W; ++x)
        {
            if (Grid[x, y] != null)
            {
                // Move one towards bottom
                Grid[x, y - 1] = Grid[x, y];
                Grid[x, y] = null;

                // Update Block position
                Grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    private static void DecreaseRowsAbove(int y) // desce as peças acima de uma linha y
    {
        for (var i = y; i < H; ++i)
            DecreaseRow(i);
    }

    public static void DeleteFullRows() // deleta todas linhas que estão completas
    {
        for (var y = 0; y < H; ++y)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                --y;
            }
        }
    }

    private static bool IsRowFull(int y) // verifica se a linha está completa
    {
        for (var x = 0; x < W; ++x)
            if (Grid[x, y] == null)
                return false;
        return true;
    }
}