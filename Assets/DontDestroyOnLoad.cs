using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Start()
    {
        if (GameObject.Find(this.name + "_Saved"))
        {
            Destroy(this.gameObject);

        }
        else
        {
            this.name = this.name + "_Saved";
            DontDestroyOnLoad(gameObject);

        }
    }
}
