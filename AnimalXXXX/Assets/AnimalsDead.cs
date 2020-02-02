using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsDead : MonoBehaviour
{
    public ParticleSystem windParticle;
    public float shootTime = 0;
    private float elapsedTime = 0.0f;

    bool particle1 = false;
    bool particle2 = false;
    bool particle3 = false;

    public List<Animal> animalList;

    void ShootParticle()
    {
        elapsedTime = 0.0f;
        windParticle.Play();
        GetAnimals();
        KillAnimals();
    }

    void GetAnimals()
    {
        animalList.Clear();
        var animals = GameObject.FindObjectsOfType<Animal>();

        foreach (var animal in animals)
        {
            animalList.Add(animal);
        }
    }

    void KillAnimals()
    {        
        animalList[0].GetComponentInChildren<ParticleSystem>().gameObject.SetActive(true);
        animalList[0].GetComponentInChildren<ParticleSystem>().Play();
        animalList[0].GetComponentInChildren<SkinnedMeshRenderer>().gameObject.SetActive(false);

        animalList[1].GetComponentInChildren<ParticleSystem>().gameObject.SetActive(true);
        animalList[1].GetComponentInChildren<ParticleSystem>().Play();
        animalList[1].GetComponentInChildren<SkinnedMeshRenderer>().gameObject.SetActive(false);

        animalList[2].GetComponentInChildren<ParticleSystem>().gameObject.SetActive(true);
        animalList[2].GetComponentInChildren<ParticleSystem>().Play();
        animalList[2].GetComponentInChildren<SkinnedMeshRenderer>().gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float windTime = Mathf.Clamp(elapsedTime, 0.0f, shootTime);

        if (windTime == shootTime)
        {
            ShootParticle();
        }
    }
}
