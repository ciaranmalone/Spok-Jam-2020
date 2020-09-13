using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listening : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onSoundMade += itsOverThere;

    }

    private void itsOverThere(Vector3 location){
        print("its over there" + location);
        transform.LookAt(location);
    }
}
