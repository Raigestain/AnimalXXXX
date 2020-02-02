using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Agent
{
    public const int ANIMATION = 0;
    public ANIMAL_STATES m_changeState;

    //Tipo del animal... jeje saludos.
    public ANIMAL_TYPES m_type;

    private Resources m_package;

    public bool m_deliveredPackage = false;
    public bool m_inDelivery = false;

    // Start is called before the first frame update
    protected void Start()
    {        
        m_changeState = ANIMAL_STATES.WALK;
        base.Start();
        if(GetComponentInChildren<ParticleSystem>())
        {
            GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // llamamos el update del padre
        base.Update();

        if(null != m_package)
        {
            m_package.UpdatePosition();
        }
    }

    public void SetState(ANIMAL_STATES newState)
    {
        m_changeState = newState;
    }

    public Resources GetPackage()
    {
        Resources tmpResource = m_package;
        m_package = null;
        return tmpResource;
    }

    public void SetPackage(Resources newPackage)
    {
        m_package = newPackage;
    }
}
