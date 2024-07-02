using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderCount : CardHolder
{
    [SerializeField]
    List<CardAndCount> counts;

    public override List<Card> GetCards()
    {
        return counts.ToList();
    }
}
