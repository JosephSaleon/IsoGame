using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotate = 100f;

    [SerializeField] private bool isGamepad;


    float MagnetRange = 3f;
    float PickUpRange = 0.5f;
    float MagnetSpeed = 3f;

    private Vector3  playerVelocity;

    // ref
    private PlayerInputManager playerInputManager;
    private CharacterController controller;
    private Animator animator;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentsInChildren<Animator>()[0];
        playerInputManager = PlayerInputManager.Instance;
    }

    void isMooving(){
        if(playerInputManager.isMooving())
        {
            animator.SetBool("Run", true);
        }else{
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

    void Update()
    {
        HandleMovement();
        isMooving();
        pickUpLoots();
    }

    

    
    void HandleMovement()
    {
        Vector3 move = new Vector3(playerInputManager.movement.x, 0, playerInputManager.movement.y);
        controller.Move(move.ToIso() * Time.deltaTime * playerSpeed);
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void OnDeviceChange(PlayerInput pi){
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}
