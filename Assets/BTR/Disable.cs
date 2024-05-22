using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    public Camera BTRñamera;
    public MonoBehaviour script1;
    public MonoBehaviour script2;

    void Start()
    {
        if (script1 != null)
        {
            script1.enabled = false;
        }

        if (script2 != null)
        {
            script2.enabled = false;
        }

        if (BTRñamera != null)
        {
            BTRñamera.gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        if (script1 != null)
        {
            script1.enabled = false;
        }

        if (script2 != null)
        {
            script2.enabled = false;
        }

        if (BTRñamera != null)
        {
            BTRñamera.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        if (script1 != null)
        {
            script1.enabled = true;
        }

        if (script2 != null)
        {
            script2.enabled = true;
        }

        if (BTRñamera != null)
        {
            BTRñamera.gameObject.SetActive(true);
        }
    }
}