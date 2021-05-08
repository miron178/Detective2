using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    [SerializeField]
    string[] causeOfDeath;

    [System.Serializable]
    class Person
    {
        public string name;
        [NonSerialized]
        public int guessCauseOfDeath  = -1;
        public int actualCauseOfDeath = -1;
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
            for(int i = 0; i < causeOfDeath.Length; i++)
            {
                Dropdown.OptionData option = new Dropdown.OptionData
                {
                    text = causeOfDeath[i]
                };
                codUI.options.Add(option);
                if (i == person.guessCauseOfDeath)
                {
                    //select this option
                    codUI.value = i;
                }
            }

            codUI.onValueChanged.AddListener(delegate { CauseOfDeathSelected(person, codUI); });
        }

        //set content panel size
        RectTransform panelRectTransform = characterListPanel.GetComponent<RectTransform>();
        panelRectTransform.sizeDelta = new Vector2(width * 0.5f, -y);
    }

    private void CauseOfDeathSelected(Person person, Dropdown codUI)
    {
        person.guessCauseOfDeath = codUI.value;
        if (person.guessCauseOfDeath == person.actualCauseOfDeath)
        {
            codUI.interactable = false;
            codUI.GetComponentInParent<Image>().color = Color.green;

            CheckVictory();
        }
    }
    
    private bool CheckVictory()
    {
        foreach(Person person in characters)
        {
            if(person.guessCauseOfDeath != person.actualCauseOfDeath)
            {
                return false;
            }
        }
        //TODO: add victory clause, open exit||change scene?
        Debug.Log("YOU WON");
        return true;
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
}
