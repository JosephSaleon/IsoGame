using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;

public class Enemy : MonoBehaviour {

    public float rangDetectionDistance = 10f;
    public float movementSpeed = 1.5f;
    public float startSpeed = 10;
    [HideInInspector]
    public float speed;
    public float startHealth = 100f;
    private float health;
    public int worth = 50;
    public GameObject deathEffect;
    public Image healthbar;

    public Rigidbody loots;

    private Transform target;
    private bool isDead = false;
    private bool isFoundPlayer = false;

    private SpriteRenderer[] spriteRenderers;
    private Collider spriteCollider;
    private Transform spriteTransform;
    private Animator spriteAnimator;
    private Color spriteColor;

    private System.Random rnd = new System.Random();

    private float rotateSpeed = 300f;
    private float scaleSpeed = 0.3f;

    private Shader shaderGUItext;
    [SerializeField]
    private Shader shaderSpritesDefault;
    private int rdnDeath;



    public void Start()
    {
        speed = startSpeed;
        health = startHealth;
        target = GameObject.FindWithTag("Player").transform;

        // Get the sprite componants
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        spriteCollider = GetComponent<Collider>();
        spriteTransform = transform.FindChild("Sprite");
        spriteAnimator = GetComponentInChildren<Animator>();
        spriteColor = spriteRenderers[0].color;

        // Find the shader for make sprite flash
        shaderGUItext = Shader.Find("GUI/Text Shader");
        // rdnDeath = new Random().Next(100);
       

        rdnDeath = rnd.Next(0,100);
        Debug.Log(rdnDeath);

        // Not working wet
        // shaderSpritesDefault = Shader.Find("Sprites/Sprit env");
        
    }

    

    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if(isFoundPlayer || distanceToPlayer < rangDetectionDistance){
            moveToTarget();
            isFoundPlayer = true;
        }
        
        if(isDead){

            // Not good inof \\

            // spriteCollider.enabled = false;
            // GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            // if(rdnDeath <= 50){
            //     spriteTransform.Rotate(0,0, Time.deltaTime * rotateSpeed, Space.Self);
            // }else{
            //     spriteTransform.Rotate(0,0, Time.deltaTime / rotateSpeed, Space.Self);
            // }

            foreach(SpriteRenderer sprite in spriteRenderers){
                sprite.color = Color.black;
            }
            
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(scale.x - (scale.x / 10) * scaleSpeed, scale.y - (scale.y / 10) * scaleSpeed, scale.z);
            
            if (spriteTransform.localScale.x <= 0 || spriteTransform.localScale.y <= 0){
                Destroy(gameObject);
            }
        }

       
    }

    public void moveToTarget() {
        if(!isDead){
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * movementSpeed * Time.deltaTime, Space.World);
        };
        
    }

    public void TakeDammage(float amount)
    {
        
        DmgPopUp.Create(transform.position, (int)amount, false);
        health -= amount;
        healthbar.fillAmount = health / startHealth;

        if(health <= 0 && !isDead){
            Die();
        }else{
            StartCoroutine(Flash());
        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);
    }

    private void Die()
    {
        spriteAnimator.enabled = false;
        isDead = true;

        // PlayerStats.money += worth;

        GameObject deathParticles = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathParticles, 2f);

        // WaveSpawner.EnemiesAlive--;
        dropLoots();
        Destroy(gameObject, 0.5f);
    }

    IEnumerator Flash()
    {
        Color defaultColor = spriteRenderers[0].color;
        foreach(SpriteRenderer sprite in spriteRenderers){
            sprite.material.shader = shaderGUItext;
            sprite.color = Color.white;
        }

        yield return new WaitForSeconds(0.05f);

        foreach(SpriteRenderer sprite in spriteRenderers){
            sprite.material.shader = shaderSpritesDefault;
            sprite.color = Color.white;
        }
    }

    void dropLoots(){
        int loopNumber = rnd.Next(0,3);
        Rigidbody clone;

        for(int i = 0; i <= loopNumber; i++ ){
            clone = Instantiate(loots, transform.position, transform.rotation);
            clone.velocity = new Vector3(rnd.Next(-1,1), rnd.Next(3,7), rnd.Next(-1,1));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangDetectionDistance);
    }

}