using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    public string carrying;
    public Transform camera;
    public LayerMask interact;
    public GameObject item, canvas;
    public GameObject[] plateIcons;
    public Sprite placedIcon;
    public AudioSource grab, place;
    public int placed = 0;
    int i = 0;
    void Start(){
        plateIcons = GameObject.FindGameObjectsWithTag("PlateIcon");
        foreach(GameObject obj in plateIcons){
            plateIcons[i].SetActive(false);
            Debug.Log(plateIcons[i].name);
            i++;
        }
    }
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, camera.forward, out hit, 3, interact)){
            if(hit.collider.name == "Plate"){
                if(item == null){
                    if(Input.GetKeyDown(KeyCode.E)){
                        item = hit.collider.gameObject;
                        item.transform.position = new Vector3(0, -99, 0);
                        grab.Play();
                        plateIcons[placed].SetActive(true);
                    }
                }
            }
            if(hit.collider.name == "Table"){
                if(Input.GetKeyDown(KeyCode.E)){
                    if(item != null){
                        place.Play();
                        item.transform.position = new Vector3(hit.transform.position.x, 2.788f, hit.transform.position.z);
                        hit.collider.name = "TablePlaced";
                        Debug.Log("placed");
                        placed++;
                        item = null;
                        plateIcons[placed].GetComponent<Image>().sprite = placedIcon;
                    }
                }
            }
        }
    }
}