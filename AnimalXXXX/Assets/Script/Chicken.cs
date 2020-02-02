using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    private Animator m_animator;
    private ANIMAL_STATES m_state;


    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        m_animator = GetComponent<Animator>();

        Vector3 target = GameObject.Find("ChickenSpawnTarget").transform.position;

        setTargetPos(target);
    }

    // Update is called once per frame
    private void Update()
    {
        base.Update();

        if(m_speed > 0 && m_state != ANIMAL_STATES.WALK)
        {
            m_animator.SetInteger("animation", (int)ANIMAL_STATES.WALK);
        }

    }
}
