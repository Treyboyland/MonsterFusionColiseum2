using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameColors-", menuName = "Game System/Game Colors")]
public class GameColors : ScriptableObject
{
    [SerializeField]
    Color movementHighlight;

    [SerializeField]
    Color summonHighlight;

    public Color SummonHighlight { get => summonHighlight; }
    public Color MovementHighlight { get => movementHighlight; }
}
