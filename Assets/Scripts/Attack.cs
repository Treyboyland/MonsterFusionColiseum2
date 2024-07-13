using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-", menuName = "Game/Card/Attack")]
public class Attack : ScriptableObject
{
    [SerializeField]
    protected string attackName;

    [TextArea]
    [SerializeField]
    protected string description;

    [Tooltip("Amount of damage or heal")]
    [SerializeField]
    int amount;

    [SerializeField]
    int manaRequirement;

    [SerializeField]
    bool isHeal;

    [SerializeField]
    bool targetAllies;

    [SerializeField]
    bool targetSelf;

    [SerializeField]
    bool targetEveryone;

    [SerializeField]
    bool affectsAllWithinRange;

    [SerializeField]
    AreaOfEffect areaOfEffect;

    public string AttackName { get => attackName; }
    public string Description { get => description; }
    public bool TargetSelf { get => targetSelf; }
    public bool TargetAllies { get => targetAllies; }
    public int ManaRequirement { get => manaRequirement; }
    public bool AffectsAllWithinRange { get => affectsAllWithinRange; }
    public bool TargetEveryone { get => targetEveryone; }

    public bool CanUseAttack(int givenMana)
    {
        return givenMana >= manaRequirement;
    }
}
