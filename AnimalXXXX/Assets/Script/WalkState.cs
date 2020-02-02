using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : FSM
{
    public override void onEntry()
    {
        Debug.Log("Entro a Walk");
        m_ID = STATES.S_WALK;
    }

    // Update is called once per frame
    public override STATES Update()
    {
        return STATES.S_WALK;
    }

    public override void onExist()
    {
        Debug.Log("Salio del Walk");
    }
}
