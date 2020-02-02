using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverState : FSM
{
    public override void onEntry()
    {
        Debug.Log("Entro a Deliver");
        GetComponent<Animator>().SetInteger("animation", (int)ANIMAL_STATES.WALK);
        m_ID = STATES.S_DELIVER;
    }

    // Update is called once per frame
    public override STATES Update()
    {
        return STATES.S_DELIVER;
    }

    public override void onExist()
    {
        Debug.Log("Salio del Deliver");
    }
}
