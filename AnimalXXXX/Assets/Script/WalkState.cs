using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : FSM
{
    Animal m_agent;

    public override void onEntry()
    {
        Debug.Log("Entro a Walk");
        m_ID = STATES.S_WALK;
        GetComponent<Animator>().SetInteger("animation", (int)ANIMAL_STATES.WALK);
        m_agent = GetComponent<Animal>();
    }

    // Update is called once per frame
    public override STATES Update()
    {
        if(m_agent.m_changeState == ANIMAL_STATES.IDLE)
        {
            return STATES.S_IDLE;
        }
        return STATES.S_WALK;
    }

    public override void onExist()
    {
        Debug.Log("Salio del Walk");
    }
}
