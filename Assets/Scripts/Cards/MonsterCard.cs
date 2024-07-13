using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    List<Ability> abilities;

    [SerializeField]
    List<Attack> attacks;

    [SerializeField]
    int healthBoostPerLevel;

    [SerializeField]
    int attackBoostPerLevel;

    public override Sprite FaceUpSprite { get => monsterAttribute.Sprite; }

    public int MaxHealth { get => maxHealth; }
    public Element Element { get => element; set { element = value; } }
    public MonsterAttribute MonsterAttribute { get => monsterAttribute; }
    public List<Attack> Attacks { get => attacks; set => attacks = value; }
    public int HealthBoostPerLevel { get => healthBoostPerLevel; }
    public int AttackBoostPerLevel { get => attackBoostPerLevel; }

    public override string DetailedName { get => Element.ElementName + " " + cardName; }

    public bool HasActiveAbility()
    {
        return abilities.Where(x => !x.IsPassive).Count() != 0;
    }

    public MonsterCard Duplicate()
    {
        MonsterCard newCard = ScriptableObject.CreateInstance<MonsterCard>();
        newCard.element = element;
        newCard.monsterAttribute = monsterAttribute;
        newCard.maxHealth = maxHealth;

        //Base Card Stuff
        CopyBasicValues(newCard);
        return newCard;
    }
}
