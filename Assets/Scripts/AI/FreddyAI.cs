using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreddyAI : MonoBehaviour
{
    public NavMeshAgent AI;
    public Transform player, main, R_Eye, L_Eye;
    public LayerMask layer;
    public GameObject jumpscarer;
    public bool able, singing;
    public Animator animator;
    public bool running, scared;
    public List<Transform> idlepositions;
    float normSpeed;
    public float runSpeed;
    public Transform currentidlepos;
    Transform playerPosition;
    void Start(){
        currentidlepos = idlepositions[Random.Range(0, idlepositions.Count)];
        if(currentidlepos == idlepositions[5]){
            currentidlepos = idlepositions[Random.Range(0, idlepositions.Count)];
        }
    }
    public void move(){
        singing = false;
        able = true;
        StartCoroutine(dontmove());
    }
    void Update()
    {
        if(able == true){
            AI.SetDestination(currentidlepos.position);
        }else{
            AI.SetDestination(transform.position);
        }
        if(Physics.Raycast(R_Eye.position, R_Eye.forward, 20, layer)){
            if(!scared && !singing){
                Scare();
            }
        }
        if(Physics.Raycast(L_Eye.position, L_Eye.forward, 20, layer)){
            if(!scared && !singing){
                Scare();
            }
        }
    }
    public void LightsOn(){
        singing = true;
        able = false;
        AI.SetDestination(transform.position);
        StopAllCoroutines();
        animator.Play("Sing");
        currentidlepos = idlepositions[Random.Range(0, idlepositions.Count)];
    }
    void Scare(){
        scared = true;
        jumpscarer.transform.position = main.position;
        jumpscarer.SetActive(true);
        main.gameObject.SetActive(false);
    }
    IEnumerator dontmove(){
        yield return new WaitForSeconds(0.2f);
        able = false;
        int rand = Random.Range(1, 5);
        if(rand == 1){
            running = true;
            StartCoroutine(run());
        }
    }
    IEnumerator run(){
        animator.CrossFade("Run", 0.1f);
        able = true;
        AI.SetDestination(currentidlepos.position);
        normSpeed = AI.speed;
        AI.speed = runSpeed;
        yield return new WaitForSeconds(Random.Range(2, 10));
        animator.CrossFade("Walk", 0.5f);
        AI.speed = normSpeed;
    }
    IEnumerator unstuck(){
        yield return new WaitForSeconds(7f);
        currentidlepos = idlepositions[Random.Range(0, idlepositions.Count)];
    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name.Contains("layer")){
            if(!scared && !singing){
                Scare();
            }
        }
    }
    public void OnTriggerEnter(Collider collider){
        if(collider.transform.gameObject.tag == "FredIdle"){
            StopCoroutine(unstuck());
            StartCoroutine(unstuck());
            Transform oldpos = currentidlepos;
            idlepositions.Remove(oldpos);
            currentidlepos = idlepositions[Random.Range(0, idlepositions.Count)];
            idlepositions.Add(oldpos);
        }
    }
}