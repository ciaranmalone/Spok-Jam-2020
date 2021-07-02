using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneRandomizer : MonoBehaviour
{
    [SerializeField]
    GameObject phone;
    Transform[] transforms;

    // Start is called before the first frame update
    void Start()
    {
        List<Transform> al = new List<Transform>(GetComponentsInChildren<Transform>());
        al.RemoveAt(0);
        transforms = al.ToArray();
        RandomizePhone();
    }

    internal void RandomizePhone()
    {
        phone.transform.parent = transforms[Random.Range(0, transforms.Length-1)];
        phone.transform.localPosition = Vector3.zero;
        phone.transform.localRotation = Quaternion.identity;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
