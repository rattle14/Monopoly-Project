using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class PropertyOwnership
{
    public string name;
    public ePos position;
    public bool isMortgaged;
    public int houseAmt;

    public PropertyOwnership(ePos _position, bool _isMortgaged, int _houseAmt)
    {
        position = _position;
        name = GameManager.gb.monoSpots[(int)position].nameSpot;
        isMortgaged = _isMortgaged;
        houseAmt = _houseAmt;
    }

}





public class sPlayer : MonoBehaviour
{ 
  public  int index;
    public ePlayerType typePlayer;
    public eDifficulty difficult;
public int cashOnHand;
public string playerName;
public Color playerColor;
   public ePiece piece;
public ePos playerPos;
    
    public soSpot so_Spot;
    

    public bool[] hasGetOutOfJail;
    public bool isInJail;
    public int turnsJail;
    public int amtOfDoubles;
    public int dice0;
    public int dice1;
    public bool doublesRolled = false;
    public ePiece whichPiece;
    public GameObject modelPiece;
   public ePos managePos;
    public sPlayer rivalPlayer;
    public sCamera s_Camera;
    public cInGame c_InGame;

    public Vector3 startPos;
    public Vector3 endPos;
    public Transform[] positionArray;
   

    GameManager gm;

    public List<PropertyOwnership> listProperties;
    //public PropertyOwnership[] propsOwned;
     

    bool isBankrupt;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gb;
       

    }
    public void RollDice()
    {
          dice0 = Random.Range(1, 7);
          dice1 = Random.Range(1, 7);

        int distance = dice0 + dice1;
        positionArray = new Transform[distance];
    if (dice0 == dice1)
        {
            amtOfDoubles++;
            doublesRolled = true;
            
            if (amtOfDoubles > 2)
            {
                GoToJail();
                gm.c_InGame.DisplayRollDice(false);
                gm.c_InGame.DisplayEndTurn();
                // if (gm.c_InGame.w_DiceInstances != null)
                //  Destroy(gm.c_InGame.w_DiceInstances.gameObject);
                
                return;
            }
        } else
        {
            doublesRolled = false;
            amtOfDoubles = 0;
        }

        int newPos = (int)playerPos + distance;


        if (newPos >= (int)ePos.terminator)
        {
           
            for (int i = 0; i < distance; i++)
            {
                int ipos = (int) playerPos + 1 + i;
                if (ipos >= (int)ePos.terminator)
                {
                    positionArray[i] = gm.s_Board.location[ipos - (int)ePos.terminator];
                }
                else
                {
                    positionArray[i] = gm.s_Board.location[ipos];
                }

                
            }
            playerPos = newPos - ePos.terminator;
            AdjustPlayerCash(200);
        }
        else
        {
            for (int i = 0; i < distance; i++)
            {
                positionArray[i] = gm.s_Board.location[(int)playerPos + i + 1];


            }

            playerPos += distance;
        }
       
        MovePlayerPiece();
      //  LandOnSpot();
        
        


    }
    public void AdjustPlayerCash(int _amount)
    {
        cashOnHand += _amount;
        gm.c_InGame.playerHUD[System.Array.IndexOf(gm.players, this)].money.text = "$" + cashOnHand;

    }

    public void LandOnSpot()
    {
        Debug.Log("Name of Property: " + GameManager.gb.monoSpots[(int)playerPos].nameSpot);
        GameManager.gb.c_InGame.DisplaySpotWidget(playerPos);
    }
    public void GoToJail()
    {
        isInJail = true;
        playerPos = ePos.justVisiting;
        StartCoroutine("GoToPos");
        //Destroy(gm.c_InGame.w_DiceInstances.gameObject);
    }
    
    public void AddProperty(ePos _position, bool _isMortgaged, int _houseAmt)
    {
        listProperties.Add(new PropertyOwnership(_position, _isMortgaged, _houseAmt));
    }
    public bool IsPropertyOwned(ePos _PropertyToCheck) 
    {
        for (int i = 0; i < listProperties.Count; i++)
        {
            if (listProperties[i].position == _PropertyToCheck)
            {
                return true;
            }
        }
        return false;
    }
   /* public void OnSellButton()
    {

        AdjustPlayerCash(GameManager.gb.monoSpots[(int)managePos].propertyCost);
        rivalPlayer.AdjustPlayerCash(-GameManager.gb.monoSpots[(int)managePos].propertyCost);
        foreach (PropertyOwnership i in listProperties)
        {
            if (i.position == managePos)
            {
                
                rivalPlayer.AddProperty(managePos, i.isMortgaged, 0);
                listProperties.Remove(i);
            }

        }


    }*/
    public void SetMortgageProperty (ePos _property, bool _activate)
    {
        for (int i = 0; i < listProperties.Count; i++)
        {
           if (listProperties[i].position == _property)
            {
                listProperties[i].isMortgaged = _activate;
                return;
            }
        }
    }
    public bool IsPropertyMortgaged (ePos _property)
    {
        for (int i = 0; i < listProperties.Count; i++)
        {
            if (listProperties[i].position == _property)
            {
                return listProperties[i].isMortgaged;
            }
        }
        return false;
    }
    public void RemoveProperty(ePos _position)
    {
        foreach (PropertyOwnership i in listProperties)
        {
            if (i.position == _position)
            {
                listProperties.Remove(i); 
            }
            
        }
    } 

     int  SortByEpos(PropertyOwnership _pos1, PropertyOwnership _pos2)
    {
     
        return _pos1.position.CompareTo(_pos2.position);
    }
    public void SortPropertiesList()
    {
        listProperties.Sort(SortByEpos);
    }
   

    public void MovePlayerPiece()
    {
        StartCoroutine("StepPos");
    }

    IEnumerator GoToPos()
    {
       // modelPiece.transform.position = startPos;
        Vector3 currentPos = startPos;
        Vector3 newPos;

        float elapsedTime = 0f;
        float waitTime = 3f;
        if (isInJail)
        {
            modelPiece.transform.position = gm.s_Board.jail.transform.position;
            modelPiece.transform.rotation = gm.s_Board.jail.transform.rotation;
        } else
        {
             newPos = gm.s_Board.location[(int)playerPos].position;
            while (elapsedTime < waitTime)
            {
                modelPiece.transform.position = Vector3.Lerp(currentPos, newPos, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
           

    } 
    IEnumerator StepPos()
    {
        startPos = modelPiece.transform.position;
        int steps = positionArray.Length;
        int counter = 0;

        while (steps > counter)
        {
            Vector3 currentPos = modelPiece.transform.position;
            Vector3 newPos = positionArray[counter].position;
            Quaternion currentRot = modelPiece.transform.rotation;
            Quaternion newRot = positionArray[counter].rotation;

            float elapsedTime = 0f;
            float waitTime = 0.3f;

            while (elapsedTime < waitTime)
            {

                modelPiece.transform.position = Vector3.Lerp(currentPos, newPos, (elapsedTime / waitTime));
                modelPiece.transform.rotation = Quaternion.Lerp(currentRot, newRot, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds (.5f);
            counter++;

        }
        if (gm.c_InGame.w_DiceInstances != null)
            Destroy(gm.c_InGame.w_DiceInstances.gameObject);
        LandOnSpot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
