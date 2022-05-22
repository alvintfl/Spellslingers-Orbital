using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private SkillsManager skillsManager;
    private GameObject[] selectedSkills;

    private void Start()
    {
        this.skillsManager.SkillsGenerated += DisplaySkills;
        List<GameObject> skillsLibrary = this.skillsManager.SkillsLibrary;
        foreach (GameObject skill in skillsLibrary)
        {
            skill.transform.SetParent(this.transform);
        }
        Skill.Selected += ResetSkills;
    }

    private void OnDisable()
    {
        this.skillsManager.SkillsGenerated -= DisplaySkills;
        Skill.Selected -= ResetSkills;
    }

    private void DisplaySkills (object sender, EventArgs e)
    {
        this.selectedSkills = this.skillsManager.SelectedSkills;
        if (this.selectedSkills != null)
        {
            for (int i = 0; i < this.selectedSkills.Length; i++)
            {
                GameObject skill = this.selectedSkills[i];
                if (skill != null)
                {
                    int yCoordinate = i == 0 ? 69 : i == 1 ? -64 : -197;
                    skill.GetComponent<RectTransform>().anchoredPosition = new Vector2(33, yCoordinate);
                    skill.GetComponentsInChildren<TextMeshProUGUI>()[3].text = skill.GetComponent<Skill>().ToString();
                }
            }
        }
        this.background.SetActive(true);   
    }

    private void ResetSkills (object sender, EventArgs e)
    {
        foreach (GameObject skill in this.selectedSkills)
        {
            skill.SetActive(false);
        }
        this.background.SetActive(false);   
    }
}
