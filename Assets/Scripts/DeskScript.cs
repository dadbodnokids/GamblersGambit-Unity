using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskScript : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[53];
    int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void GetCardValues()
    {
        int num = 0;
        // Loop to assign values to cards
        for (int i = 0; i < cardSprites.Length; i++)
        {
            num = i;
            // Count up to the amount of cards, 52
            num %= 13;
            // if there is a remainder after x/13, then remaider
            // is used as the value, unless over 10, then use 10
            if(num > 10 || num == 0)
            {
                num = 10
            }
            cardValues[i] = num++;
        }
        currentIndex = 1;
    }
}
