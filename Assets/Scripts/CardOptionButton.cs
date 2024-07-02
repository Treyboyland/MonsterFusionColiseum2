using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardOptionButton : MonoBehaviour
{
    [SerializeField]
    Button button;

    [SerializeField]
    TMP_Text textBox;

    [SerializeField]
    CardOption cardOption;

    [SerializeField]
    GameEventCardOption onClickEvent;

    public CardOption CardOption
    {
        get => cardOption; set
        {
            cardOption = value;
            textBox.text = cardOption.OptionName;
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        button.onClick.AddListener(() => onClickEvent.Invoke(cardOption));
    }
}
