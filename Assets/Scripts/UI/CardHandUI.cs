using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardHandUI : MonoBehaviour
{
    [SerializeField]
    GameObject fusionIndexObject;

    [SerializeField]
    TMP_Text fusionIndexText;

    [SerializeField]
    CardUIMini cardUIMini;

    [SerializeField]
    GameObject selectedGameObject;

    public bool IsSelected { get => selectedGameObject.activeInHierarchy; set { selectedGameObject.SetActive(value); } }

    public bool SelectedForFusion { get => fusionIndexObject.activeInHierarchy; set { fusionIndexObject.SetActive(value); } }

    public int FusionIndex { set { fusionIndexText.text = $"{value}"; } }

    public int HandIndex { get; set; } = 0;
    
    public void SetCardData(Card card)
    {
        cardUIMini.DisplayData(card);
    }
}
