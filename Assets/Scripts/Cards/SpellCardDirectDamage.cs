using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card-Spell-Direct-", menuName = "Game/Card/Spell - Direct Dmg")]
public class SpellCardDirectDamage : SpellCard
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
