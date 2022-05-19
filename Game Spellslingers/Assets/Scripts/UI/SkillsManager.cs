using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SkillsManager : MonoBehaviour
{
    private GameObject[] skillsLibrary;
    private GameObject[] selectedSkills;
    public event EventHandler SkillsGenerated;

    private void Start()
    {
        this.skillsLibrary = Resources.LoadAll<GameObject>("Skills/");
        this.selectedSkills = new GameObject[3];
        ExpManager.LevelUp += GenerateSkills;
        Skill.Selected += ResetSkills;
    }

    public GameObject[] SelectedSkills { get { return this.selectedSkills; } }

    private void ResetSkills(object sender, EventArgs e) 
    {
        for (int i = 0; i < selectedSkills.Length; i++)
        {
            GameObject.Destroy(selectedSkills[i]);
            selectedSkills[i] = null;
        }
    }
    private void GenerateSkills(object sender, EventArgs e)
    {
        //Fisher-Yates shuffle
        Random random = new Random();
        for (int i = this.skillsLibrary.Length - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            GameObject temp = this.skillsLibrary[i];
            this.skillsLibrary[i] = this.skillsLibrary[j];
            this.skillsLibrary[j] = temp;
        }

        //int[] seen = new int[] { -1, -1, -1 };
        for (int i = 0; i < this.selectedSkills.Length; i++)
        {
            // Select without replacement
            /*
            int j = random.Next(0, this.skillsLibrary.Length - 1);
            int start = i;
            while (start > -1)
            {
                if (j != seen[start])
                {
                    start--;
                }
                else
                {
                    start = i;
                    j = random.Next(0, this.skillsLibrary.Length - 1);
                }
            }
            seen[i] = j;
            */
            GameObject skillObject = Instantiate(this.skillsLibrary[i]);
            this.selectedSkills[i] = skillObject;
        }
        OnSkillGenerated(EventArgs.Empty);
    }

    private void OnSkillGenerated(EventArgs e)
    {
        SkillsGenerated?.Invoke(this, e);
    }

}
