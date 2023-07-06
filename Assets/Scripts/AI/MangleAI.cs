using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MangleAI : MonoBehaviour
{
    public GameObject jumpscarer;
    public List<Transform> idlepositions;
    public Transform currentidlepos, main;
    public LayerMask layer;
    public NavMeshAgent AI;
    void Start(){
        currentidlepos = idlepositions[Random.Range(0, idlepositions.Count)];
        AI.SetDestination(currentidlepos.position);
    }
    void Turn()
    {
        //main.eulerAngles = new Vector3(main.eulerAngles.x, main.eulerAngles.y - 180, main.eulerAngles.z);
    }
    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.name == currentidlepos.gameObject.name){
            Transform oldpos = collider.transform;
            idlepositions.Remove(oldpos);
            currentidlepos = idlepositions[Random.Range(0, idlepositions.Count)];
            idlepositions.Add(oldpos);
            AI.SetDestination(currentidlepos.position);
            if(oldpos.name == "deadend"){
                AI.speed = 0;
                this.GetComponent<Animator>().Play("Turn");
                StartCoroutine(turnwait());
            }
        }
    }
    void OnCollisionEnter(Collision collision){
        Debug.Log("collided with " + collision.gameObject.name);
        if(collision.gameObject.name.Contains("layer")){
            collision.gameObject.SetActive(false);
            jumpscarer.transform.position = main.position;
            jumpscarer.SetActive(true);
            jumpscarer.GetComponentsInChildren<Animator>()[0].Play("Jumpscare");
            //jumpscarer.GetComponent<Animator>().Play("Jumpscare");
            main.gameObject.SetActive(false);
        }
    }
    IEnumerator turnwait(){
        yield return new WaitForSeconds(1f);
        AI.speed = 10;
        AI.SetDestination(currentidlepos.position);
    }
}