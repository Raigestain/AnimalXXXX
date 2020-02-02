using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    private const float HEIGHT_OFFSET = 0.2f;

    [SerializeField]
    private float m_colRadius = 0.3f;
    [SerializeField]
    private bool m_occupied = false;

    private PlayerData m_player;
    private Animal m_carrier;

    public int m_value = 0;

    public RESOURCES_TYPES m_type;



    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Granja").GetComponent<PlayerData>();

        m_value = (int)m_type + 1;

    }

    // Update is called once per frame
    void Update()
    {
        if(!m_occupied)
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

            if (animal.m_type == ANIMAL_TYPES.DOG ||
                animal.m_type == ANIMAL_TYPES.ALPACA ||
                animal.m_type == ANIMAL_TYPES.HORSE)
            {
                m_carrier = animal;
                animal.SetPackage(this);
                UpdatePosition();
                m_occupied = true;

                animal.setTargetPos(m_player.GetBarnPosition());
                animal.SetState(ANIMAL_STATES.RUN);
            }
        }
    }

    public void UpdatePosition()
    {
        Vector3 newPosition = m_carrier.transform.position;
        newPosition.y += HEIGHT_OFFSET;
        transform.position = newPosition;
        transform.forward = m_carrier.transform.forward;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, m_colRadius);
    }
}
