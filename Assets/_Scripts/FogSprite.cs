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
        //transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        
        if(distanceToPlayer < invisibleRange) {
            sprite.color = new Color(1, 1, 1, 0);
        }
        else if (distanceToPlayer <= fadeStartRange)
        {
            sprite.color = new Color(1, 1, 1, (distanceToPlayer / fadeStartRange) * maxAlpha);
        }
        else
        {
            sprite.color = new Color(1, 1, 1, maxAlpha);
        }
    }
}
