using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebugGrid-", menuName = "Game/Debug Grid")]
public class DebugGrid : ScriptableObject
{
    [SerializeField]
    Vector2Int dimensions;

    [SerializeField]
    List<TerrainType> alternations;

    public TerrainType[,] GetGrid()
    {
        int i = 0;
        TerrainType[,] terrain = new TerrainType[dimensions.x, dimensions.y];
        for (int x = 0; x < dimensions.x; x++)
        {
            for (int y = 0; y < dimensions.y; y++)
            {
                terrain[x, y] = alternations[i];
                i = (i + 1) % alternations.Count;
            }
        }

        return terrain;
    }
}
