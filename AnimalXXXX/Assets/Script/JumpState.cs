using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : FSM
{
    public override void onEntry()
    {
        Debug.Log("Entro a Jump");
        m_ID = STATES.S_JUMP;
    }

    // Update is called once per frame
    public override STATES Update()
    {
        return STATES.S_JUMP;
    }

    public override void onExist()
    {
        Debug.Log("Salio del Jump");
    }
}
