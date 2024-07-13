using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHighlight : MonoBehaviour
{
    [SerializeField]
    Vector2Int gridPosition;

    [SerializeField]
    SpriteRenderer highlightSprite;


    public Vector2Int GridPosition { get => gridPosition; set { gridPosition = value; } }

    public Color Color { get => highlightSprite.color; set => highlightSprite.color = value; }
}
