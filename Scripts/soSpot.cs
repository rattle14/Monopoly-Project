using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Spot", menuName = "Create Spot")]

public class soSpot : ScriptableObject
{
    public string nameSpot;
    public ePos spotDesignation;
    public eTypeSpot spotType;
    public int taxCost;
    public int propertyCost;
    public int houseCost;
    public int costOfImprovement;
    public int[] rentProperty;
    public soSpot[] otherPropertiesInGroup;
    public Sprite spotArt;
    public Sprite backArt;

    public sPlayer IsPropertyOwned()
    {
        GameManager gm = GameManager.gb;
        for (int i = 0; i < gm.players.Length; i++)
        {
            if (gm.players[i].IsPropertyOwned(spotDesignation))
            {
                return gm.players[i];
            }
        }
        return null;
    }



}
