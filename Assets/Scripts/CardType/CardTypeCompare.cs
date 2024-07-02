using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardTypeComparator", menuName = "Game/Card Type Comparator", order = 0)]
public class CardTypeCompare : ScriptableObject
{
    [SerializeField]
    CardType monster, leader, spell, trap;

    public bool IsMonster(CardType givenType)
    {
        return givenType == monster;
    }

    public bool IsLeader(CardType givenType)
    {
        return givenType == leader;
    }

    public bool IsSpell(CardType givenType)
    {
        return givenType == spell;
    }

    public bool IsTrap(CardType givenType)
    {
        return givenType == trap;
    }
}
