using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : FSM
{

    public override void onEntry()
    {
        Debug.Log("Entro a Charge");
        GetComponent<Animator>().SetInteger("animation", (int)ANIMAL_STATES.RUN);
        m_ID = STATES.S_CHARGE;
    }

    // Update is called once per frame
    public override STATES Update()
    {
        return STATES.S_CHARGE;
    }

    public override void onExist()
    {
        Debug.Log("Salio del Charge");
    }
}
