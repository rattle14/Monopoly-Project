using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cInGame : MonoBehaviour
{
    GameManager gm;
    public Button bRollDice;
    public Button bEndTurn;
    public Image iPiece;
    public Image playerBG;
    public Image statsBG;
    private sPlayer s_Player;
    public Text cash;
    public Text currentPlayer;

    public wPlayerHud[] playerHUD;
    public GameObject w_playerHUDprefab;
    public GameObject w_spotCardsprefab;
    public GameObject w_managePropetyprefab;
    public GameObject w_diceRoll;
    public wDice w_DiceInstances;
    public Transform panelWidget;

   


    void Start()
    {
        
        gm = GameManager.gb;
        
        s_Player = gm.players[gm.curPlayer];
        playerHUD = new wPlayerHud[GameManager.maxNumPlayers];
        InitUI();
    }   

    public void InitUI()
    {
        for (int i = 0; i < gm.numberOfPlayers; i++)
        {
            playerHUD[i] = Instantiate(w_playerHUDprefab, panelWidget).GetComponent<wPlayerHud>();
            playerHUD[i].playerIcon.sprite = gm.sprPieces[(int)gm.players[i].piece];
            playerHUD[i].playerName.text = gm.players[i].playerName;
            playerHUD[i].money.text = "$" + gm.players[i].cashOnHand;
            playerHUD[i].playerBG.color = gm.players[i].playerColor;

            gm.players[i].modelPiece = Instantiate(gm.modelPieces[(int)gm.players[i].piece], gm.s_Board.location[0].transform.position, gm.s_Board.location[0].transform.rotation);
        }
        playerHUD[gm.curPlayer].playerIndicate.gameObject.SetActive(true);
        gm.s_Camera.target = gm.players[gm.curPlayer].modelPiece.transform;

        // UpdatePlayerUI();
        DisplayRollDice(true);
    }

 public void OnRollClick()
    {    
       gm.players[gm.curPlayer].RollDice();
        
        gm.rollDice.Play();
        DisplayRollDice(false);
        
        DisplayDiceWidget();
    }
    public void OnOptionsClick()
    {
        GameObject obj = Instantiate(gm.pCanvasOptions);
        cOptions scr = obj.GetComponent<cOptions>();
        scr.InitUI();
        gm.testButton.Play();
    }
    public void OnEndTurnButton()
    {
        gm.AdvancePlayer();
        s_Player = gm.players[gm.curPlayer];
        gm.c_InGame.DisplayRollDice(true);
        PlayerHighlight();
        bEndTurn.gameObject.SetActive(false);

        gm.testButton.Play();
        if (w_DiceInstances != null)
            Destroy(w_DiceInstances.gameObject);
        // UpdatePlayerUI();
        //UpdatePlayerCash();
    }
    public void OnManageButton()
    {
        if (s_Player.listProperties.Count == 0)
        {
            string title = "Uh Oh!";
            string message = "You don't have any properties!";
            wMessage scri = Instantiate(gm.pMessage, this.transform.root.transform).GetComponent<wMessage>();
            scri.InitUI(title, message);
        } else
        {
         wPropertyScreen scr = Instantiate(w_managePropetyprefab, this.gameObject.transform).GetComponent<wPropertyScreen>();
        scr.InitUI();
        }
        gm.testButton.Play();
    }
    public void DisplayEndTurn()
    {
        bRollDice.gameObject.SetActive(false);
        bEndTurn.gameObject.SetActive(true);
        
    }
    public void DisplayRollDice(bool _active)
    {
        bRollDice.gameObject.SetActive(_active);
       // bEndTurn.gameObject.SetActive(!_active);

    }

    public void OnBackClicked()
    {
        gm.testButton.Play();
        GameManager.gb.LoadScene(0);
    }
    public void PlayerHighlight()
    {
        for (int i = 0; i < gm.numberOfPlayers; i++)
        {
            playerHUD[i].playerIndicate.gameObject.SetActive(false);
        }
        playerHUD[gm.curPlayer].playerIndicate.gameObject.SetActive(true);
    }
    public void DisplaySpotWidget(ePos _spot)
    {
        wSpotCards scr = Instantiate(w_spotCardsprefab, this.gameObject.transform).GetComponent<wSpotCards>();
        scr.InitUI(GameManager.gb.monoSpots[(int)_spot]);
    }
    public void DisplayDiceWidget()
    {
         w_DiceInstances = Instantiate(w_diceRoll, this.gameObject.transform).GetComponent<wDice>();
        w_DiceInstances.InitUI();
    }
    /*public void UpdatePlayerUI()
    {
        
        iPiece.sprite = gm.sprPieces[(int)s_Player.piece];
        currentPlayer.text = s_Player.playerName;
        playerBG.color = s_Player.playerColor;
        statsBG.color = s_Player.playerColor;
        UpdatePlayerCash();

    }*/








}
