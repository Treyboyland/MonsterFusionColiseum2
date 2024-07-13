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
    bool isManaBoost;

    [SerializeField]
    int boostAmount;

    [SerializeField]
    List<SpellLevelBoost> levelBoosts;

    public bool IsHealthBoost { get => isHealthBoost; }
    public bool IsAttackBoost { get => isAttackBoost; }
    public bool IsManaBoost { get => isManaBoost; }

    public struct SpellLevelBoost
    {
        public int BoostAmount;

        /// <summary>
        /// If false, will assume this is a multiplicative boost
        /// </summary>
        public bool IsAddiditive;
        public int LevelThisIsIncluded;
    }

    public int GetBoostedAmount(int level)
    {
        int result = boostAmount;

        foreach (var levelBoost in levelBoosts)
        {
            if (level >= levelBoost.LevelThisIsIncluded)
            {
                result = levelBoost.IsAddiditive ? result + levelBoost.BoostAmount : result * levelBoost.BoostAmount;
            }
        }

        return result;
    }
}
