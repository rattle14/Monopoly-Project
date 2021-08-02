using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wConfirmMortgage : MonoBehaviour
{
    sPlayer s_player;
    GameManager gm;
    wPropertyScreen w_Pscreen;
    public Text tMessage; 
    sPlayer curplayer = GameManager.gb.players[GameManager.gb.curPlayer];
      // soSpot curSpot = GameManager.gb.monoSpots[(int)focusedSpot];

    // Start is called before the first frame update
    public void InitUI()
    {
        gm = GameManager.gb;
        s_player = gm.players[gm.curPlayer];
        w_Pscreen = GetComponentInParent<wPropertyScreen>();
        if (s_player.IsPropertyMortgaged(s_player.managePos))
        {

            tMessage.text = "Mortgage cost is $" + Mathf.CeilToInt((gm.monoSpots[(int)s_player.managePos].propertyCost / 2) * 1.1f);
           // tMessage.text = "You Have Un-mortgaged " + gm.monoSpots[(int)s_player.managePos].nameSpot + "!";
        }
        else
        {
            
            tMessage.text = "You can Mortgage " + gm.monoSpots[(int)s_player.managePos].nameSpot + " for $" + gm.monoSpots[(int)s_player.managePos].propertyCost / 2;
         // tMessage.text = "You Have mortgaged " + gm.monoSpots[(int)s_player.managePos].nameSpot + "!";
        }

    }

    public void OnYesButton()
    {
        Debug.Log(s_player.managePos);
        if (s_player.IsPropertyMortgaged(s_player.managePos))
        {
            s_player.SetMortgageProperty(s_player.managePos, false);

            w_Pscreen.UpdateUI();
            s_player.AdjustPlayerCash(-Mathf.CeilToInt((gm.monoSpots[(int)s_player.managePos].propertyCost / 2) * 1.1f));

        }
        else
        {
            s_player.SetMortgageProperty(s_player.managePos, true);
            w_Pscreen.UpdateUI();
            s_player.AdjustPlayerCash(gm.monoSpots[(int)s_player.managePos].propertyCost / 2);
        }
        gm.testButton.Play();
        Destroy(this.gameObject);
    }
    public void OnNoButton()
    {
        Destroy(this.gameObject);
        gm.testButton.Play();
    }
}
