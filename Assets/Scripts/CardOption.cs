using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardOption-", menuName = "Game/Card Option")]
public class CardOption : ScriptableObject
{
    [SerializeField]
    string optionName;

    public string OptionName { get => optionName; }
}
