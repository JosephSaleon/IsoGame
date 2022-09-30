using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineSprite : MonoBehaviour
{

    private SpriteRenderer[] spriteRenderers;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        // foreach (SpriteRenderer sprite in spriteRenderers){
        //     sprite.cre
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
