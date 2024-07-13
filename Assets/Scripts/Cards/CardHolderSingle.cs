using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck-Singles-", menuName = "Game/Decks/By Singles")]
public class CardHolderSingle : CardHolder
{
    [SerializeField]
    List<Card> cards;

    public override List<Card> GetCards()
    {
        return cards;
    }
}
