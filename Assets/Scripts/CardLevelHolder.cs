using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CardLevels-", menuName = "Game System/Card Levels")]
public class CardLevelHolder : ScriptableObject
{
    [Tooltip("True if we should use the scriptable object list for level counts instead")]
    [SerializeField]
    bool useScriptableObject;

    [SerializeField]
    List<CardAndLevel> cardAndLevel;

    [SerializeField]
    List<CardAndLevelSO> cardAndLevelSO;

    public bool UseScriptableObject { get => useScriptableObject; set => useScriptableObject = value; }

    /// <summary>
    /// For setting up levels using Card Name references. Probably only useful for saving
    /// </summary>
    /// <value></value>
    public List<CardAndLevel> CardAndLevel { get => cardAndLevel; set => cardAndLevel = value; }

    /// <summary>
    /// For setting up levels using Card references. Probably should only be used in editor
    /// </summary>
    /// <value></value>
    public List<CardAndLevelSO> CardAndLevelSO { get => cardAndLevelSO; set => cardAndLevelSO = value; }

    public int GetLevelForCard(Card card)
    {
        return GetLevelForCard(card.CardName);
    }

    public int GetLevelForCard(string cardName)
    {
        if (useScriptableObject)
        {
            var found = cardAndLevelSO.Where(x => x.Card.CardName == cardName).FirstOrDefault();
            return found.Card != default ? found.Level : 0;
        }
        else
        {
            var found = cardAndLevel.Where(x => x.CardName == cardName).FirstOrDefault();
            return found.CardName != default ? found.Level : 0;
        }
    }
}
