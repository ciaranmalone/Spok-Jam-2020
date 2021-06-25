using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
    Renderer m_Renderer;
    [SerializeField] Texture[] sprites;
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Renderer.material.SetTexture("_MainTex", sprites[Random.Range(0, sprites.Length)]);
    }
}
