using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
    private PlayerData m_player;
    private Animal m_attachedChick = null;
    private GameObject m_hen = null;

    public Transform _spawnObjective;

    [SerializeField]
    private Animal m_chickenBase;
    [SerializeField]
    private float m_timeToSpawn = 0f;
    [SerializeField]
    private float m_timeToStart = 0f;
    [SerializeField]
    private float m_moveDistance = 0f;
    [SerializeField]
    private float m_colRadius = 0.3f;

    private float m_spawnTimer = 0.0f;
    private float m_tranfromTimer = 0.0f;
    private ParticleSystem m_spawnFog;
    private bool m_isFogSpawned = false;

    [SerializeField]
    private bool m_occupied = false;
    [SerializeField]
    private bool m_producing = false;

    // Start is called before the first frame update
    void Start()
    {
        m_spawnTimer = m_timeToSpawn;
        m_tranfromTimer = m_timeToStart;
        m_spawnFog = GetComponentInChildren<ParticleSystem>();
        m_spawnFog.Stop();
        m_player = GameObject.Find("Granja").GetComponent<PlayerData>();

        var main = m_spawnFog.main;
        main.loop = false;
        m_hen = GetComponentInChildren<Animator>().gameObject;
        m_hen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_occupied && m_producing)
        {
            m_spawnTimer -= Time.deltaTime;

            if (m_spawnTimer < 1f && !m_isFogSpawned)
            {
                m_spawnFog.Play();
                m_isFogSpawned = true;
            }

            if (m_spawnTimer <= 0)
            {
                m_spawnTimer = m_timeToSpawn;
                Animal newChicken = Instantiate<Animal>(m_chickenBase);
                Vector3 spawnPos = transform.position;
                spawnPos.x += 0.1f;
                newChicken.transform.position = spawnPos;
                newChicken.transform.rotation = transform.rotation;
                m_isFogSpawned = false;
                newChicken.objective = _spawnObjective.position;
            }
        }
        else if(m_occupied)
        {
            m_tranfromTimer -= Time.deltaTime;

            if(m_tranfromTimer <= 0)
            {
                m_producing = true;

                if(null != m_attachedChick)
                {
                    Destroy(m_attachedChick.gameObject);
                    m_attachedChick = null;
                }

                m_hen.SetActive(true);
                m_spawnFog.Play();
            }
        }

        if (!m_occupied)
        {
            DetectAnimal();
        }
    }

    void DetectAnimal()
    {
        List<Animal> animals = null;

        animals = m_player.GetAnimalsByArea(transform.position, m_colRadius);

        foreach (var animal in animals)
        {
            if (animal.m_type == ANIMAL_TYPES.CHICKEN)
            {
                m_attachedChick = animal;
                m_attachedChick.transform.position = transform.position;
                m_attachedChick.transform.forward = transform.forward;
                m_occupied = true;
                animal.SetState(ANIMAL_STATES.IDLE);
            }
        }
    }
}
