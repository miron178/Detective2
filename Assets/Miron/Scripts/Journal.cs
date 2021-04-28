using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    private bool axisInUse = false;

    private void Update()
    {
        float axis = Input.GetAxisRaw("Journal");
        if (!axisInUse && axis > 0)
        {
            axisInUse = true;
            ToggleJournal();
        }
        else if (axis == 0)
        {
            axisInUse = false;
        }
    }

    void ToggleJournal()
    {
        bool show = !menu.activeSelf;

        menu.SetActive(show);
        GameManager.Instance.Pause(show);
    }

    void OnGUI()
    {
        //Cursor.visible = true;
    }
}
