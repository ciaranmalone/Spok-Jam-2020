using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRain : MonoBehaviour
{
    ParticleSystem rainParticle;
    [SerializeField] bool rainOn;
    private void Start() => rainParticle = GetComponent<ParticleSystem>();

    public ToggleRain()
    {
        rainOn = !rainOn;
        rainParticle.enableEmission = rainOn;
    }
}
