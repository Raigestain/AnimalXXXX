using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static int m_chickenCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddChickens(uint number)
    {
        m_chickenCount += (int)number;
    }

    public static void ExpendChickens(uint number)
    {
        m_chickenCount -= (int)number;
    }
}
