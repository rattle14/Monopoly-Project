using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wDice : MonoBehaviour
{
    public Image Dice1;
    public Image Dice2;
    public sPlayer s_Player;
    public Sprite[] dicePics;
    // Start is called before the first frame update
    public void InitUI()
    {
        s_Player = GameManager.gb.players[GameManager.gb.curPlayer];
        DiceImage();
    }

   
    public void DiceImage()
    {
        Debug.Log(s_Player.dice0);
       
                Dice1.sprite = dicePics[s_Player.dice0-1];
                Dice2.sprite = dicePics[s_Player.dice1 - 1];


    }
}
