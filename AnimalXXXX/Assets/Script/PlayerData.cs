using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int[] m_baseDurTiers = { 8, 10, 13, 18, 25, 35 };

    private int m_tier = 0;
    private int m_nextResist = 0;

    public int m_barnDurability = 0;
    public int m_baseDurability = 8;
    public int m_barnResistance = 3;
    public bool m_lost = false;
    public bool m_won = false;

    private DmgCtrl m_Barn;

    // Start is called before the first frame update
    void Start()
    {
        m_nextResist = Mathf.CeilToInt(m_barnResistance * 1.5f);
        m_Barn = GameObject.Find("Granja").GetComponent<DmgCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_lost)
        {
            Debug.Log("Lost!");
        }
        else if(m_won)
        {
            Debug.Log("Win!");
        }
    }
    
    /// <summary>
    /// Regresa una lista de animales que se encuentren en el área indicada.
    /// </summary>
    /// <param name="pos">Punto central del área</param>
    /// <param name="radius">Radio del área</param>
    /// <returns>Lista de animales</returns>
    public List<Animal> GetAnimalsByArea(Vector3 pos, float radius)
    {
        List<Animal> animalList = new List<Animal>();
        var animals = GameObject.FindObjectsOfType<Animal>();

        foreach (var animal in animals)
        {
            float distance = (animal.transform.position - pos).magnitude;
            if (distance < radius)
            {
                animalList.Add(animal);
            }
        }
        return animalList;
    }

    public void DamageBarn(int damage)
    {
        m_barnDurability -= (damage - m_barnResistance);
        m_Barn.setScore(m_barnDurability);
        if (m_barnDurability <= 0)
        {
            m_lost = true;
        }
    }

    public void BuildBarn(int increase)
    {
        m_barnDurability += increase;
        m_Barn.setScore(m_barnDurability);
        if (m_barnDurability > m_baseDurability)
        {
            m_tier++;
            if(m_tier >= m_baseDurTiers.Length - 1)
            {
                m_won = true;
                return;
            }

            m_baseDurability = m_baseDurTiers[m_tier];
            m_barnResistance = m_nextResist;
            m_nextResist = Mathf.CeilToInt(m_barnResistance * 1.5f);
        }
    }
    
}
