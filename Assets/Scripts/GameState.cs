using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState-", menuName = "Game System/Game State")]
public class GameState : ScriptableObject
{
    [SerializeField]
    string stateName;

    public string StateName { get => stateName; }
}
