using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool m_isSelectionComplete = false; //Se usa para cuando se solto el click izquierdo.
    private bool m_isSelecting = false; //Se usa mientras el click izquierdo esta apretado.
    private Vector3 m_initViewPos;
    private Vector3 m_finalViewPos;
    private Camera m_camera;
    private List<Animal> m_selectedAnimals;
    private Transform m_floor;
    private const float FLOOR_OFFSET = 0.2f;

    [SerializeField]
    private float m_deathZone = 0.2f;

    // World position
    Vector3 wsPoint1 = new Vector3();
    Vector3 wsPoint2 = new Vector3();
    Vector3 point1 = new Vector3();
    Vector3 point2 = new Vector3();
    Ray OBBdir1 = new Ray();
    Ray OBBdir2 = new Ray();
    Ray OBBdir3 = new Ray();
    Ray OBBdir4 = new Ray();
    Vector3 normal1 = new Vector3();
    Vector3 normal2 = new Vector3();
    Vector3 normal3 = new Vector3();
    Vector3 normal4 = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
        m_selectedAnimals = new List<Animal>();
        //m_floor = GameObject.Find("Floor").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !m_isSelecting)
        {
            point1 = Input.mousePosition;
            wsPoint1 = m_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_camera.nearClipPlane));

            // Cambiamos bandera a verdadero
            m_isSelecting = true;
        }

        if (Input.GetMouseButtonUp(0) && !m_isSelectionComplete)
        {
            point2 = Input.mousePosition;
            wsPoint2 = m_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_camera.nearClipPlane));

            m_selectedAnimals.Clear();

            // Cambiamos bandera a verdadero
            m_isSelectionComplete = true;
        }

        if (m_isSelectionComplete && m_isSelecting)
        {
            //Se usa un death zone, si el movimiento es menor a este, cuenta como seleccion individual.
            float distance = (point2 - point1).magnitude;
            if (distance <= m_deathZone)
            {
                //Se lanza un rayo para saber si choco contra un objeto.
                RaycastHit hit;
                Ray selectionRay = m_camera.ScreenPointToRay(Input.mousePosition);

                //Si choca y es un animal, lo selecciona.
                if (Physics.Raycast(selectionRay, out hit))
                {
                    Transform objectTransform = hit.transform;

                    Animal animal = objectTransform.GetComponent<Animal>();
                    if (null != animal)
                    {
                        m_selectedAnimals.Add(animal);
                        //Debug.Log(objectTransform.name);
                    }
                }
            }
            else
            {
                // Sacamos dos rashoooos laser
                OBBdir1 = m_camera.ScreenPointToRay(new Vector3(Mathf.Min(point1.x, point2.x), Mathf.Max(point1.y, point2.y), 0.0f)); // left top
                OBBdir2 = m_camera.ScreenPointToRay(new Vector3(Mathf.Max(point1.x, point2.x), Mathf.Max(point1.y, point2.y), 0.0f)); // right top
                OBBdir3 = m_camera.ScreenPointToRay(new Vector3(Mathf.Min(point1.x, point2.x), Mathf.Min(point1.y, point2.y), 0.0f)); // left bottom 
                OBBdir4 = m_camera.ScreenPointToRay(new Vector3(Mathf.Max(point1.x, point2.x), Mathf.Min(point1.y, point2.y), 0.0f)); // right bottom

                var left = Mathf.Min(wsPoint1.x, wsPoint2.x);
                var top = Mathf.Max(wsPoint1.y, wsPoint2.y);
                var right = Mathf.Max(wsPoint1.x, wsPoint2.x);
                var bot = Mathf.Min(wsPoint1.y, wsPoint2.y);
                var near = Mathf.Min(wsPoint1.z, wsPoint2.z);

                // Calculamos las normales
                normal1 = Vector3.Cross(OBBdir1.direction, (new Vector3(right, top, near) - new Vector3(left, top, near)).normalized); // normal top
                normal2 = Vector3.Cross(OBBdir2.direction, (new Vector3(right, bot, near) - new Vector3(right, top, near)).normalized); // normal right
                normal3 = Vector3.Cross(OBBdir3.direction, (new Vector3(left, top, near) - new Vector3(left, bot, near)).normalized); // normal left
                normal4 = Vector3.Cross(OBBdir4.direction, (new Vector3(left, bot, near) - new Vector3(right, bot, near)).normalized); // normal bot

                // Pedimos la lista de los animales e iteramos para checar quien esta dentro de la seleccion
                var animalObjs = FindObjectsOfType<Animal>();
                for (int i = 0; i < animalObjs.Length; ++i)
                {
                    var pos = animalObjs[i].transform.position;
                    if (Vector3.Dot(pos - new Vector3(left, top, near), normal1) < 0.0f &&
                        Vector3.Dot(pos - new Vector3(right, top, near), normal2) < 0.0f &&
                        Vector3.Dot(pos - new Vector3(left, bot, near), normal3) < 0.0f &&
                        Vector3.Dot(pos - new Vector3(right, bot, near), normal4) < 0.0f)
                    {
                        m_selectedAnimals.Add(animalObjs[i]);
                        //Debug.Log(animalObjs[i].name);
                    }
                    else
                    {
                        //Debug.Log("Fuera: " + animalObjs[i].name);
                    }
                }
            }

            m_isSelectionComplete = false;
            m_isSelecting = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            //Se lanza un rayo desde la posicion del mouse para detectar coordenadas para movimiento.
            RaycastHit hit;
            Ray selectionRay = m_camera.ScreenPointToRay(Input.mousePosition);

            //Si el rayo choca conta algo, entonces se guardan las coordenadas para el movimiento.
            if (Physics.Raycast(selectionRay, out hit))
            {

                if(hit.transform.gameObject.layer == (int)LAYERS.FLOOR)
                {
                    Transform floor = hit.transform;
                    Vector3 position = hit.point;
                    position.y = FLOOR_OFFSET;

                    //Se revisa la lista de objetos seleccionados para moverlos a la posicion.
                    foreach (var anim in m_selectedAnimals)
                    {
                        anim.setTargetPos(position);
                    }
                }
            }
        }
    }
}
