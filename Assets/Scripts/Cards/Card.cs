using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
    [SerializeField]
    protected string cardName;

    [TextArea]
    [SerializeField]
    protected string description;

    [SerializeField]
    protected CardType cardType;

    [SerializeField]
    protected Sprite faceDownSprite;

    [SerializeField]
    protected Sprite faceUpSprite;

    [SerializeField]
    protected List<CardOption> cardOptions;

    public string CardName { get => cardName; }
    public string Description { get => description; }
    public CardType CardType { get => cardType; }
    public virtual Sprite FaceUpSprite { get => faceUpSprite; }
    public Sprite FaceDownSprite { get => faceDownSprite; }

    public virtual string SubTypeText { get => $"[{cardType.TypeName}]"; }

    public virtual string DetailedName { get => cardName; }

    protected void CopyBasicValues(Card receiver)
    {
        receiver.cardName = cardName;
        receiver.description = description;
        receiver.cardType = cardType;
        receiver.faceDownSprite = faceDownSprite;
        receiver.cardOptions = new List<CardOption>(cardOptions);
    }

    public override bool Equals(object other)
    {
        if (other is not Card)
        {
            return false;
        }
        return cardName.Equals(((Card)other).cardName);
    }

    public override int GetHashCode()
    {
        return cardName.GetHashCode();
    }

    public static bool operator ==(Card left, Card right)
    {
        bool nullLeft = ReferenceEquals(left, null);
        bool nullRight = ReferenceEquals(right, null);

        if ((nullLeft && nullRight) == true)
        {
            return true;
        }
        if (nullLeft != nullRight)
        {
            return false;
        }

        return left.cardName == right.cardName;
    }

    public static bool operator !=(Card left, Card right)
    {
        return !(left == right);
    }
}
