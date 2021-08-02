using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wMessage : MonoBehaviour
{
    // Start is called before the first frame update
    public Text tTitle;
    public Text tMessage;
    GameManager gm;
    string action;
    GameObject sendObject;
    public GameObject YesButton;
    public GameObject NoButton;
    public GameObject ConfirmButton;



    public void InitUI(string _tTitle, string _tMessage, string _action, GameObject _sendObject)
    {
        gm = GameManager.gb;

        tTitle.text = _tTitle;
        tMessage.text = _tMessage;
        action = _action;
        sendObject = _sendObject;        
        
      
    }
    public void InitUI(string _tTitle, string _tMessage)
    {
        gm = GameManager.gb;
        tTitle.text = _tTitle;
        tMessage.text = _tMessage;

        YesButton.SetActive(false);
        NoButton.SetActive(false);
        ConfirmButton.SetActive(true);
    }
    public void DoNothing()
    {
        Destroy(this.gameObject);
        
    }
    public void OnConfirmButton()
    {
        if (sendObject)
        {
            sendObject.SendMessage(action);
        }
        if (!gm.players[gm.curPlayer].doublesRolled)
        {
            GameManager.gb.c_InGame.DisplayRollDice(false);
            GameManager.gb.c_InGame.DisplayEndTurn();
        }
        else
        {
            GameManager.gb.c_InGame.DisplayRollDice(true);
        }
        gm.cashSound.Play();
        Destroy(this.gameObject);
    }
    public void OnCancelButton()
    {
        Destroy(this.gameObject);
        gm.testButton.Play();
    }
}
