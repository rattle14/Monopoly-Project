using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wPlayerInfo : MonoBehaviour
{
    private GameManager gm;

    public int playerIndex;
    private sPlayer s_Player;
    private cSetup c_Setup;
    public Image iPiece;
    public Text tPiece;

    public Image iPlayertype;
    public Text tPlayerType;

    public GameObject panelPlayerSetup;
    public GameObject panelAddPlayer;

    public Image[] iButtonPlayerColor;

    public void InitUI(int _playerIndex, cSetup _cSetup)
    {
        gm = GameManager.gb;
        c_Setup = _cSetup;
        playerIndex = _playerIndex;
         s_Player = gm.players[_playerIndex];
        foreach (Image i in iButtonPlayerColor)
        {
            i.color = gm.playerColors[_playerIndex];
        }
    
        s_Player.index = playerIndex;
        s_Player.playerColor = gm.playerColors[playerIndex];
        Debug.Log("calling default state");
        setDefaultState();
    }
    void setDefaultState()
    {
        Debug.Log("default state called");
        panelAddPlayer.SetActive(true);
        panelPlayerSetup.SetActive(false);

        s_Player.piece = (ePiece)playerIndex;

        iPiece.sprite = gm.sprPieces[playerIndex];
        tPiece.text = gm.strPiece[playerIndex];

        iPlayertype.sprite = gm.sprPlayerTypes[(int)ePlayerType.human];
        tPlayerType.text = gm.strPlayerTypes[(int)ePlayerType.human];
        CheckForStartButton();
    }
   public void OnAddPlayerClicked()
    {
        panelAddPlayer.SetActive(false);
        panelPlayerSetup.SetActive(true);

        s_Player.typePlayer = ePlayerType.human;

        gm.numberOfPlayers++;
        gm.numHumanPlayers++;
       
        CheckForStartButton();
        
    }

    public void CheckForStartButton()
    {
        c_Setup.DisplayStartGame(gm.numHumanPlayers > 0 && gm.numberOfPlayers > 1);
    }
    public void CheckForUniquePiece()
    {
        bool checkAgain = false;
        for (int i = 0; i < GameManager.maxNumPlayers; i++)
        {
            if (s_Player.piece == gm.players[i].piece && i != playerIndex && gm.players[i].typePlayer != ePlayerType.none)
            {
                s_Player.piece++;
                if (s_Player.piece == ePiece.terminator)
                {
                    s_Player.piece = 0;
                }
                checkAgain = true;
                break;
            }
        }
        if (checkAgain)
        {
            CheckForUniquePiece();
        }
    }
    public void OnPieceClicked()
    {
        s_Player.piece++;
        if (s_Player.piece == ePiece.terminator)
        {
            s_Player.piece = 0;
        }
        CheckForUniquePiece();

        SetPlayerPieceArt();
    }

    public void OnPlayerTypeClicked()
    {
        s_Player.typePlayer++;

        if (s_Player.typePlayer > ePlayerType.AI)
        {
            s_Player.typePlayer = ePlayerType.none;
            gm.numberOfPlayers--;
            setDefaultState();
        } 
        else
        {
            if (s_Player.typePlayer == ePlayerType.AI)
            {
                gm.numHumanPlayers--;
            }
            SetPlayerTypeArt();

        }
        CheckForStartButton();
        
    }

    void SetPlayerTypeArt()
    {
        iPlayertype.sprite = gm.sprPlayerTypes[(int)s_Player.typePlayer];
        tPlayerType.text = gm.strPlayerTypes[(int)s_Player.typePlayer];
    }

    void SetPlayerPieceArt()
    {
        iPiece.sprite = gm.sprPieces[(int)s_Player.piece];
        tPiece.text = gm.strPiece[(int)s_Player.piece];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
