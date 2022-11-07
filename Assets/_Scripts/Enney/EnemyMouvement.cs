using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using MoreMountains.Feedbacks;

public class EnemyMouvement : MonoBehaviour {

    [Header("Settings")]
    public float rangDetectionDistance = 10f;
    public float startSpeed = 10;
    public float rangStaticDistance = 2f;

    [Header("Update")]
    public float movementSpeed;

    [Header("Ref")]
    private Enemy selfEnemyScript;

    [Header("Update ref")]
    private Transform target;


    public void Start()
    {
        StartMoving();
        target = GameObject.FindWithTag("Player").transform;
        selfEnemyScript = GetComponent<Enemy>();
    }

    void Update()
    {
        if(target != null){
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);
            if(distanceToPlayer < rangDetectionDistance){
                moveToTarget();
            }
        }
       
    }

    public void moveToTarget() {
        if(!selfEnemyScript.isDead){
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * movementSpeed * Time.deltaTime, Space.World);
        };
        
    }

    public void Slow(float amount)
    {
        movementSpeed = startSpeed * (1f - amount);
    }

    public void StopMoving(){
        movementSpeed = 0;
    }

    public void StartMoving(){
        movementSpeed = startSpeed;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangDetectionDistance);
    }

}