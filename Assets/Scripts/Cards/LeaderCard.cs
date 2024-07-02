using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card-Leader", menuName = "Game/Card/Leader")]
public class LeaderCard : Card
{
    [SerializeField]
    AreaOfEffect summonRange;

    public AreaOfEffect SummonRange { get => summonRange; set => summonRange = value; }
}
