using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardUI : MonoBehaviour
{
    public abstract void DisplayData(Card cardData);

    public abstract void DisplayData(GameCard card);
}
