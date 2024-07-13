using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck-Counts-", menuName = "Game/Decks/By Counts")]
public class CardHolderCount : CardHolder
{
    [SerializeField]
    List<CardAndCount> counts;

    public override List<Card> GetCards()
    {
        return counts.ToList();
    }
}
