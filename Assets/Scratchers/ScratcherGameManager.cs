using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratcherGameManager : MonoBehaviour
{
    public int cash = 1000;
    public int scratcherCost = 10;

    public ScratcherCard scratcherCardPrefab;
    public ScratcherCard curCard;
    public RectTransform cardParent;

    private void Start() {
        BuyNewScratcher();
    }

    public void OnNextClicked() {
        BuyNewScratcher();
    }

    public void BuyNewScratcher() {
        cash -= scratcherCost;

        if(curCard != null) {
            Destroy(curCard.gameObject);
        }

        curCard = Instantiate(scratcherCardPrefab, cardParent);
        curCard.OnAllSegmentsScratched.AddListener(OnScratcherAllSegmentsScratched);

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
