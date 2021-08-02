using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sBoard : MonoBehaviour
      
{
    public Transform jail;
    [NamedArray(typeof(ePos))] public Transform[] location;
    private void Start()
    {
        GameManager.gb.s_Board = this;
    }

}
