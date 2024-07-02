using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ScriptableObject
{
    [SerializeField]
    protected string attackName;

    [TextArea]
    [SerializeField]
    protected string description;

    [SerializeField]
    protected Element element;

    [SerializeField]
    AreaOfEffect areaOfEffect;

    public string AttackName { get => attackName; }
    public string Description { get => description; }
}
