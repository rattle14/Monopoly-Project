using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCamera : MonoBehaviour
{
    public Transform target;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gb;
        gm.s_Camera = this;
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
