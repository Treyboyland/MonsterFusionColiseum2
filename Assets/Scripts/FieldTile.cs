using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer fieldSprite;

    TerrainType currentType;

    public void SetData(TerrainType terrainType)
    {
        currentType = terrainType;
        fieldSprite.sprite = terrainType.TerrainSprite;
        fieldSprite.color = terrainType.TerrainColor;
    }
}
