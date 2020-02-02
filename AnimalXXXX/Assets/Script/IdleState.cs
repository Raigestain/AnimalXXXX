using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSM
{
    public override void onEntry()
    {
        Debug.Log("Entro a Idle");
        m_ID = STATES.S_IDLE;
    }

    // Update is called once per frame
    public override STATES Update()
    {
        Animal tempAnimal = GetComponent<Animal>();

        if (tempAnimal.m_type == ANIMAL_TYPES.CHICKEN)
        {
            if(tempAnimal.m_changeState == ANIMAL_STATES.IDLE)
            {
                return STATES.S_IDLE;
            }
            return STATES.S_WALK;
        }
        else if(tempAnimal.m_type == ANIMAL_TYPES.DOG)
        {
            return STATES.S_PATROL;
        }
        else if (tempAnimal.m_type == ANIMAL_TYPES.ALPACA)
        {
            return STATES.S_RUN;
        }

        return STATES.S_IDLE;
    }

    public override void onExist()
    {
        Debug.Log("Salio del Idle");
    }
}

