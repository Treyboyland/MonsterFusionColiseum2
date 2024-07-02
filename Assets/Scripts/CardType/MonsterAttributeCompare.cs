using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterAttributeComparator", menuName = "Game/Monster Attribute Comparator")]
public class MonsterAttributeCompare : ScriptableObject
{
    [SerializeField]
    MonsterAttribute golem, crystal, humanoid, beast, machine, slime;

    public bool IsGolem(MonsterAttribute givenAttribute)
    {
        return givenAttribute == golem;
    }

    public bool IsCrystal(MonsterAttribute givenAttribute)
    {
        return givenAttribute == crystal;
    }

    public bool IsHumanoid(MonsterAttribute givenAttribute)
    {
        return givenAttribute == humanoid;
    }

    public bool IsBeast(MonsterAttribute givenAttribute)
    {
        return givenAttribute == beast;
    }

    public bool IsMachine(MonsterAttribute givenAttribute)
    {
        return givenAttribute == machine;
    }

    public bool IsSlime(MonsterAttribute givenAttribute)
    {
        return givenAttribute == slime;
    }
}
