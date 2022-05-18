using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ExpManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private PauseMenu pauseMenu;
    private GameObject[] skillsLibrary;
    private GameObject[] selectedSkills;

    private int maxExp = 1;
    private int exp = 0;

    void Start()
    {
        this.selectedSkills = new GameObject[3];
        this.slider.value = this.exp;    
        this.skillsLibrary = Resources.LoadAll<GameObject>("Skills/");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddExp(1);
        }

        if (this.exp >= this.maxExp)
        {
            this.pauseMenu.Pause();
            LevelUp();
        }

        for (int i = 0; i < this.selectedSkills.Length; i++)
        {
            if (selectedSkills[i] != null 
                && selectedSkills[i].GetComponent<Skill>().IsSelected)
            {
                    this.pauseMenu.Resume();
                    ResetSkills();
            }
        }
    }

    private void ResetSkills()
    {
        for (int i = 0; i < selectedSkills.Length; i++)
        {
            GameObject.Destroy(selectedSkills[i]);
            selectedSkills[i] = null;
        }
    }

    public void LevelUp()
    {
        this.exp -= this.maxExp;
        this.maxExp += 1;
        this.slider.value = this.exp;
        this.slider.maxValue = this.maxExp;
        GenerateSkills();
    }

    public void AddExp(int exp)
    {
        this.exp += exp;
        this.slider.value += exp;
    }
    private void GenerateSkills()
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
            GameObject skillObject = Instantiate(this.skillsLibrary[i], pauseMenu.transform.GetChild(0).gameObject.transform);
            int yCoordinate = i == 0 ? 69 : i == 1 ? -64 : -197;
            skillObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(33, yCoordinate);
            this.selectedSkills[i] = skillObject;
        }
    }
}
