using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratcherGameManager : MonoBehaviour
{
    public int cash = 1000;
    public int scratcherCost = 10;
    public int scratcherCount = 0;

    public ScratcherCard.Symbol Cherry = ScratcherCard.Symbol.Cherry;
    public ScratcherCard.Symbol Orange = ScratcherCard.Symbol.Orange;
    public ScratcherCard.Symbol Banana = ScratcherCard.Symbol.Banana;

    public ScratcherCard scratcherCardPrefab;
    public ScratcherCard curCard;
    public RectTransform cardParent;

    public Camera gasCamera;
    public Camera scratchCamera;

    public Button continueButton;

    private void Start() {
        scratcherCount = 0;
        scratcherCost = 10;
        cash = 1000;
    }


    public void OnSwapClicked()
    {
        scratchCamera.enabled = !scratchCamera.enabled;
        gasCamera.enabled = !gasCamera.enabled;
    }

    public void OnNextClicked() {
        if (scratcherCount == 0)
        {
            scratcherCost = 5;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Orange, Banana, Orange);
        }
        else if (scratcherCount == 1)
        {
            scratcherCost = 10;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Banana, Banana, Cherry);
        }
        else if (scratcherCount == 2)
        {
            scratcherCost = 0;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Cherry, Cherry, Cherry);
        }
        else if (scratcherCount == 3)
        {
            scratcherCost = 200;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Orange, Banana, Cherry);
        }
        else if (scratcherCount == 4)
        {
            scratcherCost = 200;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Banana, Orange, Cherry);
        }
        else if (scratcherCount == 5)
        {
            scratcherCost = 200;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Cherry, Orange, Banana);
        }
        else if (scratcherCount == 6)
        {
            scratcherCost = 200;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Cherry, Banana, Orange);
        }
        else if (scratcherCount == 7)
        {
            scratcherCost = 200;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Orange, Cherry, Banana);
        }
        else if (scratcherCount == 8)
        {
            scratcherCost = 200;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Orange, Banana, Cherry);
        }
        else if (scratcherCount == 9)
        {
            scratcherCost = 200;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Banana, Orange, Cherry);
        }
        else if (scratcherCount == 10)
        {
            scratcherCost = 200;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Cherry, Orange, Banana);
        }
        else if (scratcherCount == 11)
        {
            scratcherCost = 200;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Orange, Cherry, Banana);
        }
        else if (scratcherCount == 12)
        {
            scratcherCost = 200;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Banana, Cherry, Orange);
        }
        else if (scratcherCount == 13)
        {
            scratcherCost = 200;
            scratcherCount += 1;
            BuyNewScratcher();
            curCard.SetSymbols(Cherry, Banana, Orange);
        }
    }

    

    public void BuyNewScratcher() {
        cash -= scratcherCost;

        if(curCard != null) {
            Destroy(curCard.gameObject);
        }

        curCard = Instantiate(scratcherCardPrefab, cardParent);
        //curCard.SetSymbols(ScratcherCard.Symbol.Cherry, ScratcherCard.Symbol.Cherry, ScratcherCard.Symbol.Cherry);
        curCard.OnAllSegmentsScratched.AddListener(OnScratcherAllSegmentsScratched);

        curCard.SetRandomSymbols();

        nextButton.interactable = false;
    }

    public void OnScratcherAllSegmentsScratched() {
        nextButton.interactable = true;
        IssueReward();
    }

    private void IssueReward() {
        int amountWon = 0;
        if (curCard.symbols[0] == ScratcherCard.Symbol.Cherry &&
            curCard.symbols[1] == ScratcherCard.Symbol.Cherry &&
            curCard.symbols[2] == ScratcherCard.Symbol.Cherry
            ) {
            amountWon = 1000;
        }

        int matches = 0;
        for(int i = 0; i < curCard.symbols.Count - 1; i++) {
            int curMatches = 0;
            for(int j = i+1; j < curCard.symbols.Count; j++) {
                if (curCard.symbols[i] == curCard.symbols[j])
                    curMatches++;
            }

            if(curMatches > matches) {
                matches = curMatches;
            }
        }

        if(matches == 2) {
            amountWon = 1000;
        } else if(matches == 1) {
            amountWon = 20;
        }

        if (amountWon > 0) {
            cash += amountWon;
            // TODO: YOU WON!
            Debug.Log($"You have {matches} matches. You won ${amountWon}.");
        }
    }

    private void Update() {
        UpdateUI();
    }

    public Text cashLabel;
    public Button nextButton;
    void UpdateUI() {
        cashLabel.text = $"${cash}";
    }
}
