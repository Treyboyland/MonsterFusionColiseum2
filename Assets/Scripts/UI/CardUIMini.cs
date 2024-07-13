using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class CardUIMini : CardUI
{
    [SerializeField]
    TMP_Text cardTitleText;

    [SerializeField]
    TMP_Text cardTypeText;

    [SerializeField]
    TMP_Text descriptionText;

    [SerializeField]
    CardTypeCompare cardTypeCompare;

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

        descriptionText.text = cardData.Description;
    }

    public override void DisplayData(Card cardData)
    {
        CurrentCard = cardData;
        DisplayGeneralData(cardData);
    }

    public override void DisplayData(GameCard card)
    {
        DisplayData(card.CurrentCardData);
    }
}
