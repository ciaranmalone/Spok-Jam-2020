using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    /*
     * If this bool is true, the original instance is kept and new instances are destroyed
     * If false, any new instances encountered are persisted and old objects are destroyed
     */
    [SerializeField] private bool PrioritiseStartingInstance;
    private void Start()
    {
        if (PrioritiseStartingInstance)
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
        else
        {
            GameObject oldObject = GameObject.Find(this.name + "_Saved");
            if (oldObject)
            {
                Destroy(oldObject);
            }
            this.name = this.name + "_Saved";
            DontDestroyOnLoad(gameObject);
        }
    }
}
