using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardType-", menuName = "Game/Card Type")]
public class CardType : ScriptableObject
{
    [SerializeField]
    protected string typeName;

    [SerializeField]
    protected string description;

    public string Description { get => description; }
    public string TypeName { get => typeName; }
}
