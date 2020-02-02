using System.Collections;
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
    protected Vector3 m_direction;
    protected float m_speed;
    protected Vector3 m_steeringForce;
    protected Vector3 m_targetPos;
    private GameObject m_followNode;
    
    // FSM
    private FSM m_currentState = null;
    private STATES m_targetStateName;

    // Start is called before the first frame update
    public void Start()
    {
        // Inicializmos la direccion
        m_direction = Vector3.forward;

        // Si hay un objetivo utilizamos  su posicion como target
        if (_objective)
        {
            m_targetPos = _objective.transform.position;
        }

        // Inicializamos el primer nodo
        m_followNode = _initNode;

        //Set initial State to all the enemies.
        m_currentState = gameObject.AddComponent<IdleState>() as IdleState;
        m_currentState.onEntry();
    }

    // Update is called once per frame
    public void Update()
    {
        if (m_currentState)
        {
            m_targetStateName = m_currentState.Update();

            if (m_targetStateName != m_currentState.m_ID)
            {
                m_currentState.onExist();
                Destroy(m_currentState);
                if (m_targetStateName == STATES.S_IDLE)
                {
                    m_currentState = gameObject.AddComponent(typeof(IdleState)) as IdleState;
                    m_currentState.onEntry();
                }
                else if (m_targetStateName == STATES.S_WALK)
                {
                    m_currentState = gameObject.AddComponent(typeof(WalkState)) as WalkState;
                    m_currentState.onEntry();
                }
                else if (m_targetStateName == STATES.S_RUN)
                {
                    m_currentState = gameObject.AddComponent(typeof(RunState)) as RunState;
                    m_currentState.onEntry();
                }
                else if (m_targetStateName == STATES.S_JUMP)
                {
                    m_currentState = gameObject.AddComponent(typeof(JumpState)) as JumpState;
                    m_currentState.onEntry();
                }
                else if (m_targetStateName == STATES.S_CHARGE)
                {
                    m_currentState = gameObject.AddComponent(typeof(ChargeState)) as ChargeState;
                    m_currentState.onEntry();
                }
                else if (m_targetStateName == STATES.S_DELIVER)
                {
                    m_currentState = gameObject.AddComponent(typeof(DeliverState)) as DeliverState;
                    m_currentState.onEntry();
                }
                else if (m_targetStateName == STATES.S_DIE)
                {
                    m_currentState = gameObject.AddComponent(typeof(DieState)) as DieState;
                    m_currentState.onEntry();
                }
                else if (m_targetStateName == STATES.S_PATROL)
                {
                    m_currentState = gameObject.AddComponent(typeof(PatrolState)) as PatrolState;
                    m_currentState.onEntry();
                }
                else
                {
                    m_currentState = null;
                }
            }
        }

        // Update steering forces 
        updateSteering();
    }

    //-------------------------------------------------------------------------
    // Funcione publicas
    //-------------------------------------------------------------------------
    public void setTargetPos(Vector3 _pos)
    {
        m_targetPos = _pos;
    }

    public float truncate(float _magnitud, float _max)
    {
        if (_magnitud > _max)
        {
            _magnitud = _max;
        }

        return _magnitud;
    }

    public void updateSteering()
    {
        // Limpiamos la fuerza
        m_steeringForce = new Vector3();

        if (m_targetPos != null || m_followNode != null)
        {
            if (m_targetStateName == STATES.S_PATROL)
            {
                // Usamos la fuerza
                m_steeringForce = FollowPath(_seekForce);
            }
            else if (m_targetStateName == STATES.S_WALK)
            {
                // Usamos la fuerza
                m_steeringForce = Arrive(m_targetPos, _seekForce) + Separation();
            }

            // Contenemos la velocidad
            m_speed = truncate(m_steeringForce.magnitude, _maxSpeed);

            // Calculamos la nueva direccion
            Vector3 newDir = (m_direction + m_steeringForce * _mass).normalized;

            // Update position
            transform.position += newDir * m_speed * Time.deltaTime;

            // Update direction
            m_direction = newDir;

            // Actualizar la rotacion asset
            transform.forward = m_direction.normalized;
        }
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
