using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManager : MonoBehaviour
{

    public static PlayerInputManager Instance;

    [Header("Value")]
    // value
    [SerializeField] private bool isGamepad;
    public Vector2 movement;
    public Vector2 aim;

    [Header("Ref")]
    // ref
    private PlayerControl playerControls;
    private PlayerInput playerInput;


    private void Awake(){
        playerControls = new PlayerControl();
        playerInput = GetComponent<PlayerInput>();
        
        if(Instance != null){
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    { 
        playerControls.Enable();
    }

     private void OnDisable()
    { 
        playerControls.Disable();
    }

    void HandleInpute()
    {
        movement = playerControls.Controls.Mouvement.ReadValue<Vector2>();
        aim = playerControls.Controls.Aim.ReadValue<Vector2>();
    }

    void Update (){
        HandleInpute();
    }

    public bool isMooving(){
        if(movement.x != 0 || movement.y != 0)
        {
            return true;
        }else{
            return false;
        }
    }

    public bool isAiming(){
        if(aim.x != 0 || aim.y != 0)
        {
            return true;
        }else{
            return false;
        }
    }

    public void OnDeviceChange(PlayerInput pi){
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
    
    
}
