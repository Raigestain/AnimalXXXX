using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : FSM
{
    public override void onEntry()
    {
        Debug.Log("Entro a Die");
        m_ID = STATES.S_DIE;
    }

    // Update is called once per frame
    public override STATES Update()
    {
        return STATES.S_DIE;
    }

    public override void onExist()
    {
        Debug.Log("Salio del Die");
    }
}
