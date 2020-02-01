using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transmutation : MonoBehaviour
{
    public Material matofObject;
    public GameObject _InvLight;
    public ParticleSystem _InvParticle;
    public Color initialColor = new Color(255.0f, 255.0f, 255.0f, 255.0f);
    public Color newColor = new Color(172.0f, 0.0f, 0.0f, 255.0f);
    public float duration = 3.0f;
    public bool isActive = false;
    public float velocity = 0.0f;

    private bool doOnce = true;
    private bool secuenceFinished = false;
    private bool changeColor = false;
    private float elapsedTime = 0.0f;
    private Vector3 InvLightInitialPosition;
    private float transInvLight = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0.0f;
        _InvParticle.Stop();
        InvLightInitialPosition = _InvLight.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            elapsedTime += Time.deltaTime;
            float transCircleTime = Mathf.Clamp(elapsedTime, 0.0f, duration);

            matofObject.SetColor("_Color",         Color.Lerp(Color.white, newColor, transCircleTime / duration));
            matofObject.SetColor("_EmissionColor", Color.Lerp(Color.white, newColor, transCircleTime / duration));
            
            if (null != _InvLight && elapsedTime > duration && elapsedTime >= duration)
            {
                transInvLight = Mathf.Clamp(elapsedTime, duration, duration + duration) - duration;

                Vector3 newPos = Vector3.Lerp(InvLightInitialPosition, new Vector3(InvLightInitialPosition.x, InvLightInitialPosition.y + 1.3f, InvLightInitialPosition.z), transInvLight);
                _InvLight.transform.position = newPos;

                if (transInvLight == (duration + duration) - duration)
                {
                    secuenceFinished = true;
                }
            }

            if (secuenceFinished)
            {
                if (doOnce)
                {
                    _InvParticle.Play();
                    doOnce = false;
                }                
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            float transCircleTime = Mathf.Clamp(elapsedTime, 0.0f, duration);

            transInvLight = Mathf.Clamp(elapsedTime, duration, duration + duration) - duration;

            Vector3 newPos = Vector3.Lerp(InvLightInitialPosition, new Vector3(InvLightInitialPosition.x, InvLightInitialPosition.y - 1.3f, InvLightInitialPosition.z), transInvLight);
            _InvLight.transform.position = newPos;

            if (changeColor)
            {
                matofObject.SetColor("_Color", Color.Lerp(newColor, Color.white, elapsedTime / duration));
                matofObject.SetColor("_EmissionColor", Color.Lerp(newColor, Color.white, elapsedTime / duration));
            }            
        }
    }

    public void Activate()
    {
        elapsedTime = 0.0f;
        transInvLight = 0.0f;
        isActive = true;
        changeColor = false;
    }

    public void Deactivate()
    {
        elapsedTime = 0.0f;
        transInvLight = 0.0f;
        isActive = false;
        secuenceFinished = false;
        changeColor = true;
        doOnce = true;
    }
}
