using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using MoreMountains.Feedbacks;

public class Enemy : MonoBehaviour {

    //Value
    [Header("Value")]
    public float startHealth = 100f;
    private float deathScaleSpeed = 0.3f;

    //Update value
    [Header("Update value")]
    private float health;
    public bool isDead = false;
    private bool isFoundPlayer = false;

    // Ref
    [Header("Ref")]
    public GameObject deathEffect;
    public Image healthbar;
    public Rigidbody loots;
    private SpriteRenderer[] spriteRenderers;
    private Shader shaderGUItext;
    [SerializeField] private Shader shaderSpritesDefault;

    //Update ref
    [Header("Update ref")]
    private Transform target;
    private Transform spriteTransform;
    private Animator spriteAnimator;
    private Color spriteColor;

    // Inner value
    private System.Random rnd = new System.Random();




    public void Start()
    {
        health = startHealth;
        // Get the sprite componants
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        spriteTransform = transform.Find("Sprite");
        spriteAnimator = GetComponentInChildren<Animator>();
        spriteColor = spriteRenderers[0].color;
        // Find the shader for make sprite flash
        shaderGUItext = Shader.Find("GUI/Text Shader");
    }

    

    void Update()
    {
        
        if(isDead){
            
            foreach(SpriteRenderer sprite in spriteRenderers){
                sprite.color = Color.black;
            }
            
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(scale.x - (scale.x / 10) * deathScaleSpeed, scale.y - (scale.y / 10) * deathScaleSpeed, scale.z);
            
            if (spriteTransform.localScale.x <= 0 || spriteTransform.localScale.y <= 0){
                Destroy(gameObject);
            }
        }

       
    }

    public void TakeDammage(float amount)
    {
        health -= amount;
        healthbar.fillAmount = health / startHealth;
        // Feedback?.PlayFeedbacks();
        if(health <= 0 && !isDead){
            DmgPopUp.Create(transform.position, (int)amount, true, "dead");
            Die();
        }else{
            DmgPopUp.Create(transform.position, (int)amount, false);
            StartCoroutine(Flash());
        }
    }

    
    private void Die()
    {
        spriteAnimator.enabled = false;
        isDead = true;

        GameObject deathParticles = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(deathParticles, 2f);
        dropLoots();
        Destroy(gameObject, 0.5f);
    }

    IEnumerator Flash()
    {
        Color defaultColor = spriteRenderers[0].color;
        foreach(SpriteRenderer sprite in spriteRenderers){
            if(sprite.sprite.texture.name != "Shadow"){ 
                sprite.material.shader = shaderGUItext;
                sprite.color = Color.white;
            }
            
        }

        yield return new WaitForSeconds(0.05f);

        foreach(SpriteRenderer sprite in spriteRenderers){
            if(sprite.sprite.texture.name != "Shadow"){ 
                sprite.material.shader = shaderSpritesDefault;
                sprite.color = Color.white;
            }
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

}