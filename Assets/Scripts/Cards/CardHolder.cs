using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardHolder : ScriptableObject
{
    public abstract List<Card> GetCards();
}
