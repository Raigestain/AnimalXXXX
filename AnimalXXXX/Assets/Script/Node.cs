using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Next and prev nodes
    public GameObject _prev = null;
    public GameObject _next = null;

    public float _radius = 1.0f;

    // Get radius
    public float Radius
    {
        get
        {
            return _radius;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_next)
        {
            Debug.DrawLine(transform.position, _next.transform.position, Color.red);
        }
        if (_prev)
        {
            Debug.DrawLine(transform.position, _prev.transform.position, Color.red);
        }
    }

    // Returns nex node
    public GameObject getNext()
    {
        return _next;
    }

    // Returns nex node
    public GameObject getPrev()
    {
        return _prev;
    }
}
