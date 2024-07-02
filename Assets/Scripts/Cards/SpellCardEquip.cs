using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card-Spell-Equip-", menuName = "Game/Card/Spell - Equip")]
public class SpellCardEquip : Card
{
    [SerializeField]
    bool isHealthBoost;

    [SerializeField]
    bool isAttackBoost;

    [SerializeField]
    bool isElementBoost;

    [SerializeField]
    int boostAmount;

    [SerializeField]
    Element elementBoosted;

    public bool IsHealthBoost { get => isHealthBoost; }
    public bool IsAttackBoost { get => isAttackBoost; }
    public bool IsElementBoost { get => isElementBoost; }
    public int BoostAmount { get => boostAmount; }
    public Element ElementBoosted { get => elementBoosted; }
}
