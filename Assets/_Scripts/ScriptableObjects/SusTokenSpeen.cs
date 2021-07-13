using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SusTokenSpeen : MonoBehaviour
{

    float zeroY;
    bool up = true;
    [SerializeField]
    internal SusToken.Token token = SusToken.Token.MONKE;

    /// <summary>
    /// the smaller the wider floating
    /// </summary>
    [SerializeField]
    float floatVariation = 2;

    /// <summary>
    /// speen speed 
    /// </summary>
    [SerializeField]
    float rotationSpeed = 10;

    float posX, posZ;
    float graphX = 0;

    void Start()
    {
        zeroY = transform.position.y;
        posX = transform.position.x;
        posZ = transform.position.z;
    }

    void Update()
    {
        upDown();
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
    }


    void upDown()
    {
        if (graphX > Mathf.PI)
        {
            graphX -= Mathf.PI;
            up = !up;
        }
        graphX += Time.deltaTime;
        transform.position = new Vector3(posX, (up ? 1 : -1) * (Mathf.Sin(graphX) / floatVariation) + zeroY, posZ);
    }


}
