using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * <summary>
 * A class that is responsible for the UI
 * when the player levels up.
 * </summary>
 */
public class LevelUpMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private SkillsManager skillsManager;

    /**
     * <summary>
     * An array of the 3 skills that a player 
     * can choose from on leveling up.
     * </summary>
     */
    private GameObject[] selectedSkills;

    /**
     * <summary>
     * Sets all skills to be a child of this UI.
     * </summary>
     */
    private void Awake()
    {
        this.skillsManager.SkillsLoaded += SetParents;
        this.skillsManager.SkillsGenerated += DisplaySkills;
        Skill.Selected += ResetSkills;
    }

    private void Start()
    {
        Player.instance.Death += StopDisplaySkills;
    }

    private void OnDestroy()
    {
        this.skillsManager.SkillsLoaded -= SetParents;
        this.skillsManager.SkillsGenerated -= DisplaySkills;
        Skill.Selected -= ResetSkills;
        Player.instance.Death -= StopDisplaySkills;
    }

    private void SetParents(SkillsManager sender, EventArgs e)
    {
        List<GameObject> skillsLibrary = this.skillsManager.SkillsLibrary;
        List<GameObject> signatureSkillsLibrary = this.skillsManager.SignatureSkillsLibrary;
        SetParent(skillsLibrary);
        SetParent(signatureSkillsLibrary);
    }

    /**
     * <summary>
     * Sets the parent of all the skill game objects 
     * in the list to this gameobject.
     * </summary>
     */
    private void SetParent(List<GameObject> lst)
    {
        foreach (GameObject skill in lst)
        {
            skill.transform.SetParent(this.transform);
            skill.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    /**
     * <summary>
     * Displays all the skills in selectedSkills
     * and their repsective levels.
     * </summary>
     */
    private void DisplaySkills(SkillsManager sender, EventArgs e)
    {
        this.selectedSkills = this.skillsManager.SelectedSkills;
        if (this.selectedSkills != null)
        {
            for (int i = 0; i < this.selectedSkills.Length; i++)
            {
                GameObject skill = this.selectedSkills[i];
                if (skill != null)
                {
                    skill.transform.SetParent(this.background.transform.GetChild(i));
                    skill.GetComponentsInChildren<TextMeshProUGUI>()[3].text = skill.GetComponent<Skill>().ToString();
                    skill.SetActive(true);
                }
            }
        }
        this.background.SetActive(true);
    }

    private void ResetSkills(Skill sender, EventArgs e)
    {
        foreach (GameObject skill in this.selectedSkills)
        {
            if (skill != null)
            {
                skill.SetActive(false);
                skill.transform.SetParent(this.transform);
            }
        }
        this.background.SetActive(false);
    }

    private void StopDisplaySkills(Character sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }
}
