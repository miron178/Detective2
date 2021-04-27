using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIText : MonoBehaviour
{
    [SerializeField]
    private bool vanishOnStart = false;
    [SerializeField]
    private Text ui = null;
    [SerializeField]
    private string[] messages = null;

    [SerializeField]
    private float timeToWait = 2f;
    private IEnumerator coroutine;

    void Start()
    {
        ui.text = "";
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
        if (ui != null)
        {
            ui.gameObject.SetActive(enable);
            if(enable)
            {
                coroutine = ChangeText();
                StartCoroutine(coroutine);
            }
            else
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                ui.text = "";
            }
        }
    }

    IEnumerator ChangeText()
    {
        for (int i = 0; i < messages.Length; i++)
        {
            ui.text = messages[i];
            yield return new WaitForSeconds(timeToWait);
        }
        ui.text = "";
    }
}

