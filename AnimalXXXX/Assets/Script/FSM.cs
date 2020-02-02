using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATES { S_NONE, S_IDLE, S_WALK, S_RUN, S_JUMP, S_CHARGE, S_DELIVER, S_DIE, S_PATROL };

public class FSM : MonoBehaviour
{
    public STATES m_ID;

    /*
     * FSM BASE
     */
    public virtual void onEntry()
    {
        m_ID = STATES.S_NONE;
    }

    // Update is called once per frame
    public virtual STATES Update()
    {
        return STATES.S_NONE;
    }

    public virtual void onExist()
    {
    }
}
