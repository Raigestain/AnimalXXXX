using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmutation : MonoBehaviour
{
    public Material matofObject;
    public GameObject _InvLight;
    public ParticleSystem _InvParticle;
    public Color initialColor = new Color(255.0f, 255.0f, 255.0f, 255.0f);
    public Color newColor = new Color(172.0f, 0.0f, 0.0f, 255.0f);
    public float duration = 3.0f;
    public bool isActive = false;

    private bool secuenceFinished = false;
    private float elapsedTime = 0.0f;

    public int m_ipig = 0;
    public int m_ichick = 0;

    public int m_ipigtoDestroy = 0;
    public int m_ichicktoDestroy = 0;

    public GameObject Wood;
    public GameObject Straw;
    public GameObject Bricks;

    public GameObject Pig;
    public GameObject Horse;
    public GameObject Dog;
    public GameObject Alpaca;

    public List<Animal> nearAnimals;
    public ParticleSystem _godLigth;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;
        _InvParticle.Stop();
        _godLigth.Stop();

        matofObject.SetColor("_Color", initialColor);
        matofObject.SetColor("_EmissionColor", initialColor);
    }

    // Update is called once per frame
    void Update()
    { 
        if (isActive)
        {
            elapsedTime += Time.deltaTime;
            float transCircleTime = Mathf.Clamp(elapsedTime, 0.0f, duration);

            matofObject.SetColor("_Color", Color.Lerp(Color.white, newColor, transCircleTime / duration));
            matofObject.SetColor("_EmissionColor", Color.Lerp(Color.white, newColor, transCircleTime / duration)); 
            
            if (transCircleTime == duration)
            {
                Deactivate();
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            float transCircleTime = Mathf.Clamp(elapsedTime, 0.0f, duration);
                   
            matofObject.SetColor("_Color", Color.Lerp(newColor, Color.white, transCircleTime / duration));
            matofObject.SetColor("_EmissionColor", Color.Lerp(newColor, Color.white, transCircleTime / duration));
            
        }
    }

    public void checkRecipes()
    {
        if (m_ipig >= 3 && m_ichick >= 1)
        {
            m_ipigtoDestroy = 3;
            m_ichicktoDestroy = 1;
            Instantiate(Bricks, new Vector3(3.9f, 0.22f, 0.0f), Quaternion.identity);
            elminateAnimals();
            print("1 DE LADRILLO");
        }
        else if (m_ipig >= 2 && m_ichick >= 1)
        {
            m_ipigtoDestroy = 2;
            m_ichicktoDestroy = 1;
            Instantiate(Wood, new Vector3(3.9f, 0.22f, 0.0f), Quaternion.identity);
            elminateAnimals();
            print("1 DE MADERA");
        }
        else if (m_ipig >= 1 && m_ichick >= 1)
        {
            m_ipigtoDestroy = 1;
            m_ichicktoDestroy = 1;
            Instantiate(Straw, new Vector3(3.9f, 0.22f, 0.0f), Quaternion.identity);
            elminateAnimals();
            print("1 DE PAJA");
        }
        else if (m_ichick >= 15)
        {
            m_ichicktoDestroy = 15;
            Instantiate(Horse, new Vector3(3.9f, 0.22f, 0.0f), Quaternion.identity);
            elminateAnimals();
            print("CABALLOOOOO");
        }
        else if (m_ichick >= 10)
        {
            m_ichicktoDestroy = 10;
            Instantiate(Alpaca, new Vector3(3.9f, 0.22f, 0.0f), Quaternion.identity);
            elminateAnimals();
            print("ALPACAAAAA");
        }
        else if (m_ichick >= 5)
        {
            m_ichicktoDestroy = 5;
            Instantiate(Dog, new Vector3(3.9f, 0.22f, 0.0f), Quaternion.identity);
            elminateAnimals();
            print("PERRITOOOOOO");
        }
        else if (m_ichick >= 3)
        {
            m_ichicktoDestroy = 3;
            Instantiate(Pig, new Vector3(3.9f, 0.22f, 0.0f), Quaternion.identity);
            elminateAnimals();
            print("CERDITOOOOOOO");
        }               
    }

    public void nearAnimalsSearch()
    {
        nearAnimals = GetComponent<PlayerData>().GetAnimalsByArea(new Vector3(4.024f, 0.288f, 0.494f), 2);

        for (int i = 0; i < nearAnimals.Count; i++)
        {
            if (nearAnimals[i].tag == "Pig")
            {
                m_ipig++;
            }
            else if (nearAnimals[i].tag == "Chick")
            {
                m_ichick++;
            }
        }        
    }

    public void elminateAnimals()
    {
        for (int i = 0; i < nearAnimals.Count; i++)
        {
            if (m_ipigtoDestroy > 0)
            {
                if (nearAnimals[i].tag == "Pig")
                {
                    Destroy(nearAnimals[i].gameObject);
                    m_ipigtoDestroy--;
                }
            }

            if (m_ichicktoDestroy > 0)
            {
                if (nearAnimals[i].tag == "Chick")
                {
                    Destroy(nearAnimals[i].gameObject);
                    m_ichicktoDestroy--;
                }
            }
        }
    }

    public bool _canSacrifice()
    {
        nearAnimalsSearch();

        if (m_ipig >= 3 && m_ichick >= 1)
        {
            return true;
        }
        else if (m_ipig >= 2 && m_ichick >= 1)
        {
            return true;
        }
        else if (m_ipig >= 1 && m_ichick >= 1)
        {
            return true;
        }
        else if (m_ichick >= 15)
        {
            return true;
        }
        else if (m_ichick >= 10)
        {
            return true;
        }
        else if (m_ichick >= 5)
        {
            return true;
        }
        else if (m_ichick >= 3)
        {
            return true;
        }

        return false;
    }

    public void Activate()
    {
        if(_canSacrifice() && !isActive)
        {            
            isActive = true;
            elapsedTime = 0.0f;
            _godLigth.Play();
            _InvParticle.Play();
            checkRecipes();
        }        
    }

    public void Deactivate()
    {
        elapsedTime = 0.0f;
        isActive = false;
        secuenceFinished = false;
    }
}
