using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private PauseMenu pauseMenu;
    private GameObject[] skillsLibrary;
    private GameObject[] selectedSkills;
    private bool IsSkillSelected = false;
    
    private int maxExp = 1;
    private int exp = 0;

    void Start()
    {
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
            this.selectedSkills = LevelUp();
        }    

        if (IsSkillSelected)
        {
            IsSkillSelected = false;
            foreach (GameObject skill in selectedSkills)
            {
                GameObject.Destroy(skill);
            }
        }
    }

    public void Delete()
    {
        this.pauseMenu.Resume();
        this.IsSkillSelected = true;
    }

    public GameObject[] LevelUp()
    {
        this.exp -= this.maxExp;
        this.maxExp += 1;
        this.slider.value = this.exp;
        this.slider.maxValue = this.maxExp;
        return GenerateSkills();
    }

    public void AddExp(int exp)
    {
        this.exp += exp;
        this.slider.value += exp;
    }
    private GameObject[] GenerateSkills()
    {
        //TODO
        //Randomly select skills
        GameObject[] selectedSkills = new GameObject[3];
        selectedSkills[0] = this.skillsLibrary[0];
        selectedSkills[1] = this.skillsLibrary[1];
        selectedSkills[2] = this.skillsLibrary[2];

        GameObject[] selectedSkillsPrefab = new GameObject[3];
        for (int i = 0; i < selectedSkills.Length; i++)
        {
            GameObject skillObject = Instantiate(selectedSkills[i], pauseMenu.transform.GetChild(0).gameObject.transform);
            int yCoordinate = i == 0 ? 69 : i == 1 ? -64 : -197;
            skillObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(33, yCoordinate);
            selectedSkillsPrefab[i] = skillObject;
        }
        return selectedSkillsPrefab;
    }
}
