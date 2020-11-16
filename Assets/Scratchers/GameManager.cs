using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Game Buttons
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;
    public Button leaveBtn;

    private int standClicks = 0;

    // Access the player and dealer's script
    public PlayerScript playerScript;
    public PlayerScript dealerScript;

    // public Text to access and update - HUD
    public Text scoreText;
    public Text dealerScoreText;
    public Text betsText;
    public Text cashText;
    public Text mainText;
    public Text standBtnText;

    // Card hiding dealer's 2nd card
    public GameObject hideCard;

    // How much is bet
    int pot = 0;

    // How many rounds played
    int round = 0;

    void Start()
    {
        // Add on click listeners to the buttons
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        betBtn.onClick.AddListener(() => BetClicked());
    }

    

    private void DealClicked()
    {
        // Reset round, hide text, prep for new hand. Add 1 to round counter.
        round += 1;
        playerScript.ResetHand();
        dealerScript.ResetHand();

        // Hide deal hand score at start of deal
        dealerScoreText.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();

        // Update the score displayed
        scoreText.text = "Hand: " + playerScript.handValue.ToString();
        dealerScoreText.text = "Hand: " + dealerScript.handValue.ToString();

        // Enable to hide one of the dealer's cards
        hideCard.GetComponent<Renderer>().enabled = true;

        // Adjust buttons visibility
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        betBtn.gameObject.SetActive(true);
        standBtnText.text = "Stand";

        // Set standard pot size
        pot = 40;
        betsText.text = "Bets: $" + pot.ToString();
        playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.GetMoney().ToString();

    }

    private void HitClicked()
    {
        // Check that there is still room on the table
        if (playerScript.cardIndex <= 10)
        {
            playerScript.GetCard();
            scoreText.text = "Hand: " + playerScript.handValue.ToString();
            if (playerScript.handValue > 20) RoundOver();
        }
    }

    private void StandClicked()
    {
        standClicks++;
        if (standClicks > 1) RoundOver();
        HitDealer();
        standBtnText.text = "Call";
        hitBtn.gameObject.SetActive(false);
    }

    private void LeaveClicked()
    {
        SceneManager.LoadScene("EndScene");
    }

    private void HitDealer()
    {
        while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            dealerScoreText.text = "Hand: " + dealerScript.handValue.ToString();
            if (dealerScript.handValue > 20) RoundOver();
        }
    }

    // Check for winner and loser, hand is over
    void RoundOver()
    {
        // Booleans for bust and blackjack
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;

        // If stand has been clicked less than twice, no 21s or busts, quit function
        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;
        bool roundOver = true;

        // All bust, bets returned
        if (playerBust && dealerBust)
        {
            mainText.text = "All Bust: Bets returned";
            playerScript.AdjustMoney(pot / 2);
        }
        // If player busts, dealer didn't, or if dealer has more points, dealer wins
        else if (playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue))
        {
            mainText.text = "Dealer wins!";
        }
        // dealer busts, player didn't, or player has more points, player wins
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            mainText.text = "You win!";
            playerScript.AdjustMoney(pot);
        }
        // Check for tie, return bets
        else if (playerScript.handValue == dealerScript.handValue)
        {
            mainText.text = "Push";
            playerScript.AdjustMoney(pot / 2);
        }
        else
        {
            roundOver = false;
        }
        // Set up UI for next move / hand / turn
        if (roundOver)
        {
            hitBtn.gameObject.SetActive(false);
            standBtn.gameObject.SetActive(false);
            dealBtn.gameObject.SetActive(true);
            mainText.gameObject.SetActive(true);
            betBtn.gameObject.SetActive(false);
            dealerScoreText.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            cashText.text = "$" + playerScript.GetMoney().ToString();
            standClicks = 0;
            // Activate "Leave Table button after round 3"
            if (round >= 3)
            {
                leaveBtn.gameObject.SetActive(true);
            }
            if (playerScript.GetMoney() <= 0)
            {
                dealBtn.gameObject.SetActive(false);
                SceneManager.LoadScene(2);                
            }
            print("End of Round: " + round);
        }
    }

    // Add money to pot if bet clicked
    void BetClicked()
    {
        Text newBet = betBtn.GetComponentInChildren(typeof(Text)) as Text;
        int intBet = int.Parse(newBet.text.ToString().Remove(0, 1));
        if (playerScript.money >= 20)
        {
            playerScript.AdjustMoney(-intBet);
            cashText.text = "$" + playerScript.GetMoney().ToString();
            pot += (intBet * 2);
            betsText.text = "Bets: $" + pot.ToString();
        }
    }

}
