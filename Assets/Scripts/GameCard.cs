using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameCard : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    GameObject selectedPip;

    [SerializeField]
    Vector2Int currentGridPosition;

    [SerializeField]
    AreaOfEffect faceDownMovementRange;

    [SerializeField]
    bool faceUp;

    [SerializeField]
    bool isSelected;

    [SerializeField]
    CardOwner cardOwner;

    [SerializeField]
    Card currentCardData;

    [SerializeField]
    CardTypeCompare cardTypeCompare;

    [SerializeField]
    List<SpellCardEquip> equippedCards;

    public bool FaceUp { get => faceUp; }

    public bool IsSelected
    {
        get => isSelected; set
        {
            isSelected = value;
            selectedPip.SetActive(isSelected);
        }
    }

    public Vector2Int CurrentGridPosition { get => currentGridPosition; }
    public CardOwner CardOwner { get => cardOwner; set => cardOwner = value; }

    public Card CurrentCardData { get => currentCardData; }

    public bool CanHaveEquips { get => cardTypeCompare.IsMonster(currentCardData.CardType); }

    public List<SpellCardEquip> EquippedCards { get => equippedCards; }

    int currentHealth;

    public int MaxHealth
    {
        get
        {
            if (cardTypeCompare.IsMonster(currentCardData.CardType))
            {
                return ((MonsterCard)CurrentCardData).MaxHealth;
            }
            return 0;
        }
    }

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        SetData(currentCardData);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetData(Card card)
    {
        currentCardData = card;
        gameObject.name = "GameCard-" + currentCardData.CardName;
        IsSelected = false;
        if (cardTypeCompare.IsLeader(currentCardData.CardType))
        {
            faceUp = true;
        }
        if (cardTypeCompare.IsMonster(currentCardData.CardType))
        {
            currentHealth = ((MonsterCard)currentCardData).MaxHealth;
        }
        SetAppropriateSprite();
    }

    void SetAppropriateSprite()
    {
        spriteRenderer.sprite = FaceUp ? currentCardData.FaceUpSprite : currentCardData.FaceDownSprite;
    }

    public void SetGridPosition(Vector2Int pos)
    {
        currentGridPosition = pos;
    }

    public void Revert()
    {

    }

    public void Flip()
    {
        faceUp = cardTypeCompare.IsLeader(currentCardData.CardType) ? true : !faceUp;
        SetAppropriateSprite();
    }

    List<Vector2Int> GetSpotsWithinDistance(int distance)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        for (int x = -distance; x <= distance; x++)
        {
            for (int y = -distance; y <= distance; y++)
            {
                int absX = Mathf.Abs(x);
                int absY = Mathf.Abs(y);
                if (absX + absY <= distance)
                {
                    Vector2Int offset = new Vector2Int(x, y);
                    locations.Add(currentGridPosition + offset);
                }
            }
        }

        return locations;
    }

    public List<Vector2Int> GetMovementPositions()
    {
        if (faceUp)
        {
            //TODO: special abilities...terrain movement bonus?
            return faceDownMovementRange.GetPositions(currentGridPosition);
        }
        else
        {
            return faceDownMovementRange.GetPositions(currentGridPosition);
        }
    }

    public string GetEquippedElements()
    {
        var elementEquips = equippedCards.Where(x => x.IsElementBoost);
        Dictionary<Element, int> elementCounts = new Dictionary<Element, int>();

        foreach (var equip in elementEquips)
        {
            if (!elementCounts.ContainsKey(equip.ElementBoosted))
            {
                elementCounts.Add(equip.ElementBoosted, 0);
            }

            elementCounts[equip.ElementBoosted] += equip.BoostAmount;
        }

        var keys = elementCounts.Keys.OrderByDescending(x => x.ElementName).ToList();
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < keys.Count; i++)
        {
            sb.Append($"{elementCounts[keys[i]]} {keys[i]}");
            sb.Append(i != keys.Count - 1 ? ", " : "");
        }

        return sb.ToString();
    }
}
