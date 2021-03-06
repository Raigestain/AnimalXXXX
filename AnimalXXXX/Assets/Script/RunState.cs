﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : FSM
{
    public override void onEntry()
    {
        Debug.Log("Entro a Run");
        GetComponent<Animator>().SetInteger("animation", (int)ANIMAL_STATES.RUN);
        m_ID = STATES.S_RUN;
    }

    // Update is called once per frame
    public override STATES Update()
    {
        Animal tempAnimal = GetComponent<Animal>();
        if (tempAnimal.m_changeState == ANIMAL_STATES.WALK)
        {
            return STATES.S_WALK;
        }
        return STATES.S_RUN;
    }

    public override void onExist()
    {
        Debug.Log("Salio del Run");
    }
}
