using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandUI : MonoBehaviour
{
    [SerializeField]
    CardHandUI handCardPrefab;

    [SerializeField]
    GameObject cardHoldingObject;

    [SerializeField]
    GameObject handEmptyObject;

    [SerializeField]
    Player player;

    [Header("State Management")]
    [SerializeField]
    GameManager manager;

    [SerializeField]
    GameState selectingSummonState;

    ObjectPool<CardHandUI> objectPool;

    int currentSelectedIndex;

    int currentFusionCount = 0;

    List<CardHandUI> currentCards = new List<CardHandUI>();


    // Start is called before the first frame update
    void Start()
    {
        objectPool = new ObjectPool<CardHandUI>(handCardPrefab);
        HideHand();
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

    public void ToggleFusionAdd()
    {
        bool removed = currentCards[currentSelectedIndex].SelectedForFusion;
        currentCards[currentSelectedIndex].SelectedForFusion = !removed;
        currentCards[currentSelectedIndex].FusionIndex = removed ? 0 : currentFusionCount + 1;
        if (!removed)
        {
            currentFusionCount++;
        }
        else
        {
            currentFusionCount--;
        }
        UpdateFusionList();
    }

    private void UpdateFusionList()
    {
        List<CardHandUI> orderedCards = currentCards.Where(x => x.SelectedForFusion).OrderBy(x => x.FusionIndex).ToList();
        for (int i = 0; i < orderedCards.Count; i++)
        {
            orderedCards[i].FusionIndex = i + 1;
        }
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
            cardUI.SelectedForFusion = false;
            cardUI.FusionIndex = 0;
            cardUI.HandIndex = i;
            cardUI.SetCardData(player.Hand[i]);
            cardUI.transform.SetParent(cardHoldingObject.transform);
            cardUI.gameObject.SetActive(true);
            cardUI.transform.localScale = Vector3.one; //For some reason, this spawns in at double size
            currentCards.Add(cardUI);
        }
    }

    public void HideHand()
    {
        objectPool.DisableAll();
        handEmptyObject.SetActive(false);
    }

    public List<Card> GetFusionList()
    {
        var cardList = currentCards.Where(x => x.SelectedForFusion).OrderBy(x => x.FusionIndex);
        var firstList = cardList.Select(x => x.CurrentCard).ToList();
        if (firstList.Count == 0)
        {
            return currentCards.Where(x => x.IsSelected).Select(x => x.CurrentCard).ToList();
        }
        return firstList;
    }

    #region Controller Input
    public void ProcessNavigation(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && manager.CurrentState == selectingSummonState)
        {
            Vector2 direction = context.ReadValue<Vector2>();

            if (direction == Vector2.zero)
            {
                return;
            }

            if (MathF.Abs(direction.x) > MathF.Abs(direction.y))
            {
                //Left Right
                if (direction.x < 0)
                {
                    PreviousIndex();
                }
                else
                {
                    NextIndex();
                }
            }
            else
            {
                //Up Down
                ToggleFusionAdd();
            }
        }
    }

    public void ProcessAcceptance(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && manager.CurrentState == selectingSummonState)
        {
            manager.PerformSummon();
        }
    }

    #endregion
}
