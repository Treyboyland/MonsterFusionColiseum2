using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderSingle : CardHolder
{
    [SerializeField]
    List<Card> cards;

    public override List<Card> GetCards()
    {
        return cards;
    }
}
