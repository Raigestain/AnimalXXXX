using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Agent
{
    public const int ANIMATION = 0;
    public ANIMAL_STATES m_changeState;

    //Tipo del animal... jeje saludos.
    public ANIMAL_TYPES m_type;

    // Start is called before the first frame update
    protected void Start()
    {
        m_changeState = ANIMAL_STATES.WALK;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // llamamos el update del padre
        base.Update();
    }

    public void SetState(ANIMAL_STATES newState)
    {
        m_changeState = newState;
    }
}
