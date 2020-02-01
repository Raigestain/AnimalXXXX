using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScroll : MonoBehaviour
{
    // Variables
    public float m_TranslationVel;
    public Vector3 m_MaxScrollLimit;
    public Vector3 m_MinScrollLimit;

    // Start is called before the first frame update
    void Start()
    {
        // Default camera position
        transform.position = new Vector3(-6.0f, 10.0f, -5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Update cam position w/WASD
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
            {
                Walk(new Vector3(-1.0f, 0.0f, 0.0f));
            }
            if (Input.GetKey(KeyCode.D))
            {
                Walk(new Vector3(1.0f, 0.0f, 0.0f));
            }
            if (Input.GetKey(KeyCode.W))
            {
                Walk(new Vector3(0.0f, 0.0f, 1.0f));
            }
            if (Input.GetKey(KeyCode.S))
            {
                Walk(new Vector3(0.0f, 0.0f, -1.0f));
            }

            return;
        }
        
        
        if (Input.mousePosition.x <= 0)
        {
            Walk(new Vector3(-1.0f, 0.0f, 0.0f));
        }
        if (Input.mousePosition.x >= Screen.width)
        {
            Walk(new Vector3(1.0f, 0.0f, 0.0f));
        }
        if (Input.mousePosition.y >= Screen.height)
        {
            Walk(new Vector3(0.0f, 0.0f, 1.0f));
        }
        if (Input.mousePosition.y <= 0)
        {
            Walk(new Vector3(0.0f, 0.0f, -1.0f));
        }
    }

    // Update cam position by given translation transform
    void Walk(Vector3 _Translation)
    {
        Vector3 newPosition = transform.position + (_Translation * m_TranslationVel);

        newPosition.x = Mathf.Clamp(newPosition.x, m_MinScrollLimit.x, m_MaxScrollLimit.x);
        newPosition.y = Mathf.Clamp(newPosition.y, m_MinScrollLimit.y, m_MaxScrollLimit.y);
        newPosition.z = Mathf.Clamp(newPosition.z, m_MinScrollLimit.z, m_MaxScrollLimit.z);

        transform.position = newPosition;
    }
}
