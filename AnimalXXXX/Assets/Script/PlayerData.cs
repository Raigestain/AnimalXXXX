using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static int m_barnDurability = 0;
    public static int m_barnLife = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <summary>
    /// Regresa una lista de animales que se encuentren en el área indicada.
    /// </summary>
    /// <param name="pos">Punto central del área</param>
    /// <param name="radius">Radio del área</param>
    /// <returns>Lista de animales</returns>
    public static List<Animal> GetAnimalsByArea(Vector3 pos, float radius)
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
    
}
