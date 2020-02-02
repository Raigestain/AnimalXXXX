using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
    [SerializeField]
    private Chicken m_chickenBase;

    [SerializeField]
    private float m_timeToSpawn = 0f;

    private float m_spawnTimer;
    private ParticleSystem m_spawnFog;
    private bool m_isFogSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        m_spawnTimer = m_timeToSpawn;
        m_spawnFog = GetComponentInChildren<ParticleSystem>();
        m_spawnFog.Stop();

        var main = m_spawnFog.main;
        main.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_spawnTimer -= Time.deltaTime;

        if(m_spawnTimer < 1f && !m_isFogSpawned)
        {
            m_spawnFog.Play();
            m_isFogSpawned = true;
        }

        if (m_spawnTimer <= 0)
        {
            m_spawnTimer = m_timeToSpawn;
            Chicken newChicken = Instantiate<Chicken>(m_chickenBase);
            Vector3 spawnPos = transform.position;
            spawnPos.x += 0.1f;
            newChicken.transform.position = spawnPos;
            newChicken.transform.rotation = transform.rotation;
            m_isFogSpawned = false;
            PlayerData.AddChickens(1);
        }
    }
}
