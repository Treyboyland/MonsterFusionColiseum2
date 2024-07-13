using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [Header("Cursor")]
    [SerializeField]
    TileCursor cursor;

    [Header("Config stuff")]

    [SerializeField]
    FieldTile tilePrefab;

    [SerializeField]
    Vector2 tileDimensions;

    [SerializeField]
    float offset;

    [SerializeField]
    bool useDebugGrid;

    [SerializeField]
    DebugGrid debugGrid;

    FieldTile[,] grid;

    ObjectPool<FieldTile> pool;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        pool = new ObjectPool<FieldTile>(tilePrefab);
        if (useDebugGrid)
        {
            CreateTiles(debugGrid.GetGrid());
        }
    }

    Vector3 GetPosition(int x, int y)
    {
        return new Vector2(x * tileDimensions.x + offset * x, y * tileDimensions.y + offset * y);
    }


    public void CreateTiles(TerrainType[,] terrain)
    {
        pool.DisableAll();
        grid = new FieldTile[terrain.GetLength(0), terrain.GetLength(1)];

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                var tile = pool.GetItem();
                tile.gameObject.name = $"Tile [{x},{y}]";
                tile.transform.SetParent(transform);
                tile.transform.position = GetPosition(x, y);
                tile.SetData(terrain[x, y]);
                tile.gameObject.SetActive(true);
                grid[x, y] = tile;
            }
        }

        cursor.SetPosition(new Vector2Int(grid.GetLength(0) / 2, grid.GetLength(1) / 2));
    }

    public Vector3 GetTileAtPosition(Vector2Int currentPosition)
    {
        currentPosition = ClampGridPosition(currentPosition);
        return grid[currentPosition.x, currentPosition.y].transform.position;
    }

    public Vector2Int ClampGridPosition(Vector2Int currentPosition)
    {
        currentPosition.Clamp(Vector2Int.zero, new Vector2Int(grid.GetLength(0) - 1, grid.GetLength(1) - 1));
        return currentPosition;
    }

    public bool IsValidPosition(Vector2Int position)
    {
        return position.x >= 0 && position.x < grid.GetLength(0) && position.y >= 0 && position.y < grid.GetLength(1);
    }
}
