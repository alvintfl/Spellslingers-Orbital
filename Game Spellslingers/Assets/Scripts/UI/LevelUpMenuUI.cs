using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private SkillsManager skillsManager;

    private void Start()
    {
        this.skillsManager.GetComponent<SkillsManager>().SkillsGenerated += DisplaySkills;
        Skill.Selected += ToggleOff;
    }

    private void ToggleOn()
    {
        this.background.SetActive(true);   
    }

    private void ToggleOff(object sender, EventArgs e)
    {
        this.background.SetActive(false);   
    }

    private void DisplaySkills (object sender, EventArgs e)
    {
        GameObject[] arr = this.skillsManager.SelectedSkills;
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                GameObject skill = arr[i];
                skill.transform.SetParent(this.transform);
                int yCoordinate = i == 0 ? 69 : i == 1 ? -64 : -197;
                skill.GetComponent<RectTransform>().anchoredPosition = new Vector2(33, yCoordinate);
            }
        }
        ToggleOn();
    }
}
