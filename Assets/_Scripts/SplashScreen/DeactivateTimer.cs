using UnityEngine;

public class DeactivateTimer : MonoBehaviour
{
    [SerializeField]
    float timer;

    float ttl;

    AudioSource auso;
    private void Awake()
    {
        auso = GetComponent<AudioSource>();
    }
    internal void Prompt()
    {
        ttl = timer;
        gameObject.SetActive(true);
        if(auso) auso.Play();
    }

    private void Update()
    {
        if (ttl < 0) return;
        ttl -= Time.deltaTime;
        if (ttl < 0) gameObject.SetActive(false);
    }
}
