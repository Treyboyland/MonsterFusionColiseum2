using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
    [SerializeField]
    protected string cardName;

    [SerializeField]
    protected string description;

    [SerializeField]
    protected CardType cardType;

    [SerializeField]
    protected Sprite faceDownSprite;

    [SerializeField]
    protected Sprite faceUpSprite;

    [SerializeField]
    List<CardOption> cardOptions;

    public string CardName { get => cardName; }
    public string Description { get => description; }
    public CardType CardType { get => cardType; }
    public virtual Sprite FaceUpSprite { get => faceUpSprite; }
    public Sprite FaceDownSprite { get => faceDownSprite; }

    public virtual string SubTypeText { get => $"[{cardType.TypeName}]"; }
}
