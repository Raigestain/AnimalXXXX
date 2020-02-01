using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool m_isSelected = false; //Se usa para cuando se solto el click izquierdo.
    private bool m_isSelecting = false; //Se usa mientras el click izquierdo esta apretado.
    private Vector3 m_initPosition;
    private Vector3 m_finalPosition;
    private Vector3 m_initViewPos;
    private Vector3 m_finalViewPos;
    private Camera m_camera;
    private List<Animal> m_selectedAnimals;
    private Transform m_floor;
    private const float FLOOR_OFFSET = 0.5f;

    public float m_deathZone = 0.01f;
    

    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
        m_initPosition = new Vector3();
        m_finalPosition = new Vector3();
        m_selectedAnimals = new List<Animal>();
        m_floor = GameObject.Find("Floor").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0) && !m_isSelecting)
        {
            m_initPosition = Input.mousePosition;
            m_isSelecting = true;
            m_isSelected = false;
            
            // Obtenemos la posicion de mundo del punto inicial
            m_initViewPos = m_camera.ScreenToViewportPoint(new Vector3(m_initPosition.x, m_initPosition.y, 0));
        }

        //Mientras se este seleccionando se sigue guardando la posicion. Esto puede servir para crear la caja visual.
        if(m_isSelecting)
        {
            m_finalPosition = Input.mousePosition;

            // Obtenemos la posicion de mundo del punto final
            m_finalViewPos = m_camera.ScreenToViewportPoint(new Vector3(m_finalPosition.x, m_finalPosition.y, 0));
        }

        //Al soltarse el mouse se guarda la posicion final.
        if (Input.GetMouseButtonUp(0))
        {
            m_finalPosition = Input.mousePosition;
            m_finalViewPos = m_camera.ScreenToViewportPoint(new Vector3(m_finalPosition.x, m_finalPosition.y, 0));
            
            m_isSelected = true;
            m_isSelecting = false;
        }

        //Si ya se solto el click izquierdo.
        if (m_isSelected)
        {
            //Se usa un death zone, si el movimiento es menor a este, cuenta como seleccion individual.
            float distance = (m_finalViewPos - m_initViewPos).magnitude;
            if (distance <= m_deathZone)
            {
                //Se lanza un rayo para saber si choco contra un objeto.
                RaycastHit hit;
                Ray selectionRay = m_camera.ViewportPointToRay(m_initViewPos);

                //Si choca y es un animal, lo selecciona.
                if (Physics.Raycast(selectionRay, out hit))
                {
                    Transform objectTransform = hit.transform;

                    Animal animal = objectTransform.GetComponent<Animal>();
                    if (null != animal)
                    {
                        //Limpiar la lista de animales seleccionados.
                        foreach(var anim in m_selectedAnimals)
                        {
                            anim.Deselect();
                        }
                        m_selectedAnimals.Clear();

                        //Se selecciona solo al animal al que se le dio click.
                        animal.Select();
                        m_selectedAnimals.Add(animal);
                        Debug.Log(objectTransform.name);
                    }

                }
            }
            else
            {
                //Limpiar la lista de animales seleccionados.
                foreach(var anim in m_selectedAnimals)
                {
                    anim.Deselect();
                }
                m_selectedAnimals.Clear();

                float width = Mathf.Abs(m_finalViewPos.x - m_initViewPos.x);
                float height = Mathf.Abs(m_finalViewPos.y - m_initViewPos.y);

                //Se crea la caja de seleccion a partir del punto izquierdo superior.
                //No importa el punto inicial del click.
                Vector2 rectInitPoint = new Vector2();
                rectInitPoint.x = m_initViewPos.x <= m_finalViewPos.x ?
                                  m_initViewPos.x :
                                  m_finalViewPos.x;

                rectInitPoint.y = m_initViewPos.y > m_finalViewPos.y ?
                                  m_initViewPos.y :
                                  m_finalViewPos.y;

                var animalObjs = FindObjectsOfType<Animal>();

                //Se revisa si alguno de los animales esta dentro de la caja de seleccion.
                for (int i = 0; i < animalObjs.Length; ++i)
                {
                    Vector2 objScreenPos = m_camera.WorldToViewportPoint(animalObjs[i].transform.position);

                    if (objScreenPos.x > rectInitPoint.x &&
                        objScreenPos.y < rectInitPoint.y &&
                        objScreenPos.x < rectInitPoint.x + width &&
                        objScreenPos.y > rectInitPoint.y - height)
                    {
                        Debug.Log(animalObjs[i].name);
                        animalObjs[i].Select();
                        m_selectedAnimals.Add(animalObjs[i]);
                    }
                }
            }
            m_isSelected = false;
        }

        if(Input.GetMouseButtonUp(1))
        {
            //Se lanza un rayo desde la posicion del mouse para detectar coordenadas para movimiento.
            RaycastHit hit;
            Ray selectionRay = m_camera.ScreenPointToRay(Input.mousePosition);

            //Si el rayo choca conta algo, entonces se guardan las coordenadas para el movimiento.
            if(Physics.Raycast(selectionRay, out hit))
            {
                Vector3 position = hit.point;
                position.y = m_floor.position.y + FLOOR_OFFSET;
                
                //Se revisa la lista de objetos seleccionados para moverlos a la posicion.
                foreach (var anim in m_selectedAnimals)
                {
                    anim.transform.position = position;
                }
            }
        }
    }
}
