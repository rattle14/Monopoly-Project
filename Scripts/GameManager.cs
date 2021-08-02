using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Audio;
public class GameManager : MonoBehaviour
{
    public static GameManager gb;

    public const int maxNumPlayers = 4;
    public sPlayer[] players;
public int numberOfPlayers;
    public int numHumanPlayers;
     public int curPlayer;
    [NamedArray(typeof(ePos))] public string[] namesOfProps;

    [Space]
[Header("Game Settings")]
 public float SoundVolume;
public float MusicVolume;
public bool isPlayAnimation = true;
    public float Brightness;
    public bool isTutorialFinished;
public bool areSubtitlesActive;
    public float soundVol;
    public float musicVol;
    public float brightnessLevel;
  


    [Space]
    [Header("House Rules")]
public bool houseFreePark;
public bool houseExtraGo;

    [Space]
    [Header("Prefabs")]
    public GameObject pCanvasMainMenu;
    public GameObject pCanvasSetup;
    public GameObject pCanvasOptions;
    public GameObject pInGame;
    public GameObject pPlayer;
    public GameObject pMarker;
    public GameObject pMessage;
    public GameObject pDiceRoll;
    public sBoard s_Board;
    public sCamera s_Camera;
 

    [Space]
    [Header("House Rules")]
    public MainMenu c_MainMenu;
    public cInGame c_InGame;
   

    [Space]
    [Header("Instantiated objects")]
    private MainMenu MainMenu;

    [Space]
    [Header("Assets")]
    public String[] playerNames;
    public Color[] playerColors;
    [NamedArray(typeof(ePiece))] public GameObject[] modelPieces;
    [NamedArray(typeof(ePiece))] public Sprite[] sprPieces;
    [NamedArray(typeof(ePiece))] public String[] strPiece = { "Car", "Thimble", "Tophat", "Boot", "Wheelbarrow", "Dog", "Cat", "Battleship" };
    [NamedArray(typeof(ePlayerType))] public Sprite[] sprPlayerTypes;
    [NamedArray(typeof(ePlayerType))] public String[] strPlayerTypes = { "none", "Human", "AI" };

    [Space]
    [Header("Audio")]
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup masterMixer;
    public AudioSource testButton;
    public AudioSource mainTheme;
    public AudioSource rollDice;
    public AudioSource cashSound;

    [Space]
    [Header("Lookups")]
    [NamedArray(typeof(ePos))]   public soSpot[] monoSpots;

    public float musicValue = 0.8f;
    public float soundValue = 0.8f;


    private void Awake()
    {
        if (gb == null)
        {
            DontDestroyOnLoad(gameObject);
            gb = this;
        }
        else if (gb != this)
        {
            Destroy(gameObject);
        }
    }
        void Start()
    {

        SetPLayerDefaults();
        
        Screen.SetResolution(1024, 768, true);

        // ChangeMusicVol(-80f)
    }
    /*void ChangeMusicVol(float _newVal)
    {
        musicMixer.audioMixer.SetFloat("MusicVol", _newVal);
        
    } */


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene has loaded: " + scene.name);
        if (scene.buildIndex == 0)
        {          
                c_MainMenu = Instantiate(pCanvasMainMenu).GetComponent<MainMenu>();                              
        }
        else if (scene.buildIndex == 1)
        {
            cInGame scr = Instantiate(pInGame).GetComponent<cInGame>();
            c_InGame = scr;
            //scr.InitUI();

        } 
    }
    public void LoadScene (int _idx)
    {
        SceneManager.LoadScene(_idx);
    }
    
    public void SetPLayerDefaults()
    {
        players = new sPlayer[maxNumPlayers];
        for (int i = 0; i < maxNumPlayers; i++)
        {
           
            players[i] = Instantiate(pPlayer, this.gameObject.transform).GetComponent<sPlayer>();
            players[i].playerName = "player: " + (i + 1);
            players[i].cashOnHand = 1500;
            
        }

        playerColors = new Color[maxNumPlayers];
        playerColors[0] = Color.blue;
        playerColors[1] = Color.yellow;
        playerColors[2] = Color.red;
        playerColors[3] = Color.green;

        /*playerNames = new String[maxNumPlayers];
        playerNames[0] = "Player1";
        playerNames[1] = "Player2";
        playerNames[2] = "Player3";
        playerNames[3] = "Player4";*/
    }

    // Update is called once per frame
    public void AdvancePlayer()
    {
        curPlayer++;
        if (curPlayer >= numberOfPlayers)
        {
            curPlayer = 0;
        }
        s_Camera.target = players[curPlayer].modelPiece.transform;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            LoadScene(1);
        }
    }
}
