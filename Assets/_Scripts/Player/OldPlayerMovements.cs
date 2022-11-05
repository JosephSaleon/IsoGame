using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerMovements : MonoBehaviour
{

    [SerializeField] private Rigidbody body;
    public Animator animator;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    float MagnetRange = 3f;
    float PickUpRange = 0.5f;
    float MagnetSpeed = 3f;



    public float runSpeed = 20.0f;

    void Start(){
        animator = GetComponentsInChildren<Animator>()[0];
    }

    void Update()
    {
        isMooving();
        pickUpLoots();
    }

    void isMooving(){
        if(GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    
    }

    void pickUpLoots(){
        GameObject[] loots = GameObject.FindGameObjectsWithTag("Loot");
        foreach (GameObject loot in loots)
        {
            float distanceToLoot = Vector3.Distance(transform.position, loot.transform.position);
            if(MagnetRange >= distanceToLoot)
            {
                Vector3 dir =  transform.position - loot.transform.position;
                loot.transform.Translate(dir.normalized * MagnetSpeed * Time.deltaTime, Space.World);
                if(PickUpRange >= distanceToLoot){
                    Loots lootScript = loot.GetComponent<Loots>();
                    lootScript.pickUpLoot();
                    Destroy(loot);
                    
                    
                }
            }
        }
    }

    void FixedUpdate()
    {
        // if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        // {
        //     horizontal *= moveLimiter;
        //     vertical *= moveLimiter;
        // } 

        // body.velocity = new Vector3(horizontal * runSpeed, 0, vertical * runSpeed).ToIsoOther();
    }
}

public static class Helpers 
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIsoOther(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}

