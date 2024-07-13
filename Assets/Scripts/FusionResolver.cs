using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FusionResolver : MonoBehaviour
{
    [SerializeField]
    FusionTable fusionTable;

    [SerializeField]
    CardTypeCompare cardTypeCompare;

    public struct FusionData
    {
        public Card FusedCard;
        public List<SpellCardEquip> EquippedCards;
        public string FusionSteps;
    }

    bool OneEquipSpellOneMonster(Card first, Card second)
    {
        return cardTypeCompare.IsMonster(first.CardType) && second is SpellCardEquip ||
            cardTypeCompare.IsMonster(second.CardType) && first is SpellCardEquip;
    }

    bool BothMonsters(Card first, Card second)
    {
        return cardTypeCompare.IsMonster(first.CardType) && cardTypeCompare.IsMonster(second.CardType);
    }

    Card ResolveFusionStep(Card first, Card second, List<SpellCardEquip> equippedSpells)
    {
        if (OneEquipSpellOneMonster(first, second))
        {
            equippedSpells.Add(first is SpellCardEquip ? (SpellCardEquip)first : (SpellCardEquip)second);
            return cardTypeCompare.IsMonster(first.CardType) ? first : second;
        }

        bool validfusion = false;
        Card fusedCard = null;

        if (BothMonsters(first, second))
        {
            MonsterCard firstMonster = (MonsterCard)first;
            MonsterCard secondMonster = (MonsterCard)second;

            bool elementMatch = fusionTable.HasElementMatch(firstMonster, secondMonster);
            bool monsterMatch = fusionTable.HasMonsterMatch(firstMonster, secondMonster);

            validfusion = elementMatch || monsterMatch;

            //TODO: Determine if monster element fusion is valid
            Element fusedElement = elementMatch ?
                fusionTable.GetMatchingElement(firstMonster, secondMonster) :
                firstMonster.Element;

            if (monsterMatch)
            {
                fusedCard = fusionTable.GetMatchingMonster(firstMonster, secondMonster).Duplicate();
            }
            else
            {
                fusedCard = firstMonster.Duplicate();
            }
            ((MonsterCard)fusedCard).Element = fusedElement;
        }

        if (!validfusion)
        {
            equippedSpells.Clear();
        }

        //Fusion can only happen between monster and spell or monster and monster that is valid
        return validfusion ? fusedCard : second;
    }

    public FusionData ResolveFusion(List<Card> cards)
    {
        StringBuilder sb = new StringBuilder();
        if (cards.Count == 1)
        {
            sb.Append($"Result: {cards[0].DetailedName}");
            return new FusionData() { FusedCard = cards[0], EquippedCards = new List<SpellCardEquip>(), FusionSteps = sb.ToString() };
        }

        List<Card> cardList = new List<Card>(cards);
        Card first, second;
        first = cardList[0];
        second = cardList[1];
        cardList.RemoveAt(0);
        cardList.RemoveAt(0);

        List<SpellCardEquip> equippedSpells = new List<SpellCardEquip>();

        sb.Append($"{first.DetailedName} + {second.DetailedName} = ");

        first = ResolveFusionStep(first, second, equippedSpells);
        sb.AppendLine($"{first.DetailedName}");

        while (cardList.Count > 0)
        {
            second = cardList[0];
            cardList.RemoveAt(0);
            sb.Append($"{first.DetailedName} + {second.DetailedName} = ");
            first = ResolveFusionStep(first, second, equippedSpells);
            sb.AppendLine($"{first.DetailedName}");
        }

        return new FusionData() { FusedCard = first, EquippedCards = equippedSpells, FusionSteps = sb.ToString() };
    }
}
