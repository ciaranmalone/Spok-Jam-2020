<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demonPuddle : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Sprite sprites;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.name == "mopObjective")
        {
            m_SpriteRenderer.sprite = sprites;
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demonPuddle : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    [SerializeField] private Sprite sprites;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.name == "mopObjective")
        {
            m_SpriteRenderer.sprite = sprites;
        }
    }
}
>>>>>>> a5cb3f95e602a33444a2c7565876a08e4fe5f083
