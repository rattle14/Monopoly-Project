using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gb;
    }
    public void OnStartGameClicked()
    {
        Debug.Log("On Start clicked");
        GameObject obj = Instantiate(gm.pCanvasSetup);
        cSetup scr = obj.GetComponent<cSetup>();
        gm.testButton.Play();
        scr.InitUI();
    }
    public void OnOptionsClicked()
    {
        Debug.Log("On Options clicked");
        GameObject obj = Instantiate(gm.pCanvasOptions);
        cOptions scr = obj.GetComponent<cOptions>();
        gm.testButton.Play();
        //scr.InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
