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
    GameCard cardPrefab;

    [SerializeField]
    GameField field;

    [SerializeField]
    List<GameCard> cardsInPlay;

    ObjectPool<GameCard> objectPool;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        objectPool = new ObjectPool<GameCard>(cardPrefab);
    }

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

    public void CreateCard(FusionResolver.FusionData data, Vector2Int position, Player owningPlayer)
    {
        if (HasCardAtPosition(position))
        {
            var newCard = GetGameCardAtPosition(position);
            newCard.SetData(data);
        }
        else
        {
            var newCard = objectPool.GetItem();
            newCard.SetData(data);
            newCard.Player = owningPlayer;
            newCard.transform.SetParent(transform);
            MoveGameCardToPosition(newCard, position);
            newCard.gameObject.SetActive(true);
            cardsInPlay.Add(newCard);
        }
    }

    public void RemoveFromPlay(GameCard card)
    {
        card.gameObject.SetActive(false);
        cardsInPlay.Remove(card);
    }
}
