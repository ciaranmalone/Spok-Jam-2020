using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class FogSprite : MonoBehaviour
{
    [SerializeField] private float maxAlpha = 0.5f;
    [SerializeField] private string playerName = "Player";
    [SerializeField] private float fadeStartRange = 20f;
    [SerializeField] private float invisibleRange = 5f;
    [SerializeField] private bool DEBUG = false;
    private SpriteRenderer sprite;
    private Transform Player;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Player = GameObject.Find(playerName).transform;
    }

    void Update()
    {
        transform.LookAt(Player);
        transform.Rotate(0, 180, 0);

        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        
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
}
