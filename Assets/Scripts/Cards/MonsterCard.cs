using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card-Monster-", menuName = "Game/Card/Monster")]
public class MonsterCard : Card
{
    [SerializeField]
    Element element;

    [SerializeField]
    MonsterAttribute monsterAttribute;

    [SerializeField]
    int maxHealth;

    public override Sprite FaceUpSprite { get => monsterAttribute.Sprite; }

    public int MaxHealth { get => maxHealth; }
    public Element Element { get => element; }
    public MonsterAttribute MonsterAttribute { get => monsterAttribute; }
}
