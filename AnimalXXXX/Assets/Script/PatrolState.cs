using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : FSM
{
    public override void onEntry()
    {
        Debug.Log("Entro a Patrol");
        GetComponent<Animator>().SetInteger("animation", (int)ANIMAL_STATES.WALK);
        m_ID = STATES.S_PATROL;
    }

    // Update is called once per frame
    public override STATES Update()
    {
        return STATES.S_PATROL;
    }

    public override void onExist()
    {
        Debug.Log("Salio del Patrol");
    }
}
