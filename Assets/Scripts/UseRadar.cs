using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRadar : MonoBehaviour
{
    public Transform player, vent, current;
    public float difference;
    void Update()
    {
        if(player.gameObject.active == false){
            current = vent;
        }else{
            current = player;
        }
        transform.position = new Vector3(current.position.x - difference, transform.position.y, current.position.z);
    }
}