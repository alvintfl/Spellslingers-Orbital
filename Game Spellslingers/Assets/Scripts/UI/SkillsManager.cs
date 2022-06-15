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
     * A list of player skills.
     * </summary>
     */
    private List<GameObject> skillsLibrary;

    /**
     * <summary>
     * A list of player skills.
     * </summary>
     */
    private List<GameObject> signatureSkillsLibrary;

    /**
     * <summary>
     * An array of the 3 skills that a player 
     * can choose from on leveling up.
     * </summary>
     */
    private GameObject[] selectedSkills;

    /**
     * <summary>
     * Prevent the same skills from being selected.
     * </summary>
     */
    private HashSet<int> seen;

    /**
     * <summary>
     * The level needed to obtain a signature skill.
     * </summary>
     */
    private readonly int signatureSkillRequirement = 10;
    public delegate void SkillsEventHandler<T, U>(T sender, U eventArgs);
    public event SkillsEventHandler<SkillsManager, EventArgs> SkillsGenerated;

    /**
     * <summary>
     * Loads all skills from the resources folder 
     * into their respective skills libraries 
     * and instantiates them. The skills libraries 
     * remove the skills when they are maxed out.
     * </summary>
     */
    private void Awake()
    {
        GameObject[] skillPrefabs = Resources.LoadAll<GameObject>("Skills/");
        this.skillsLibrary = new List<GameObject>();
        this.signatureSkillsLibrary = new List<GameObject>();
        this.seen = new HashSet<int>();
        for (int i = 0; i < skillPrefabs.Length; i++)
        {
            GameObject skillObject = Instantiate(skillPrefabs[i]);
            skillObject.SetActive(false);
            Skill skill = skillObject.GetComponentInChildren<Skill>();
            skill.MaxedOut +=
                (Skill sender, EventArgs e) => this.skillsLibrary.Remove(skillObject);
            skill.Reset();
            if (skill.IsSignatureSkill())
            {
                this.signatureSkillsLibrary.Add(skillObject);
            }
            else
            {
                this.skillsLibrary.Add(skillObject);
            }
        }
    }

    private void Start()
    {
        this.selectedSkills = new GameObject[3];
        ExpManager.LevelUp += GenerateSkills;
    }

    public List<GameObject> SkillsLibrary { get { return this.skillsLibrary; } }
    public List<GameObject> SignatureSkillsLibrary { get { return this.signatureSkillsLibrary; } }

    public GameObject[] SelectedSkills { get { return this.selectedSkills; } }

    /**
     * <summary>
     * Shuffles the list of skills in skillsLibrary and randomly
     * select 3 skills without replacement, storing them in 
     * selectedSkills.
     * </summary>
     */
    private void GenerateSkills(ExpManager sender, EventArgs e)
    {
        if (sender.Level == this.signatureSkillRequirement)
        {
            GenerateSignatureSkills();
            return;
        }

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
            for (int i = 0; i < this.selectedSkills.Length; i++)
            {
                int j = random.Next(0, this.skillsLibrary.Count - 1);
                GameObject skillObject = this.skillsLibrary[j];
                while (this.seen.Contains(j))
                {
                    j = random.Next(0, this.skillsLibrary.Count - 1);
                    skillObject = this.skillsLibrary[j];
                }
                this.seen.Add(j);
                skillObject.SetActive(true);
                this.selectedSkills[i] = skillObject;
            }
            this.seen.Clear();
        }
        else
        {
            for (int i = 0; i < this.skillsLibrary.Count; i++)
            {
                GameObject skillObject = this.skillsLibrary[i];
                if (skillObject != null)
                {
                    skillObject.SetActive(true);
                    this.selectedSkills[i] = skillObject;
                }
            }
        }
        OnSkillGenerated(EventArgs.Empty);
    }

    private void GenerateSignatureSkills()
    {
        for (int i = 0; i < this.signatureSkillsLibrary.Count; i++)
        {
            GameObject skillObject = this.signatureSkillsLibrary[i];
            if (skillObject != null)
            {
                skillObject.SetActive(true);
                this.selectedSkills[i] = skillObject;
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
