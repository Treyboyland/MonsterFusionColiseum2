using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain-", menuName = "Game/Terrain")]
public class TerrainType : ScriptableObject
{
    [SerializeField]
    private string terrainName;

    [SerializeField]
    private Sprite terrainSprite;

    [SerializeField]
    private Color terrainColor;

    [SerializeField]
    private List<Element> boostedElements;

    [SerializeField]
    private List<Element> weakenedElements;

    public string TerrainName { get => terrainName; }
    public Sprite TerrainSprite { get => terrainSprite; }
    public Color TerrainColor { get => terrainColor; }
    public List<Element> BoostedElements { get => boostedElements; }
    public List<Element> WeakenedElements { get => weakenedElements; }
}
