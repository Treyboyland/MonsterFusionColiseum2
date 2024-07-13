using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class CardUILarge : CardUI
{

    [Header("Text Objects")]
    [SerializeField]
    TMP_Text cardTitleText;

    [SerializeField]
    TMP_Text healthText;

    [SerializeField]
    TMP_Text currentEquippedElementsText;

    [SerializeField]
    TMP_Text cardTypeText;

    [SerializeField]
    TMP_Text descriptionText;

    [SerializeField]
    CardTypeCompare cardTypeCompare;

    void GameCardSpecificText(GameCard card)
    {
        var cardData = card.CurrentCardData;
        if (cardTypeCompare.IsMonster(cardData.CardType))
        {
            healthText.text = $"{card.CurrentHealth} / {card.MaxHealth}";
            currentEquippedElementsText.text = card.GetEquippedMana();
        }
        else
        {
            healthText.text = "";
            currentEquippedElementsText.text = "";
        }
    }

    void CardSpecificText(Card card)
    {
        if (cardTypeCompare.IsMonster(card.CardType))
        {
            var monster = (MonsterCard)card;
            healthText.text = $"{monster.MaxHealth}";
            currentEquippedElementsText.text = "";
        }
        else
        {
            healthText.text = "";
            currentEquippedElementsText.text = "";
        }
    }

    public override void DisplayData(GameCard card)
    {
        CurrentCard = card.CurrentCardData;

        GameCardSpecificText(card);
        DisplayGeneralData(CurrentCard);
    }

    void DisplayGeneralData(Card cardData)
    {
        cardTitleText.text = cardData.CardName;

        StringBuilder typeText = new StringBuilder();
        typeText.Append("[");
        if (cardTypeCompare.IsMonster(cardData.CardType))
        {
            var monster = (MonsterCard)cardData;
            typeText.Append($"{monster.MonsterAttribute.AttributeName} / {monster.Element.ElementName}");
        }
        else
        {
            typeText.Append($"{cardData.CardType.TypeName}");
        }
        typeText.Append("]");

        cardTypeText.text = typeText.ToString();

        //TODO: Abilites, attacks for monsters
        descriptionText.text = cardData.Description;
    }


    public override void DisplayData(Card cardData)
    {
        CurrentCard = cardData;
        CardSpecificText(cardData);
        DisplayGeneralData(cardData);
    }
}
