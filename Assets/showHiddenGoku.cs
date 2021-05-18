using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showHiddenGoku : MonoBehaviour
{
    [SerializeField] private GameObject HiddenObjects;
    [SerializeField] private AudioClip hereheis;
    AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        HiddenObjects.SetActive(false);
    }

    public void showObjects()
    {
        HiddenObjects.SetActive(true);
        audioSource.clip = hereheis;
        audioSource.Play();
        StartCoroutine("ShowGoku");
    }

    private IEnumerator ShowGoku()
    {
        yield return new WaitForSeconds(.5f);
        HiddenObjects.SetActive(false);

    }
}
