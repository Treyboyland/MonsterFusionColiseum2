using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameCardsInPlay : MonoBehaviour
{
    [Tooltip("True if this should grab game cards that are in the scene to start")]
    [SerializeField]
    bool isDebug;

    [SerializeField]
    GameField field;

    [SerializeField]
    List<GameCard> cardsInPlay;

    // Start is called before the first frame update
    void Start()
    {
        if (isDebug)
        {
            foreach (var card in cardsInPlay)
            {
                MoveGameCardToPosition(card, card.CurrentGridPosition);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool HasCardAtPosition(Vector2Int pos)
    {
        return cardsInPlay.Where(x => x.CurrentGridPosition == pos).FirstOrDefault() != default(GameCard);
    }

    public GameCard GetGameCardAtPosition(Vector2Int pos)
    {
        return cardsInPlay.Where(x => x.CurrentGridPosition == pos).First();
    }

    public void MoveGameCardToPosition(GameCard gameCard, Vector2Int pos)
    {
        gameCard.SetGridPosition(pos);
        gameCard.transform.position = field.GetTileAtPosition(pos);
    }
}
