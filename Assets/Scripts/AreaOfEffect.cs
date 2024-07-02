using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AreaOfEffect-", menuName = "Game/Area of Effect")]
public class AreaOfEffect : ScriptableObject
{
    [SerializeField]
    int distance;

    [SerializeField]
    bool distanceIsPerDimension;

    [SerializeField]
    bool excludeOrigin;

    public List<Vector2Int> GetPositions(Vector2Int origin)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        for (int x = -distance; x <= distance; x++)
        {
            for (int y = -distance; y <= distance; y++)
            {
                if (excludeOrigin && x == 0 && y == 0)
                {
                    continue;
                }
                int absX = Mathf.Abs(x);
                int absY = Mathf.Abs(y);

                if (distanceIsPerDimension)
                {
                    if (absX <= distance && absY <= distance)
                    {
                        locations.Add(origin + new Vector2Int(x, y));
                    }
                }
                else
                {
                    if (absX + absY <= distance)
                    {
                        locations.Add(origin + new Vector2Int(x, y));
                    }
                }
            }
        }

        return locations;
    }
}
