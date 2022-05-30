using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/**
 * <summary>
 * A class that holds all of the skills that
 * the player has not chosen and generates the 
 * skills that the player can choose from upon 
 * leveling up.
 * </summary>
 */
public class SkillsManager : MonoBehaviour
{
    /**
     * <summary>
     * A list of all player skills.
     * </summary>
     */
    private List<GameObject> skillsLibrary;

    /**
     * <summary>
     * An array of the 3 skills that a player 
     * can choose from on leveling up.
     * </summary>
     */
    private GameObject[] selectedSkills;
    public event EventHandler SkillsGenerated;

    /**
     * <summary>
     * Loads all skills from the resources folder into
     * the skillsLibrary list and instantiates them.
     * The skillsLibrary list also subscribes to the maxed out
     * event of the skills, removing them from the list when
     * they are maxed out.
     * </summary>
     */
    private void Awake()
    {
        GameObject[] skillPrefabs = Resources.LoadAll<GameObject>("Skills/");
        this.skillsLibrary = new List<GameObject>();
        for (int i = 0; i < skillPrefabs.Length; i++)
        {
            GameObject skillObject = Instantiate(skillPrefabs[i]);
            skillObject.SetActive(false);
            Skill skill = skillObject.GetComponentInChildren<Skill>();
            skill.MaxedOut += 
                (sender, e) => this.skillsLibrary.Remove(skillObject);
            skill.Reset();
            this.skillsLibrary.Add(skillObject);
        }
    }

    private void Start()
    {
        this.selectedSkills = new GameObject[3];
        ExpManager.LevelUp += GenerateSkills;
    }
    public List<GameObject> SkillsLibrary { get { return this.skillsLibrary; } }

    public GameObject[] SelectedSkills { get { return this.selectedSkills; } }

    /**
     * <summary>
     * Shuffles the list of skills in skillsLibrary and randomly
     * select 3 skills without replacement, storing them in 
     * selectedSkills.
     * </summary>
     */
    private void GenerateSkills(object sender, EventArgs e)
    {
        //Fisher-Yates shuffle
        Random random = new Random();
        for (int i = this.skillsLibrary.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            GameObject temp = this.skillsLibrary[i];
            this.skillsLibrary[i] = this.skillsLibrary[j];
            this.skillsLibrary[j] = temp;
        }

        if (this.skillsLibrary.Count > 3)
        {
            // Select without replacement
            Dictionary<int, bool> seen = new Dictionary<int, bool>();
            for (int i = 0; i < this.selectedSkills.Length; i++)
            {
                int j = random.Next(0, this.skillsLibrary.Count - 1);
                GameObject skillObject = this.skillsLibrary[j];
                while (seen.ContainsKey(j))
                {
                    j = random.Next(0, this.skillsLibrary.Count - 1);
                    skillObject = this.skillsLibrary[j];
                }
                seen.Add(j, true);
                skillObject.SetActive(true);
                this.selectedSkills[i] = skillObject;
                //Debug.Log(skillObject.name);
            }
        } else
        {
            for (int i = 0; i < this.skillsLibrary.Count; i++)
            {
                GameObject skillObject = this.skillsLibrary[i];
                //Debug.Log(skillObject.name);
                if (skillObject != null)
                {
                    skillObject.SetActive(true);
                    this.selectedSkills[i] = skillObject;
                }
            }
        }
        OnSkillGenerated(EventArgs.Empty);
    }

    private void OnDisable()
    {
        ExpManager.LevelUp -= GenerateSkills;
    }

    private void OnSkillGenerated(EventArgs e)
    {
        SkillsGenerated?.Invoke(this, e);
    }
}
