using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseVents : MonoBehaviour
{
    public GameObject player, vent;
    public Transform cam, playerCam;
    public bool inVent;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "VentTrigger"){
            if(inVent){
                player.transform.position = this.gameObject.transform.position;
                player.transform.rotation = Quaternion.LookRotation(collider.transform.forward);
                player.transform.Translate(0,1,3);
                vent.transform.rotation = Quaternion.LookRotation(-collider.transform.forward);
                player.SetActive(true);
                vent.SetActive(false);
            }else{
                //vent.transform.position = new Vector3(collider.transform.position.x, 4.579446f, collider.transform.position.z);
                vent.transform.Translate(0,0,3);
                vent.SetActive(true);
                player.SetActive(false);
            }
        }
    }
}