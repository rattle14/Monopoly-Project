using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wPropertyScreen : MonoBehaviour
{
    public Text tTitle;
    public Text tMessage;
    public Button prev;
    public Button next;
    public Button mortgage;
    public Image iPropertyCard;
    public int listIndex;

    public Text tMortgageButton;
    
    
    sPlayer s_player;
     GameManager gm;
    
    // Start is called before the first frame update
    public void InitUI()
    {
        gm = GameManager.gb;
        s_player = gm.players[gm.curPlayer];
        s_player.SortPropertiesList();
        s_player.managePos = s_player.listProperties[0].position;       
        prev.gameObject.SetActive(false);
        if (s_player.listProperties.Count == 1)
        {
          next.gameObject.SetActive(false);
        }
        
        listIndex = 0;
        UpdateUI();
      
    }
    public void UpdateUI()
    {
        s_player.SortPropertiesList();


         if (s_player.IsPropertyMortgaged(s_player.managePos)) 
        {
          iPropertyCard.sprite = gm.monoSpots[(int)s_player.managePos].backArt;
            tMortgageButton.text = "UnMortage";
           // tMessage.text = "Do you want to unmortgage " + gm.monoSpots[(int)s_player.managePos].nameSpot;
        } else
        {
            iPropertyCard.sprite = gm.monoSpots[(int)s_player.managePos].spotArt;
            tMortgageButton.text = "Mortage";
           // tMessage.text = "Do you want to mortgage this property " + gm.monoSpots[(int)s_player.managePos].nameSpot;
        }
        if (listIndex == 0)
        {
            
            prev.gameObject.SetActive(false);
            next.gameObject.SetActive(false);
            if (s_player.listProperties.Count > 1)
            {
               next.gameObject.SetActive(true);
            }
            

        }
        else if (listIndex == s_player.listProperties.Count - 1)
        {
            prev.gameObject.SetActive(true);
            next.gameObject.SetActive(false);
        }
        else 
        {
            prev.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
        }
    }
    public void OnMortgageButton()
    {
        //  wConfirmMortgage scr = Instantiate(w_Confirm, this.transform).GetComponent<wConfirmMortgage>();
         
        wMessage scr = Instantiate(gm.pMessage, this.transform).GetComponent<wMessage>();
        if (s_player.IsPropertyMortgaged(s_player.managePos))
        {
            string title = "Un-Mortgage Property?";

             
            string message = "Mortgage cost is $" + Mathf.CeilToInt((gm.monoSpots[(int)s_player.managePos].propertyCost / 2) * 1.1f);
            scr.InitUI(title, message, "MortgageProps", this.gameObject);
        } else
        {
            string title = "Mortgage Property?";


            string message = "You can Mortgage " + gm.monoSpots[(int)s_player.managePos].nameSpot + " for $" + gm.monoSpots[(int)s_player.managePos].propertyCost / 2;
            scr.InitUI(title, message, "MortgageProps", this.gameObject);
            
        }
            
    }
  
    public void OnBackButton()
    {
        gm.testButton.Play();
        Destroy(this.gameObject);
    }
    public void OnNextButton()
    {
        listIndex++;
        s_player.managePos = s_player.listProperties[listIndex].position;
        gm.testButton.Play();
        UpdateUI();
        
    }
    public void OnPrevButton()
    {
        listIndex--;
        s_player.managePos = s_player.listProperties[listIndex].position;
        gm.testButton.Play();
        UpdateUI();
    }
    public void MortgageProps()
    {
        
        wMessage scr = Instantiate(gm.pMessage, this.transform).GetComponent<wMessage>();
        Debug.Log(s_player.managePos);
        if (s_player.IsPropertyMortgaged(s_player.managePos))
        {
            string title = "Property Un-Mortgaged";
            string message = "You Have Un-mortgaged " + gm.monoSpots[(int)s_player.managePos].nameSpot + "!";
            s_player.SetMortgageProperty(s_player.managePos, false);
            scr.InitUI(title, message);
            UpdateUI();
            s_player.AdjustPlayerCash(-Mathf.CeilToInt((gm.monoSpots[(int)s_player.managePos].propertyCost / 2) * 1.1f));

        }
        else
        {
            string title = "Property Mortgaged!";
                string message = "You Have mortgaged " + gm.monoSpots[(int)s_player.managePos].nameSpot + "!";
            s_player.SetMortgageProperty(s_player.managePos, true);
            scr.InitUI(title, message);
            UpdateUI();
            s_player.AdjustPlayerCash(gm.monoSpots[(int)s_player.managePos].propertyCost / 2);
        }
        
    }
}
