using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SusToken
{

    public enum Token
    {
        MONKE,
        BEE
    }


    public class SusToken : MonoBehaviour
    {
        [SerializeField]
        internal Token token;
    }
}
