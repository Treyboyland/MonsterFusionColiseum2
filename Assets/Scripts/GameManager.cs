using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TileCursor tileCursor;

    [SerializeField]
    FusionResolver fusionResolver;

    [SerializeField]
    PlayerInput input;

    [SerializeField]
    Player player;

    [SerializeField]
    string playerMap;

    [SerializeField]
    string uiMap;

    [SerializeField]
    PlayerHandUI playerHandUI;

    [SerializeField]
    GameState currentState;

    [SerializeField]
    GameCardsInPlay cardsInPlay;

    [SerializeField]
    GameField gameField;

    [SerializeField]
    PositionHighlights highlights;

    [SerializeField]
    CardOwner playerOwner;

    [SerializeField]
    CardUIController cardUIController;

    [Header("States")]
    [SerializeField]
    GameState normalState;

    [SerializeField]
    GameState movingCardState, selectingSummonPositionState, selectingCardsState, infoState;

    [SerializeField]
    CardTypeCompare cardTypeCompare;

    GameCard activeCard;

    public GameState CurrentState
    {
        get => currentState; protected set
        {
            currentState = value;
            UpdateStateStuff();
        }
    }

    void Awake()
    {
        currentState = normalState;
    }

    public void OnPositionSelected(Vector2Int gridPosition)
    {
        if (currentState == normalState)
        {
            NormalPositionSelection(gridPosition);
        }
        else if (currentState == movingCardState)
        {
            MovementPositionSelection(gridPosition);
        }
        else if (currentState == selectingSummonPositionState)
        {
            SummonPositionSelection(gridPosition);
        }
    }

    void NormalPositionSelection(Vector2Int gridPosition)
    {
        bool cardAtPosition = cardsInPlay.HasCardAtPosition(gridPosition);
        if (!cardAtPosition)
        {
            return;
        }

        var card = cardsInPlay.GetGameCardAtPosition(gridPosition);

        if (card.CardOwner == playerOwner)
        {
            activeCard = card;
            CurrentState = movingCardState;
        }

        // if (activeCard != null)
        // {
        //     if (!cardAtPosition)
        //     {
        //         cardsInPlay.MoveGameCardToPosition(activeCard, gridPosition);
        //         activeCard.IsSelected = false;
        //         activeCard = null;
        //         currentState = normalState;
        //     }
        //     else
        //     {
        //         var newCard = cardsInPlay.GetGameCardAtPosition(gridPosition);
        //         if (newCard.CardOwner == playerOwner)
        //         {
        //             //Fusion stuff
        //         }
        //     }
        // }
    }

    void UpdateStateStuff()
    {
        if (currentState == normalState)
        {
            input.SwitchCurrentActionMap(playerMap);
            highlights.DisableHighlight();
            if (activeCard != null)
            {
                activeCard.IsSelected = false;
                activeCard = null;
            }
            playerHandUI.HideHand();
            cardUIController.HideUI();
        }
        else if (currentState == movingCardState)
        {
            if (activeCard != null)
            {
                activeCard.IsSelected = true;
                highlights.ShowValidMovementPositions(activeCard);
            }
        }
        else if (currentState == selectingSummonPositionState)
        {
            input.SwitchCurrentActionMap(playerMap);
            playerHandUI.HideHand();
            if (activeCard != null)
            {
                highlights.ShowValidSummonPositions(activeCard);
            }
        }
        else if (currentState == selectingCardsState)
        {
            input.SwitchCurrentActionMap(uiMap);
            playerHandUI.ShowHand();
        }
        else if (currentState == infoState)
        {
            input.SwitchCurrentActionMap(uiMap);
            cardUIController.ShowUI();
        }
    }

    void MovementPositionSelection(Vector2Int gridPosition)
    {
        /*
        TODO: I don't think attacks should be launched by walking over (i.e. opposing cards should be obstacles...unless...
        Can opposing cards be equipped??)
        */
        bool cardAtPosition = cardsInPlay.HasCardAtPosition(gridPosition);
        if (!cardAtPosition)
        {
            cardsInPlay.MoveGameCardToPosition(activeCard, gridPosition);
            CurrentState = normalState;
        }
        else
        {
            var newCard = cardsInPlay.GetGameCardAtPosition(gridPosition);
            if (newCard == activeCard)
            {
                CurrentState = normalState;
            }
            else if (newCard.CardOwner == playerOwner)
            {
                if (cardTypeCompare.IsLeader(activeCard.CurrentCardData.CardType))
                {
                    //Leaders overwrite their cards without fusion
                    cardsInPlay.RemoveFromPlay(newCard);
                    cardsInPlay.MoveGameCardToPosition(activeCard, gridPosition);
                    CurrentState = normalState;
                }
                else
                {
                    //TODO: Fusion
                    List<Card> fusionCards = new List<Card>() { newCard.CurrentCardData, activeCard.CurrentCardData };
                    var fusionResult = fusionResolver.ResolveFusion(fusionCards);
                    Debug.LogWarning(fusionResult.FusionSteps);
                    cardsInPlay.CreateCard(fusionResult, gridPosition, player);

                    //Remove previous fusion card from play
                    activeCard.IsSelected = false;
                    cardsInPlay.RemoveFromPlay(activeCard);
                    activeCard = null;
                    CurrentState = normalState;
                }
            }
        }
    }

    void SummonPositionSelection(Vector2Int gridPosition)
    {
        bool cardAtPosition = cardsInPlay.HasCardAtPosition(gridPosition);
        if (cardAtPosition)
        {
            var card = cardsInPlay.GetGameCardAtPosition(gridPosition);
            if (card.CardOwner == playerOwner && !cardTypeCompare.IsLeader(card.CurrentCardData.CardType))
            {
                CurrentState = selectingCardsState;
            }
        }
        else
        {
            CurrentState = selectingCardsState;
        }
    }

    public void OnPositionAbility(Vector2Int gridPosition)
    {
        if (currentState == normalState)
        {
            bool hasCard = cardsInPlay.HasCardAtPosition(gridPosition);
            if (hasCard)
            {
                var card = cardsInPlay.GetGameCardAtPosition(gridPosition);
                if (card.CardOwner == playerOwner)
                {
                    if (cardTypeCompare.IsMonster(card.CurrentCardData.CardType))
                    {
                        //TODO: Ability stuff
                    }
                    else if (cardTypeCompare.IsLeader(card.CurrentCardData.CardType))
                    {
                        activeCard = card;
                        activeCard.IsSelected = true;
                        CurrentState = selectingSummonPositionState;
                    }
                }
            }
        }
    }

    public void OnPositionCancel(Vector2Int gridPosition)
    {
        if (currentState == selectingSummonPositionState)
        {
            CurrentState = normalState;
        }
        else if (CurrentState == movingCardState)
        {
            CurrentState = normalState;
        }
        else if (currentState == infoState)
        {
            CurrentState = normalState;
        }
        else if (currentState == selectingCardsState)
        {
            CurrentState = selectingSummonPositionState;
        }
    }

    public void OnPositionInfo(Vector2Int gridPosition)
    {
        if (currentState == normalState)
        {
            bool hasCard = cardsInPlay.HasCardAtPosition(gridPosition);
            if (hasCard)
            {
                var card = cardsInPlay.GetGameCardAtPosition(gridPosition);
                if (card.CardOwner == playerOwner || card.FaceUp)
                {
                    cardUIController.SetData(card);
                    CurrentState = infoState;
                }
            }
        }
    }

    public void OnPositionFlip(Vector2Int gridPosition)
    {
        if (activeCard != null)
        {
            activeCard.Flip();
        }
    }

    /// <summary>
    /// Attempts a move to the given position. Returns the actual position reached
    /// </summary>
    /// <param name="destination"></param>
    /// <returns></returns>
    public Vector2Int AttemptMoveToPosition(Vector2Int destination, Vector2Int currentPosition)
    {
        if (currentState == selectingSummonPositionState)
        {
            return highlights.IsValidPosition(destination) ? destination : currentPosition;
        }
        else if (currentState == movingCardState)
        {
            return highlights.IsValidPosition(destination) ? destination : currentPosition;
        }
        else if (currentState == normalState)
        {
            return gameField.ClampGridPosition(destination);
        }
        else
        {
            return currentPosition;
        }
    }

    public void PerformSummon()
    {
        StringBuilder sb = new StringBuilder();
        var cards = playerHandUI.GetFusionList();

        if (cardsInPlay.HasCardAtPosition(tileCursor.CurrentPosition))
        {
            //Do we need to check again for ownership?
            var cardInPlay = cardsInPlay.GetGameCardAtPosition(tileCursor.CurrentPosition);
            cards.Insert(0, cardInPlay.CurrentCardData);
        }

        var fusionResult = fusionResolver.ResolveFusion(cards);
        foreach (var card in cards)
        {
            player.RemoveFromHand(card);
        }
        Debug.LogWarning(fusionResult.FusionSteps);
        cardsInPlay.CreateCard(fusionResult, tileCursor.CurrentPosition, player);
        CurrentState = normalState;
    }
}
