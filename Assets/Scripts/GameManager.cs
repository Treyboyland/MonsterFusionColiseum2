using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
            activeCard.IsSelected = true;
            currentState = movingCardState;
            highlights.ShowValidMovementPositions(activeCard);
        }

        if (activeCard != null)
        {
            if (!cardAtPosition)
            {
                cardsInPlay.MoveGameCardToPosition(activeCard, gridPosition);
                activeCard.IsSelected = false;
                activeCard = null;
                currentState = normalState;
            }
            else
            {
                var newCard = cardsInPlay.GetGameCardAtPosition(gridPosition);
                if (newCard.CardOwner == playerOwner)
                {
                    //Fusion stuff
                }
            }
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
            activeCard.IsSelected = false;
            activeCard = null;
            currentState = normalState;
            highlights.DisableHighlight();
        }
        else
        {
            var newCard = cardsInPlay.GetGameCardAtPosition(gridPosition);
            if (newCard == activeCard)
            {
                activeCard.IsSelected = false;
                activeCard = null;
                currentState = normalState;
                highlights.DisableHighlight();
            }
            else if (newCard.CardOwner == playerOwner)
            {
                //TODO: Fusion
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
                //TODO: Summon 
                currentState = selectingCardsState;
            }
        }
        else
        {
            //TODO: Summon
            currentState = selectingCardsState;
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
                        //TODO: Summon
                        currentState = selectingSummonPositionState;
                        activeCard = card;
                        activeCard.IsSelected = true;
                        highlights.ShowValidSummonPositions(activeCard);
                    }
                }
            }
        }
    }

    public void OnPositionCancel(Vector2Int gridPosition)
    {
        if (currentState == selectingSummonPositionState)
        {
            currentState = normalState;
            activeCard.IsSelected = false;
            activeCard = null;
            highlights.DisableHighlight();
        }
        else if (currentState == movingCardState)
        {
            currentState = normalState;
            activeCard.IsSelected = false;
            activeCard = null;
            highlights.DisableHighlight();
        }
        else if (currentState == infoState)
        {
            currentState = normalState;
            cardUIController.HideUI();
        }
        else if (currentState == selectingCardsState)
        {
            currentState = selectingSummonPositionState;
            //TOOD: Hide summon ui
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
                    currentState = infoState;
                    //TODO: Show info window for card
                    cardUIController.SetData(card);
                    cardUIController.ShowUI();
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
}
