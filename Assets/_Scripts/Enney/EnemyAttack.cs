using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using DG.Tweening;

public class EnemyAttack : MonoBehaviour {

    [Header("Setting")]
    public float rangDetectionDistance = 10f;
    public float projectilSpeed = 2f;
    public int attackPhase = 3;
    public float phaseDelay = 0.1f;
    public float attackCooldown = 1.5f;
    public Vector2 attackDmg;
    public int numberOfProjectilPerShoot = 1;
    public float angleOfProjectil = 0;
    public float scaleTime = 0.25f;
    public bool syncWhileInPhase = true;

    [Header("Update")]
    private bool readyToAttack = true;
    private bool readyForNextPhase = true;

    [Header("Reference")]
    public GameObject projectilPrefab;
    public Transform firePoint; 

    [Header("Reference Update")]
    private Transform target;
    private System.Random rnd = new System.Random();


    public void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        readyToAttack = true;
        readyForNextPhase = true;
        
    }

    

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        
        if(readyToAttack && distanceToPlayer < rangDetectionDistance){
            readyToAttack = false;
            Vector3 fixDir = target.position - transform.position;
            StartCoroutine(StartShootPhase(fixDir));
        }
    }

    IEnumerator StartShootPhase(Vector3 fixDir){
        for(int i = 0; i < attackPhase; i++){
            ShootAtDirection(fixDir);
            yield return new WaitForSeconds(phaseDelay);
            if(i == (attackPhase - 1)){
                Invoke(nameof(resetAttack), attackCooldown);
            }                
        }
    }

    private void ShootAtDirection(Vector3 fixDir){
        Vector3 dir;
        if(syncWhileInPhase){
            dir = target.position - transform.position;
        }else{
            dir = fixDir;
        }
        for(int i = 0; i < numberOfProjectilPerShoot; i++){
            float angle = (angleOfProjectil * i) - ((angleOfProjectil * (numberOfProjectilPerShoot / 2)));
            Vector3 newDir = Quaternion.Euler(0, angle, 0) * dir.normalized;
            shoot(newDir);
        }
    }

    private void shoot(Vector3 dir){
        GameObject projectilInstance = (GameObject)Instantiate(projectilPrefab, firePoint.position, firePoint.rotation);
        Vector3 initialScale = projectilInstance.transform.localScale;
        projectilInstance.transform.DOScale(0,0);
        projectilInstance.GetComponent<Rigidbody>().AddForce(dir * projectilSpeed, ForceMode.Impulse);
        projectilInstance.transform.DOScale(initialScale,scaleTime);

    }

    private void resetAttack(){
        readyToAttack = true;
    }

    private void resetPhase(){
        readyForNextPhase = true;

    }



}