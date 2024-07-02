using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card-Spell-AOE-", menuName = "Game/Card/Spell - Area of Effect")]
public class SpellCardAreaOfEffect : SpellCard
{
    [SerializeField]
    bool isSelf;

    [SerializeField]
    bool isHeal;

    [SerializeField]
    int amount;

    public bool IsSelf { get => isSelf; }
    public bool IsHeal { get => isHeal; }
    public int Amount { get => amount; }
}
