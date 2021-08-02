using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cSetup : MonoBehaviour
{
    private GameManager gm;
    public GameObject w_PlayerInfo;
    public Transform panelWidget;
    public Button bStartGame;
    // Start is called before the first frame update
    void Awake()
    {
        gm = GameManager.gb;
    }
    public void InitUI()
    {
        for (int i = 0; i < GameManager.maxNumPlayers; i++)
        {
            wPlayerInfo scr = Instantiate(w_PlayerInfo, panelWidget).GetComponent<wPlayerInfo>();
            scr.InitUI(i, this);
        }
       // wPlayerInfo scr = Instantiate(w_PlayerInfo, panelWidget).GetComponent<wPlayerInfo>();
       // Debug.Log(gm);
       // scr.InitUI(gm.numberOfPlayers, this);

    }
    public void DisplayStartGame (bool _active)
    {
        bStartGame.gameObject.SetActive(_active);
    }
    public void OnStartGameClicked()
    {
        Debug.Log("On Start clicked");
        gm.testButton.Play();
        GameManager.gb.LoadScene(1);
    }

    public void OnBackButtonClicked()
    {
        gm.testButton.Play();
        Destroy(gameObject);
        gm.numberOfPlayers = 0;
        gm.numHumanPlayers = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
