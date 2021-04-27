using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximity : MonoBehaviour
{
    [SerializeField]
    private bool vanishOnStart = false;

    void Start()
    {
        EnableChildren(!vanishOnStart);
    }

    void OnTriggerEnter(Collider other)
    {
        EnableChildren(vanishOnStart);
    }
    
    void OnTriggerExit(Collider other)
    {
        EnableChildren(!vanishOnStart);
    }

    void EnableChildren(bool enable)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(enable);
    }
}
