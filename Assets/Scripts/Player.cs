using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    bool shouldShuffle;

    [SerializeField]
    CardHolderCount startingDeck;

    [SerializeField]
    int handSize;

    [SerializeField]
    List<Card> hand;

    [SerializeField]
    List<Card> discardPile;

    Deck<Card> deck;

    public List<Card> Hand { get => hand; }
    public List<Card> DiscardPile { get => discardPile; }


    // Start is called before the first frame update
    void Start()
    {
        deck = new Deck<Card>(startingDeck.GetCards());
        if (shouldShuffle)
        {
            deck.Shuffle();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDeck(List<CardAndCount> cards)
    {
        deck = new Deck<Card>(cards.ToList());
        hand.Clear();
        discardPile.Clear();
        if (shouldShuffle)
        {
            deck.Shuffle();
        }
    }

    public void FillHand()
    {
        while (hand.Count < handSize)
        {
            if (deck.Count == 0)
            {
                break;
            }
            hand.Add(deck.Draw());
        }
    }

    public void AddToDiscard(Card card)
    {
        discardPile.Add(card);
    }

}
