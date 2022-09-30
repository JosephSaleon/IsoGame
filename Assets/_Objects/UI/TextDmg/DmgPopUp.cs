using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgPopUp : MonoBehaviour
{

    public static DmgPopUp Create(Vector3 position, int dmgAmount, bool isCritical){
        Quaternion newQuaternion = new Quaternion();
        newQuaternion.Set(0f,0f,0f,1f);
        Transform dmgPopUpTransform = Instantiate(GameAsset.i.TextDmg, position, Quaternion.Euler(0, 45, 0));
        DmgPopUp dmgPopUp = dmgPopUpTransform.GetComponent<DmgPopUp>();
        dmgPopUp.Setup(dmgAmount, isCritical);

        return dmgPopUp;
    }

    private static int sortingOrder;

    private const float disappearTimerMax = 1f;
    private const float startToFade = 0.5f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;


    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();    
    }

    public void Setup(int dmgAmount, bool isCritical){
        textMesh.SetText(dmgAmount.ToString());
        if(isCritical){
            textMesh.fontSize = 12;
        }else{
            textMesh.fontSize = 8;
        }
        textColor = textMesh.color;
        disappearTimer = disappearTimerMax;
        moveVector = new Vector3(.7f, 1) * 5f;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

    }

    private void Update() {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if(disappearTimer > disappearTimerMax * .8f){
            float increaseScaleAmount = 2f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }else{
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }


        disappearTimer -= Time.deltaTime;
        if(disappearTimer < startToFade ){
            float disappearSpeed = 5f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0 || transform.localScale.y <= 0 || transform.localScale.x <= 0){
                Destroy(gameObject);
            }
        }

    }    
}
