using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FusionTable-", menuName = "Game System/Fusion Table")]
public class FusionTable : ScriptableObject
{
    [Serializable]
    public struct ElementMatch
    {
        public Element FirstElement;
        public Element SecondElement;
        public Element FusedElement;

        public bool HasMatch(MonsterCard first, MonsterCard second)
        {
            return (first.Element == FirstElement && second.Element == SecondElement) ||
            (first.Element == SecondElement && second.Element == FirstElement);
        }
    }

    [Serializable]
    public struct MonsterMatch
    {
        public MonsterCard FirstMonster;
        public MonsterCard SecondMonster;
        public MonsterCard FusedMonster;

        public bool HasMatch(MonsterCard first, MonsterCard second)
        {
            //Because we duplicate in fusion, we can't match ScriptableObjects directly, so use card name
            return (first.CardName == FirstMonster.CardName && second.CardName == SecondMonster.CardName) ||
            (first.CardName == SecondMonster.CardName && second.CardName == FirstMonster.CardName);
        }
    }

    public List<ElementMatch> elementMatches;

    public List<MonsterMatch> monsterMatches;

    public bool HasElementMatch(MonsterCard first, MonsterCard second)
    {
        return elementMatches.Any(x => x.HasMatch(first, second));
    }

    public Element GetMatchingElement(MonsterCard first, MonsterCard second)
    {
        return elementMatches.Where(x => x.HasMatch(first, second)).First().FusedElement;
    }

    public bool HasMonsterMatch(MonsterCard first, MonsterCard second)
    {
        return monsterMatches.Any(x => x.HasMatch(first, second));
    }

    public MonsterCard GetMatchingMonster(MonsterCard first, MonsterCard second)
    {
        return monsterMatches.Where(x => x.HasMatch(first, second)).First().FusedMonster;
    }
}
