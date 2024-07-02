using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterAttribute-", menuName = "Game/Monster Attribute")]
public class MonsterAttribute : ScriptableObject
{
    [SerializeField]
    string attributeName;

    [SerializeField]
    string description;

    [SerializeField]
    Sprite sprite;

    public string Description { get => description; }
    public string AttributeName { get => attributeName; }
    public Sprite Sprite { get => sprite; }
}
