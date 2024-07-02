using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandUI : MonoBehaviour
{
    [SerializeField]
    CardHandUI handCardPrefab;

    [SerializeField]
    GameObject handEmptyObject;

    [SerializeField]
    Player player;

    ObjectPool<CardHandUI> objectPool;

    int currentSelectedIndex;

    int currentFusionCount = 0;

    List<CardHandUI> currentCards = new List<CardHandUI>();


    // Start is called before the first frame update
    void Start()
    {
        objectPool = new ObjectPool<CardHandUI>(handCardPrefab);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateSelectedIcon()
    {
        foreach (var card in currentCards)
        {
            card.IsSelected = currentSelectedIndex == card.HandIndex;
        }
    }

    public void NextIndex()
    {
        currentSelectedIndex = (currentSelectedIndex + 1) % currentCards.Count;
        UpdateSelectedIcon();
    }

    public void PreviousIndex()
    {
        currentSelectedIndex--;
        if (currentSelectedIndex < 0)
        {
            currentSelectedIndex = currentCards.Count - 1;
        }
        UpdateSelectedIcon();
    }

    public void ShowHand()
    {
        objectPool.DisableAll();
        player.FillHand();
        currentCards.Clear();
        currentSelectedIndex = 0;
        currentFusionCount = 0;

        handEmptyObject.SetActive(player.Hand.Count == 0);
        if (player.Hand.Count == 0)
        {
            return;
        }

        for (int i = 0; i < player.Hand.Count; i++)
        {
            var cardUI = objectPool.GetItem();
            cardUI.IsSelected = currentSelectedIndex == i;
            cardUI.HandIndex = i;
            cardUI.SetCardData(player.Hand[i]);
        }
    }
}
