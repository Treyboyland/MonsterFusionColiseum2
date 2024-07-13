using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrder : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Player enemyPlayer;

    Player currentPlayer;

    [SerializeField]
    bool bypassTurnRequirement;

    public bool IsPlayerTurn => bypassTurnRequirement || currentPlayer == player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndTurn()
    {
        if (currentPlayer == player)
        {
            currentPlayer = enemyPlayer;
        }
        else
        {
            currentPlayer = player;
        }
    }
}
