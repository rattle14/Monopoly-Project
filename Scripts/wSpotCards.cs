using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class wSpotCards : MonoBehaviour
{
    public Text tTitle;
    public Text tMessage;
    
    public Image iPropertyCard;

    public Text tActionButton;
    public Text tActionButton2;
    public Button bPassButton;




    ePos focusedSpot;
    public soSpot so_Spot;
    public sPlayer s_Player;
    
    sPlayer curplayer;
    soSpot curSpot;
    GameManager gm;

    // Start is called before the first frame update
    public void InitUI( soSpot _spot)
    {
         curplayer = GameManager.gb.players[GameManager.gb.curPlayer];
         curSpot = GameManager.gb.monoSpots[(int)focusedSpot];
        gm = GameManager.gb;
        tTitle.text = "For Sale";
        tActionButton.text = "Buy";
        iPropertyCard.sprite = _spot.spotArt;
        focusedSpot = _spot.spotDesignation;
        so_Spot = _spot;
        bPassButton.gameObject.SetActive(true);
        tActionButton.gameObject.SetActive(true);

        tMessage.text = "Purchase " + _spot.nameSpot + " for $ " + _spot.propertyCost;
        switch (_spot.spotType)
        {
            case eTypeSpot.doNothing:
                tTitle.text = "Space";
                tActionButton.text = "Ok";
                focusedSpot = _spot.spotDesignation;
                iPropertyCard.sprite = _spot.spotArt;
                tMessage.text = "You Landed on: " + _spot.nameSpot;
                bPassButton.gameObject.SetActive(false);
                break;
            case eTypeSpot.property:
                sPlayer owner = so_Spot.IsPropertyOwned();
               // Debug.Log(owner.playerName);


                if (owner == GameManager.gb.players[GameManager.gb.curPlayer])
                {
                    tTitle.text = "Property";
                    tActionButton.text = "Ok";
                    tMessage.text = "You already Own: " + _spot.nameSpot;
                    bPassButton.gameObject.SetActive(false);
                }
                else if (!owner)
                {

                    tMessage.text = "Purchase " + so_Spot.nameSpot + " for $ " + so_Spot.propertyCost;

                }
                else
                {
                    if (!owner.IsPropertyMortgaged(so_Spot.spotDesignation))
                    {
                        tTitle.text = owner.playerName + " Owns this Property";
                        tActionButton.text = "Pay $ " + so_Spot.rentProperty[0];
                        tMessage.text = "you landed on: " + _spot.nameSpot + " Pay Rent: " + _spot.rentProperty[0];
                        bPassButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        tTitle.text = owner.playerName + " Owns this Property";
                        tActionButton.text = "Ok";
                        tMessage.text = _spot.nameSpot + " is currently mortgaged";
                        bPassButton.gameObject.SetActive(false);
                    }

                }
                       

                   
                break;
            case eTypeSpot.commChest:
                tTitle.text = "Community Chest!";
                tActionButton.text = "Draw";
                focusedSpot = _spot.spotDesignation;
                iPropertyCard.sprite = _spot.spotArt;
                tMessage.text = "Draw a Community chest card!";
                bPassButton.gameObject.SetActive(false);
                break;
            case eTypeSpot.chance:
                tTitle.text = "Chance!";
                tActionButton.text = "Draw";
                focusedSpot = _spot.spotDesignation;
                iPropertyCard.sprite = _spot.spotArt;
                tMessage.text = "Draw a Chance card!";
                bPassButton.gameObject.SetActive(false);
                break;
            case eTypeSpot.tax:
                tTitle.text = "Pay Tax!";
                tActionButton.text = "Pay";
                focusedSpot = _spot.spotDesignation;
                iPropertyCard.sprite = _spot.spotArt;
                tMessage.text = "Pay: " + _spot.nameSpot + " $ " + _spot.taxCost;
                bPassButton.gameObject.SetActive(false);
                break;
            case eTypeSpot.railroad:
                owner = so_Spot.IsPropertyOwned();
                // Debug.Log(owner.playerName);


                if (owner == GameManager.gb.players[GameManager.gb.curPlayer])
                {
                    tTitle.text = "RailRoad!";
                    tActionButton.text = "Ok";
                    tMessage.text = "You already Own: " + _spot.nameSpot;
                    bPassButton.gameObject.SetActive(false);
                }
                else if (!owner)
                {

                    tMessage.text = "Purchase " + so_Spot.nameSpot + " for $ " + so_Spot.propertyCost;

                }
                else
                {
                    if (!owner.IsPropertyMortgaged(so_Spot.spotDesignation))
                    {
                        tTitle.text = owner.playerName + " Owns this Property";
                        tActionButton.text = "Pay $ " + so_Spot.rentProperty[0];
                        tMessage.text = "you landed on: " + _spot.nameSpot + " Pay Rent: " + _spot.rentProperty[0];
                        bPassButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        tTitle.text = owner.playerName + " Owns this Property";
                        tActionButton.text = "Ok";
                        tMessage.text = _spot.nameSpot + " is currently mortgaged";
                        bPassButton.gameObject.SetActive(false);
                    }

                }
                break;
            case eTypeSpot.utitily:
                 owner = so_Spot.IsPropertyOwned();
                // Debug.Log(owner.playerName);


                if (owner == GameManager.gb.players[GameManager.gb.curPlayer])
                {
                    tTitle.text = "Utility";
                    tActionButton.text = "Ok";
                    tMessage.text = "You already Own: " + _spot.nameSpot;
                    bPassButton.gameObject.SetActive(false);
                }
                else if (!owner)
                {

                    tMessage.text = "Purchase " + so_Spot.nameSpot + " for $ " + so_Spot.taxCost;

                }
                else
                {
                    if (!owner.IsPropertyMortgaged(so_Spot.spotDesignation))
                    {
                        tTitle.text = owner.playerName + " Owns this Property";
                        tActionButton.text = "Pay $ " + so_Spot.taxCost;
                        tMessage.text = "you landed on: " + _spot.nameSpot + " Pay Rent: " + _spot.taxCost;
                        bPassButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        tTitle.text = owner.playerName + " Owns this Property";
                        tActionButton.text = "Ok";
                        tMessage.text = _spot.nameSpot + " is currently mortgaged";
                        bPassButton.gameObject.SetActive(false);
                    }

                }
                break;
            case eTypeSpot.goToJail:
                tTitle.text = "Jail Time!";
                tActionButton.text = "Ok";
                focusedSpot = _spot.spotDesignation;
                iPropertyCard.sprite = _spot.spotArt;
                tMessage.text = "You have been sent to Jail!";
                bPassButton.gameObject.SetActive(false);
                break;
            default:
                break;
        }


       
    }
       
    

    public void OnActionButton()
    {
   
         curSpot = GameManager.gb.monoSpots[(int)focusedSpot];
        switch ( curSpot.spotType)
        {
            case eTypeSpot.doNothing:
                Destroy(this.gameObject);
                if (!curplayer.doublesRolled)
                {
                    GameManager.gb.c_InGame.DisplayRollDice(false);
                    GameManager.gb.c_InGame.DisplayEndTurn();
                }
                else
                {
                    GameManager.gb.c_InGame.DisplayRollDice(true);
                }
                break;
            case eTypeSpot.property:
                sPlayer owner = so_Spot.IsPropertyOwned();
                //Debug.Log(owner.playerName);

                if (owner == GameManager.gb.players[GameManager.gb.curPlayer])
                {
                    Destroy(this.gameObject);
                    if (!curplayer.doublesRolled)
                    {
                        GameManager.gb.c_InGame.DisplayRollDice(false);
                        GameManager.gb.c_InGame.DisplayEndTurn();
                    }
                    else
                    {
                        GameManager.gb.c_InGame.DisplayRollDice(true);

                    }
                }
                else if (!owner)
                {

                    if (curplayer.cashOnHand > curSpot.propertyCost)
                    {
                        string title = "Purchase Property";
                       string message = "Are you sure you want to buy " + curSpot.nameSpot + " for $" + curSpot.propertyCost + " ?";
                        wMessage scr = Instantiate(gm.pMessage, this.transform).GetComponent<wMessage>();
                        scr.InitUI(title, message, "BuyProps", this.gameObject);
                    }
                    else
                    {
                        Destroy(this.gameObject);
                        if (!curplayer.doublesRolled)
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(false);
                            GameManager.gb.c_InGame.DisplayEndTurn();
                        }
                        else
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(true);

                        }
                    }
                }
                else
                {
                    if (!owner.IsPropertyMortgaged(so_Spot.spotDesignation))
                    {
                        if (curplayer.cashOnHand > curSpot.rentProperty[0])
                        {
                           
                            owner.cashOnHand += so_Spot.rentProperty[0];
                            curplayer.AdjustPlayerCash(-curSpot.rentProperty[0]);
                            curplayer.AddProperty(focusedSpot, false, 0);
                            Destroy(this.gameObject);
                            if (!curplayer.doublesRolled)
                            {
                                GameManager.gb.c_InGame.DisplayRollDice(false);
                                GameManager.gb.c_InGame.DisplayEndTurn();
                            }
                            else
                            {
                                GameManager.gb.c_InGame.DisplayRollDice(true);

                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        Destroy(this.gameObject);
                        if (!curplayer.doublesRolled)
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(false);
                            GameManager.gb.c_InGame.DisplayEndTurn();
                        }
                        else
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(true);

                        }
                    }

                }


               
                break;
            case eTypeSpot.commChest:
                Destroy(this.gameObject);
                if (!curplayer.doublesRolled)
                {
                    GameManager.gb.c_InGame.DisplayRollDice(false);
                    GameManager.gb.c_InGame.DisplayEndTurn();
                }
                else
                {
                    GameManager.gb.c_InGame.DisplayRollDice(true);
                }
                break;
            case eTypeSpot.chance:
                Destroy(this.gameObject);
                if (!curplayer.doublesRolled)
                {
                    GameManager.gb.c_InGame.DisplayRollDice(false);
                    GameManager.gb.c_InGame.DisplayEndTurn();
                }
                else
                {
                    GameManager.gb.c_InGame.DisplayRollDice(true);
                }
                break;
            case eTypeSpot.tax:
                curplayer.AdjustPlayerCash(-curSpot.taxCost);
                if (!curplayer.doublesRolled)
                {
                    GameManager.gb.c_InGame.DisplayRollDice(false);
                    GameManager.gb.c_InGame.DisplayEndTurn();
                }
                else
                {
                    GameManager.gb.c_InGame.DisplayRollDice(true);
                }
                Destroy(this.gameObject);
                break;
            case eTypeSpot.railroad:
                 owner = so_Spot.IsPropertyOwned();
                //Debug.Log(owner.playerName);

                if (owner == GameManager.gb.players[GameManager.gb.curPlayer])
                {
                    Destroy(this.gameObject);
                    if (!curplayer.doublesRolled)
                    {
                        GameManager.gb.c_InGame.DisplayRollDice(false);
                        GameManager.gb.c_InGame.DisplayEndTurn();
                    }
                    else
                    {
                        GameManager.gb.c_InGame.DisplayRollDice(true);

                    }
                }
                else if (!owner)
                {

                    if (curplayer.cashOnHand > curSpot.propertyCost)
                    {
                        string title = "Purchase Railroad";
                        string message = "Are you sure you want to buy " + curSpot.nameSpot + " for $" + curSpot.propertyCost + " ?";
                        wMessage scr = Instantiate(gm.pMessage, this.transform).GetComponent<wMessage>();
                        scr.InitUI(title, message, "BuyProps", this.gameObject);
                    }
                    else
                    {
                        Destroy(this.gameObject);
                        if (!curplayer.doublesRolled)
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(false);
                            GameManager.gb.c_InGame.DisplayEndTurn();
                        }
                        else
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(true);

                        }
                    }
                }
                else
                {
                    if (!owner.IsPropertyMortgaged(so_Spot.spotDesignation))
                    {
                        if (curplayer.cashOnHand > curSpot.rentProperty[0])
                        {

                            owner.cashOnHand += so_Spot.rentProperty[0];
                            curplayer.AdjustPlayerCash(-curSpot.rentProperty[0]);
                            curplayer.AddProperty(focusedSpot, false, 0);
                            Destroy(this.gameObject);
                            if (!curplayer.doublesRolled)
                            {
                                GameManager.gb.c_InGame.DisplayRollDice(false);
                                GameManager.gb.c_InGame.DisplayEndTurn();
                            }
                            else
                            {
                                GameManager.gb.c_InGame.DisplayRollDice(true);

                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        Destroy(this.gameObject);
                        if (!curplayer.doublesRolled)
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(false);
                            GameManager.gb.c_InGame.DisplayEndTurn();
                        }
                        else
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(true);

                        }
                    }

                }
                break;
            case eTypeSpot.utitily:
                owner = so_Spot.IsPropertyOwned();
                //Debug.Log(owner.playerName);

                if (owner == GameManager.gb.players[GameManager.gb.curPlayer])
                {
                    Destroy(this.gameObject);
                    if (!curplayer.doublesRolled)
                    {
                        GameManager.gb.c_InGame.DisplayRollDice(false);
                        GameManager.gb.c_InGame.DisplayEndTurn();
                    }
                    else
                    {
                        GameManager.gb.c_InGame.DisplayRollDice(true);

                    }
                }
                else if (!owner)
                {

                    if (curplayer.cashOnHand > curSpot.propertyCost)
                    {
                        string title = "Purchase Utility";
                        string message = "Are you sure you want to buy " + curSpot.nameSpot + " for $" + curSpot.taxCost + " ?";
                        wMessage scr = Instantiate(gm.pMessage, this.transform).GetComponent<wMessage>();
                        scr.InitUI(title, message, "BuyProps", this.gameObject);
                    }
                    else
                    {
                        Destroy(this.gameObject);
                        if (!curplayer.doublesRolled)
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(false);
                            GameManager.gb.c_InGame.DisplayEndTurn();
                        }
                        else
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(true);

                        }
                    }
                }
                else
                {
                    if (!owner.IsPropertyMortgaged(so_Spot.spotDesignation))
                    {
                        if (curplayer.cashOnHand > curSpot.taxCost)
                        {

                            owner.cashOnHand += so_Spot.taxCost;
                            curplayer.AdjustPlayerCash(-curSpot.taxCost);
                            curplayer.AddProperty(focusedSpot, false, 0);
                            Destroy(this.gameObject);
                            if (!curplayer.doublesRolled)
                            {
                                GameManager.gb.c_InGame.DisplayRollDice(false);
                                GameManager.gb.c_InGame.DisplayEndTurn();
                            }
                            else
                            {
                                GameManager.gb.c_InGame.DisplayRollDice(true);

                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        Destroy(this.gameObject);
                        if (!curplayer.doublesRolled)
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(false);
                            GameManager.gb.c_InGame.DisplayEndTurn();
                        }
                        else
                        {
                            GameManager.gb.c_InGame.DisplayRollDice(true);

                        }
                    }
                }
                
               
                    break;
            case eTypeSpot.goToJail:
                curplayer.GoToJail();
                Destroy(this.gameObject);
                gm.c_InGame.DisplayEndTurn();
                break;
            default:
                break;
        }

       // gm.c_InGame.DisplayEndTurn();
        gm.testButton.Play();

    }
    public void OnPassButton()
    {
        gm.testButton.Play();
        Destroy(this.gameObject);
        if (!curplayer.doublesRolled)
        {
            GameManager.gb.c_InGame.DisplayRollDice(false);
            GameManager.gb.c_InGame.DisplayEndTurn();
        }
        else
        {
            GameManager.gb.c_InGame.DisplayRollDice(true);
          
        }
    }
    public void BuyProps()
    {
        string title = "Purchased!";
        string message = "You have purchased " + curSpot.nameSpot + "!";
        curplayer.AdjustPlayerCash(-curSpot.propertyCost);
        curplayer.AddProperty(focusedSpot, false, 0);
        wMessage scr = Instantiate(gm.pMessage, this.transform.root.transform).GetComponent<wMessage>();
        scr.InitUI(title, message);
        Destroy(this.gameObject);
        Renderer ren = Instantiate(gm.pMarker, gm.s_Board.location[(int)focusedSpot].transform).GetComponent<Renderer>();
        ren.material.SetColor("_Color", curplayer.playerColor);
        ren.material.SetColor("_EmissionColor", curplayer.playerColor*2);
        ren.material.SetColor("_SpecColor", curplayer.playerColor);

    }
   

    // Update is called once per frame

}
