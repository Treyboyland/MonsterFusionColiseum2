using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Element-", menuName = "Game/Element")]
public class Element : ScriptableObject
{
    [SerializeField]
    protected string elementName;

    public string ElementName { get => elementName; }
}
