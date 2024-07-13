using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Player enemyPlayer;

    [SerializeField]
    GameCard enemyLeader;

    [SerializeField]
    CardTypeCompare cardTypeCompare;


    // Start is called before the first frame update
    void Start()
    {
        enemyLeader.Player = enemyPlayer;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool WithinAttackRange()
    {
        // if (cardTypeCompare.IsSpell(currentCardData.CardType))
        // {

        // }
        // else if (cardTypeCompare.IsMonster(currentCardData.CardType))
        // {
        //     var monster = (MonsterCard)currentCardData;
        //     if (monster.Attacks.Count == 0)
        //     {
        //         return true;
        //     }
        //     return monster.Attacks.Where(x => x.CanUseAttack(CurrentMana) && x.)
        // }
        return false;
    }
}
