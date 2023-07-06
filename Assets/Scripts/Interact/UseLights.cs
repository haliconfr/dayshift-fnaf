using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class UseLights : MonoBehaviour
{
    public LayerMask interact;
    public GameObject lights, timer;
    bool lightStatus;
    public Volume volume;
    public float exposureOff;
    public float exposureOn;
    public FreddyAI fredai;
    void Update()
    {
        Debug.DrawRay(transform.position, Camera.main.transform.forward, Color.green, 3);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 3, interact)){
            if(Input.GetKeyDown(KeyCode.E)){
                if(hit.collider.name == "Lights"){
                    if(!timer.active){
                        hit.collider.GetComponent<Animator>().Play("LightTimer");
                        StartCoroutine(lightsOn());
                    }
                }
            }
        }
    }
    IEnumerator lightsOn(){
        lightStatus = true;
        StartCoroutine(changeExposure());
        lights.SetActive(true);
        fredai.LightsOn();
        timer.SetActive(true);
        yield return new WaitForSeconds(6.177f);
        lightStatus = false;
        StartCoroutine(changeExposure());
        lights.SetActive(false);
        yield return new WaitForSeconds(6f);
        timer.SetActive(false);
    }
    IEnumerator changeExposure(){
        yield return new WaitForSeconds(0.05f);
        VolumeProfile volumeProfile = volume.sharedProfile;
        volumeProfile.TryGet<Exposure>(out var exp);
        if(lightStatus == true){
            exp.limitMin.value = exposureOn;
        }
        if(lightStatus == false){
            exp.limitMin.value = exp.limitMin.value + 0.1f;
            if(exp.limitMin.value <= exposureOff){
                StartCoroutine(changeExposure());
            }
        }
    }
}