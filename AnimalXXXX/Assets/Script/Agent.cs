﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    // Variables publicas
    public GameObject _objective;
    public float _seekForce = 5.0f;
    [Range(0.0f, 1.0f)]
    public float _mass = 0.5f;
    public float _velocity = 1.0f;
    public float _maxSpeed = 2.0f;
    public float _arriveRadius = 1.0f;
    public float _floackingRange = 1.0f;
    public float _separationDistance = 1.0f;
    public float _separationForce = 2.0f;
    public GameObject _initNode;
    public bool _patrol = false;

    // Variables privadas
    private Vector3 m_direction;
    private float m_speed;
    private Vector3 m_steeringForce;
    private Vector3 m_targetPos;
    private bool m_hasTarget = false;
    private GameObject m_followNode;

    // Start is called before the first frame update
    public void Start()
    {
        // Inicializmos la direccion
        m_direction = Vector3.left;

        // Si hay un objetivo utilizamos  su posicion como target
        if (_objective)
        {
            m_targetPos = _objective.transform.position;
            m_hasTarget = true;
        }

        // Init follow node
        m_followNode = _initNode;
    }

    // Update is called once per frame
    public void Update()
    {
        // Limpiamos la fuerza
        m_steeringForce = new Vector3();

        if (m_hasTarget)
        {
            // Usamos la fuerza
            m_steeringForce = FollowPath(_seekForce);

            // Contenemos la velocidad
            m_speed = truncate(m_steeringForce.magnitude, _maxSpeed);

            // Calculamos la nueva direccion
            Vector3 newDir = (m_direction + m_steeringForce * _mass).normalized;

            // Update position
            transform.position += newDir * m_speed * Time.deltaTime;

            // Update direction
            m_direction = newDir;
        }
    }

    //-------------------------------------------------------------------------
    // Funcione publicas
    //-------------------------------------------------------------------------
    public void setTargetPos(Vector3 _pos)
    {
        m_targetPos = _pos;
        m_hasTarget = true;
    }

    public float truncate(float _magnitud, float _max)
    {
        if (_magnitud > _max)
        {
            _magnitud = _max;
        }

        return _magnitud;
    }

    //-------------------------------------------------------------------------
    // Steering behaviors
    //-------------------------------------------------------------------------
    Vector3 Seek(Vector3 _target, float _force)
    {
        return (_target - transform.position).normalized * _force;
    }

    Vector3 Flee(Vector3 _target, float _force)
    {
        return (transform.position - _target).normalized * _force;
    }

    public Vector3 Arrive(Vector3 objective, float forceMagnitude)
    {
        Vector3 desiredVector = objective - transform.position;
        float distance = desiredVector.magnitude;

        float multiplier = 1.0f;
        if (distance < _arriveRadius)
        {
            multiplier = distance / _arriveRadius;
        }

        return desiredVector.normalized * (forceMagnitude * multiplier);
    }

    Vector3 Separation()
    {
        // Get list of all the agents
        var agents = FindObjectsOfType<Animal>();
        Vector3 Separation = Vector3.zero;

        if (agents.Length != 0)
        {
            for (int i = 0; i < agents.Length; ++i)
            {
                if (agents[i] != this)
                {
                    // Get distance between this and the other agent
                    Vector3 target = agents[i].transform.position - transform.position;
                    if (target.magnitude < _floackingRange)
                    {
                        // If is in distance for separation
                        if (target.magnitude < _separationDistance)
                        {
                            Separation = Flee(agents[i].transform.position, _separationForce);
                        }
                    }
                }
            }
        }
        return Separation;
    }

    public Vector3 FollowPath(float _force)
    {
        Node currentNode = m_followNode.GetComponent<Node>();
        Vector3 desired = currentNode.transform.position - transform.position;
        if (desired.magnitude < currentNode.Radius)
        {
            if (currentNode.getNext())
            {
                m_followNode = currentNode.getNext();
            }
            else
            {
                if (_patrol)
                {
                    m_followNode = _initNode;
                }
            }
        }

        return Seek(currentNode.transform.position, _force);
    }
}
