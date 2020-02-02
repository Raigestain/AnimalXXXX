using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Agent
{
    private bool m_isSelected = false;
    public const int ANIMATION = 0;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // llamamos el update del padre
        base.Update();
    }

    public void Select()
    {
        m_isSelected = true;
    }

    public void Deselect()
    {
        m_isSelected = false;
    }
}
