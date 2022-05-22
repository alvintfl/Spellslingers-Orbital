using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SkillsManager : MonoBehaviour
{
    private List<GameObject> skillsLibrary;
    private GameObject[] selectedSkills;
    public event EventHandler SkillsGenerated;

    private void Awake()
    {
        Debug.Log("Starting");
        GameObject[] skillPrefabs = Resources.LoadAll<GameObject>("Skills/");
        this.skillsLibrary = new List<GameObject>();
        for (int i = 0; i < skillPrefabs.Length; i++)
        {
            GameObject skillObject = Instantiate(skillPrefabs[i]);
            skillObject.SetActive(false);
            skillObject.GetComponent<Skill>().MaxedOut += 
                (sender, e) => this.skillsLibrary.Remove(skillObject);
            this.skillsLibrary.Add(skillObject);
        }
        Debug.Log("Ending");
    }

    private void Start()
    {
        this.selectedSkills = new GameObject[3];
        ExpManager.LevelUp += GenerateSkills;
    }
    public List<GameObject> SkillsLibrary { get { return this.skillsLibrary; } }

    public GameObject[] SelectedSkills { get { return this.selectedSkills; } }

    private void GenerateSkills(object sender, EventArgs e)
    {
        Debug.Log("Generating");
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
            }
        } else
        {
            for (int i = 0; i < this.skillsLibrary.Count; i++)
            {
                GameObject skillObject = this.skillsLibrary[i];
                skillObject.SetActive(true);
                this.selectedSkills[i] = skillObject;
            }
        }
        OnSkillGenerated(EventArgs.Empty);
        Debug.Log("End-Generating");
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
