using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUIController : MonoBehaviour
{
    [SerializeField]
    CardUI infoCard;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        HideUI();
    }

    public void HideUI()
    {
        infoCard.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        infoCard.gameObject.SetActive(true);
    }

    public void SetData(GameCard card)
    {
        infoCard.DisplayData(card);
    }

    public void SetData(Card card)
    {
        infoCard.DisplayData(card);
    }
}
