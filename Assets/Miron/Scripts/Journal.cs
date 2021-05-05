using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    enum CauseOfDeath
    {
        unknown,
        one,
        two
    };

    [System.Serializable]
    class Person
    {
        public string name;
        [NonSerialized]
        public CauseOfDeath guessCauseOfDeath = CauseOfDeath.unknown;
        public CauseOfDeath actualCauseOfDeath;
    };

    [SerializeField]
    private Person[] characters;

    [SerializeField]
    private GameObject menu;
    private bool axisInUse = false;

    [SerializeField]
    private Transform characterListPanel;
    [SerializeField]
    private GameObject characterListItemPrefab;

    private void Start()
    {
        float width = 0f;
        float y = 0;
        
        //add all characters
        foreach (Person person in characters)
        {
            //add UI prefab for this character
            GameObject itemUI = GameObject.Instantiate<GameObject>(characterListItemPrefab, characterListPanel);
            RectTransform rectTransform = itemUI.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(rectTransform.rect.width * 0.5f, y - rectTransform.rect.height * 0.5f, 0);
            width = rectTransform.rect.width;
            y -= rectTransform.rect.height;

            //set names
            Text nameUI = itemUI.GetComponentInChildren<Text>();
            nameUI.text = person.name;

            //set dropdown options
            Dropdown codUI = itemUI.GetComponentInChildren<Dropdown>();
            codUI.options.Clear();
            foreach (CauseOfDeath cod in Enum.GetValues(typeof(CauseOfDeath)))
            {
                Dropdown.OptionData option = new Dropdown.OptionData
                {
                    text = cod.ToString()
                };
                codUI.options.Add(option);
                if (cod == person.guessCauseOfDeath)
                {
                    //select this option - it's the last one (so far)
                    codUI.value = codUI.options.Count - 1;
                }
            }
        }

        //set content panel size
        RectTransform panelRectTransform = characterListPanel.GetComponent<RectTransform>();
        panelRectTransform.sizeDelta = new Vector2(width * 0.5f, -y);
    }

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

#if false
    private string CauseOfDeathToString(CauseOfDeath cod)
    {
        switch (cod)
        {
            case CauseOfDeath.one:
                return "one";
            case CauseOfDeath.two:
                return "two";
            default:
                Debug.LogError("unknown cause of death");
                return "???";
        }
    }
#endif
}
