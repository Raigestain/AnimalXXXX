using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgCtrl : MonoBehaviour
{
    // Variables
    int m_Score = 4;
    public int m_Tier1;
    public int m_Tier2;
    public int m_Tier3;
    public int m_Tier4;
    public int m_Tier5;

    GameObject m_Piso;
    GameObject m_Paredes;
    GameObject m_Techo;
    GameObject m_Maiz;
    GameObject m_Ventanas;
    GameObject m_Calabaza;
    GameObject m_Palito;
    GameObject m_Molino;
    ParticleSystem m_Construccion;
    ParticleSystem m_Destruccion;

    // Start is called before the first frame update
    void Start()
    {
        m_Piso = GameObject.Find("Piso");
        m_Paredes = GameObject.Find("Paredes");
        m_Techo = GameObject.Find("Techo");
        m_Maiz = GameObject.Find("Maiz");
        m_Ventanas = GameObject.Find("Ventanas");
        m_Calabaza = GameObject.Find("Calabaza");
        m_Palito = GameObject.Find("Palito");
        m_Molino = GameObject.Find("Molino");
        m_Construccion = GameObject.Find("Construccion").GetComponent<ParticleSystem>();
        m_Destruccion = GameObject.Find("Desctruccion").GetComponent<ParticleSystem>();

        m_Piso.SetActive(false);
        m_Paredes.SetActive(false);
        m_Techo.SetActive(false);
        m_Maiz.SetActive(false);
        m_Ventanas.SetActive(false);
        m_Calabaza.SetActive(false);
        m_Palito.SetActive(false);
        m_Molino.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // set score 
    public void setScore(int _Score)
    {
        m_Score = _Score;

        // update visibility
        if (m_Score >= m_Tier1)
        {
            if (!m_Piso.activeInHierarchy)
            {
                m_Piso.SetActive(true);
                m_Construccion.Play();
            }
        }
        else
        {
            m_Piso.SetActive(false);
            m_Destruccion.Play();
        }

        if (m_Score >= m_Tier2)
        {
            if (!m_Paredes.activeInHierarchy)
            {
                m_Paredes.SetActive(true);
                m_Construccion.Play();
            }
        }
        else
        {
            m_Paredes.SetActive(false);
            m_Destruccion.Play();
        }

        if (m_Score >= m_Tier3)
        {
            if (!m_Techo.activeInHierarchy &&
                !m_Maiz.activeInHierarchy)
            {
                m_Techo.SetActive(true);
                m_Maiz.SetActive(true);
                m_Construccion.Play();
            }
        }
        else
        {
            m_Techo.SetActive(false);
            m_Maiz.SetActive(false);
            m_Destruccion.Play();
        }

        if (m_Score >= m_Tier4)
        {
            if (!m_Ventanas.activeInHierarchy &&
                !m_Calabaza.activeInHierarchy)
            {
                m_Ventanas.SetActive(true);
                m_Calabaza.SetActive(true);
                m_Construccion.Play();
            }
        }
        else
        {
            m_Ventanas.SetActive(false);
            m_Calabaza.SetActive(false);
            m_Destruccion.Play();
        }

        if (m_Score >= m_Tier5)
        {
            if (!m_Palito.activeInHierarchy &&
                !m_Molino.activeInHierarchy)
            {
                m_Palito.SetActive(true);
                m_Molino.SetActive(true);
                m_Construccion.Play();
            }
        }
        else
        {
            m_Palito.SetActive(false);
            m_Molino.SetActive(false);
            m_Destruccion.Play();
        }
    }

    // get score 
    public int getScore()
    {
        return m_Score;
    }
}
