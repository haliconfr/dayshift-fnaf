using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRadar : MonoBehaviour
{
    bool hidden;
    public GameObject radar;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)){
            if(!hidden){
                radar.SetActive(false);
                hidden = true;
            }else{
                radar.SetActive(true);
                hidden = false;
            }
        }
    }
}