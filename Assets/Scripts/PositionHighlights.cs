using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionHighlights : MonoBehaviour
{
    [SerializeField]
    PositionHighlight highlightPrefab;

    [SerializeField]
    GameField gameField;

    [SerializeField]
    CardTypeCompare cardTypeCompare;

    [SerializeField]
    GameColors colors;

    ObjectPool<PositionHighlight> pool;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        pool = new ObjectPool<PositionHighlight>(highlightPrefab);
    }

    public void DisableHighlight()
    {
        pool.DisableAll();
    }

    public bool IsValidPosition(Vector2Int position)
    {
        return pool.GetActiveObjects().Where(x => x.GridPosition == position).ToList().Count != 0;
    }

    public void ShowValidMovementPositions(GameCard card)
    {
        var potentialPositions = card.GetMovementPositions().Where(x => gameField.IsValidPosition(x)).ToList();
        foreach (var position in potentialPositions)
        {
            var highlight = pool.GetItem();
            highlight.Color = colors.MovementHighlight;
            highlight.GridPosition = position;
            highlight.transform.SetParent(transform);
            highlight.transform.position = gameField.GetTileAtPosition(position);
            highlight.gameObject.SetActive(true);
        }
    }

    public void ShowValidSummonPositions(GameCard card)
    {
        var data = card.CurrentCardData;
        if (cardTypeCompare.IsLeader(data.CardType))
        {
            var leaderCard = (LeaderCard)data;
            var potentialPositions = leaderCard.SummonRange.GetPositions(card.CurrentGridPosition).Where(x => gameField.IsValidPosition(x));
            foreach (var position in potentialPositions)
            {
                var highlight = pool.GetItem();
                highlight.Color = colors.SummonHighlight;
                highlight.GridPosition = position;
                highlight.transform.SetParent(transform);
                highlight.transform.position = gameField.GetTileAtPosition(position);
                highlight.gameObject.SetActive(true);
            }
        }
    }
}
