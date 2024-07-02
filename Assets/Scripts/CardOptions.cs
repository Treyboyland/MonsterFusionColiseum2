using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardOptions : MonoBehaviour
{
    [SerializeField]
    CardOptionButton buttonPrefab;

    [SerializeField]
    CardType leader, monster;

    [Header("State")]
    [SerializeField]
    GameState inactiveState;

    [SerializeField]
    GameState activeState, attackState;

    [Header("Card Options")]
    [SerializeField]
    CardOption abilityOption;

    [SerializeField]
    CardOption attackOption, summonOption, backOption;

    GameState currentState;

    ObjectPool<CardOptionButton> buttonPool;

    Card currentCard;

    // Start is called before the first frame update
    void Start()
    {
        buttonPool = new ObjectPool<CardOptionButton>(buttonPrefab);
    }

    void GoBack()
    {
        if (currentState == activeState)
        {
            currentState = inactiveState;
            buttonPool.DisableAll();
        }
        if (currentState == attackState)
        {
            currentState = activeState;
            SetOptionsForCard();
        }
    }

    private void SetOptionsForCard()
    {
        buttonPool.DisableAll();
        //If leaders can have manual abilities, then we might want to add this here
        // if (currentCard.CardType == leader)
        // {
        //     CreateOptions(summonOption, backOption);
        // }
        if (currentCard.CardType == monster)
        {
            //TODO: Ability option should be based on having a manual ability
            CreateOptions(attackOption, backOption);
        }
    }

    void CreateOptions(params CardOption[] options)
    {
        foreach (var option in options)
        {
            var optionButton = buttonPool.GetItem();
            optionButton.CardOption = option;
            optionButton.gameObject.SetActive(true);
        }
    }

    public void OptionSelected(CardOption cardOption)
    {
        if (cardOption == backOption)
        {
            GoBack();
        }
        if (cardOption == attackOption)
        {
            Debug.Log("Do this action");
        }
    }
}
