using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSprite : MonoBehaviour
{
    [SerializeField] private float maxAlpha = 0.5f;
    [SerializeField] private float fadeStartRange = 20f;
    [SerializeField] private float invisibleRange = 5f;
    [SerializeField] private bool DEBUG = false;
    private SpriteRenderer sprite;
    private Transform Target;

    [SerializeField] bool targetPlayer = true;

    [SerializeField] string alternateTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        
        assignTarget();
    }

    void Update()
    {
        transform.LookAt(Target);
        transform.Rotate(0, 180, 0);

        float distanceToPlayer = Vector3.Distance(transform.position, Target.position);
        
        if(distanceToPlayer < invisibleRange) {
            sprite.color = new Color(1, 1, 1, 0);
        }
        else if (distanceToPlayer <= fadeStartRange)
        {
            //sprite.color = new Color(1, 1, 1, (distanceToPlayer / (fadeStartRange + 10)) * maxAlpha);


            float newAlpha = ((distanceToPlayer - invisibleRange) / (fadeStartRange - invisibleRange)) * maxAlpha;
            sprite.color = new Color(1, 1, 1, newAlpha);

            if (DEBUG)
            {
                print("newAlpha: " + newAlpha);
                print("everything minus maxAlpha: " + ((distanceToPlayer - invisibleRange) / (fadeStartRange - invisibleRange)));
                print("distanceToPlayer: " + distanceToPlayer);
                print("fadeStartRange: " + fadeStartRange);
            }
        }
        else
        {
            sprite.color = new Color(1, 1, 1, maxAlpha);
        }


    }

    private void assignTarget(){
        if(targetPlayer){
            try{
                Target = PlayerMovement.Instance.transform;
            }
            catch{

                try{
                    Target = GameObject.Find(alternateTarget).transform;
                }
                catch{
                    print("No valid target found");
                    this.enabled = false;
                }
            }
        }else{
            try{
                    Target = GameObject.Find(alternateTarget).transform;
                }
            catch{
                print("No valid target found");
                this.enabled = false;
            }
        }
    }
}
