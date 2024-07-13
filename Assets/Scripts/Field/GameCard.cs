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
    Player currentPlayer;

    [SerializeField]
    Card currentCardData;

    [SerializeField]
    int currentMana;

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

    public Player Player { get => currentPlayer; set => currentPlayer = value; }

    public CardOwner CardOwner { get => Player.Owner; }

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
    public int CurrentMana { get => currentMana; set => currentMana = value; }

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

    public void SetData(FusionResolver.FusionData data)
    {
        SetData(data.FusedCard);
        equippedCards = data.EquippedCards;
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

    public string GetEquippedMana()
    {
        var elementEquips = equippedCards.Where(x => x.IsManaBoost);
        int amount = elementEquips.Select(x => x.GetBoostedAmount(Player != null ?
            Player.CardLevelHolder.GetLevelForCard(currentCardData) : 0)).Sum();
        return "" + amount + " Mana";
    }
}
