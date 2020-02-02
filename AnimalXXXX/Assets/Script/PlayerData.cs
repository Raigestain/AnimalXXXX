using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private const float BARN_OFFSET = 2.2f;
    private const float DELIVERY_HEIGHT = 0.2f;

    [SerializeField]
    private float m_barnRadius = 2f;

    private int[] m_baseDurTiers = { 8, 10, 13, 18, 25, 35 };
    private int m_tier = 0;
    private int m_nextResist = 0;

    public int m_barnDurability = 0;
    public int m_baseDurability = 8;
    public int m_barnResistance = 3;
    public bool m_lost = false;
    public bool m_won = false;

    private float m_timeToDetect = 1f;
    private float m_detectTimer = 0f;

    private DmgCtrl m_barn;
    private Vector3 m_deliveryPoint = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        m_nextResist = Mathf.CeilToInt(m_barnResistance * 1.5f);
        m_barn = GetComponent<DmgCtrl>();
        m_deliveryPoint = transform.position + transform.right * BARN_OFFSET;
        m_deliveryPoint.y = DELIVERY_HEIGHT;
        m_detectTimer = m_timeToDetect;
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

        m_detectTimer -= Time.deltaTime;

        //if(m_detectTimer <= 0)
        //{
        DetectAnimal();
        m_detectTimer = m_timeToDetect;
        //}
    }

    private void DetectAnimal()
    {
        List<Animal> animals = null;

        animals = GetAnimalsByArea(m_deliveryPoint, m_barnRadius);

        foreach (var animal in animals)
        {
            if (animal.m_type == ANIMAL_TYPES.HORSE ||
                animal.m_type == ANIMAL_TYPES.ALPACA ||
                animal.m_type == ANIMAL_TYPES.DOG)
            {
                if(!animal.m_deliveredPackage)
                {
                    Resources package = animal.GetPackage();
                    BuildBarn(package.m_value);
                    Destroy(package.gameObject);
                    animal.setTargetPos(new Vector3(0, DELIVERY_HEIGHT, 0));
                    animal.SetState(ANIMAL_STATES.WALK);
                    animal.m_deliveredPackage = true;
                }
            }
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
        m_barn.setScore(m_barnDurability);
        if (m_barnDurability <= 0)
        {
            m_lost = true;
        }

        Debug.Log("Barn Durability" + m_barnDurability);
    }

    public void BuildBarn(int increase)
    {
        m_barnDurability += increase;
        m_barn.setScore(m_barnDurability);
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

        Debug.Log("Barn Durability: " + m_barnDurability);
    }

    public Vector3 GetBarnPosition()
    {
        return m_barn.transform.position;
    }
}
