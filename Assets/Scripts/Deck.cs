using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck<T>
{
    List<T> cards;

    System.Random rand;

    public int Count { get { return cards.Count; } }

    public Deck(List<T> newcards)
    {
        cards = new List<T>(newcards);
        rand = new System.Random();
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int chosenIndex = rand.Next(i, cards.Count);
            var temp = cards[i];
            cards[i] = cards[chosenIndex];
            cards[chosenIndex] = temp;
        }
    }

    public T Draw()
    {
        if (cards.Count != 0)
        {
            T toReturn = cards[0];
            cards.RemoveAt(0);
            return toReturn;
        }
        return default;
    }

    public void Add(T card)
    {
        cards.Add(card);
    }
}

[Serializable]
public struct CardAndLevelSO
{
    public Card Card;
    public int Level;
}

[Serializable]
public struct CardAndLevel
{
    public string CardName;
    public int Level;
}


[Serializable]
public struct CardAndCount
{
    public Card Card;
    public int Count;
}


public static class DeckExtensions
{
    public static Deck<Card> CreateDeck(List<CardAndCount> cards)
    {
        return new Deck<Card>(cards.ToList());
    }

    public static List<Card> ToList(this List<CardAndCount> cards)
    {
        List<Card> cardList = new List<Card>();

        foreach (var card in cards)
        {
            for (int i = 0; i < card.Count; i++)
            {
                cardList.Add(card.Card);
            }
        }

        return cardList;
    }
}
