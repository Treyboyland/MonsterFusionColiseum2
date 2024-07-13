using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability-", menuName = "Game/Card/Ability")]
public abstract class Ability : ScriptableObject
{
    [SerializeField]
    string abilityName;

    [SerializeField]
    string description;

    [SerializeField]
    bool isPassive;

    [SerializeField]
    AreaOfEffect areaOfEffect;

    [SerializeField]
    bool targetAllies;

    [SerializeField]
    bool targetSelf;

    [SerializeField]
    GameState passiveTrigger;

    public string AbilityName { get => abilityName; }
    public string Description { get => description; }
    public bool IsPassive { get => isPassive; }
    public AreaOfEffect AreaOfEffect { get => areaOfEffect; }
    public bool TargetAllies { get => targetAllies; }
    public bool TargetSelf { get => targetSelf; }
    public GameState PassiveTrigger { get => passiveTrigger; }
}
