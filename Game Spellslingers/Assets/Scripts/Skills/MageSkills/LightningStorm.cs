using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightningStorm : Skill
{
    public LightningStorm() : base(10) { }
    
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            Mage mage = (Mage) Player.instance;
            if (Level == 1)
            {
                mage.CastLightningStorm();
                GameObject skillDescription = gameObject.transform.GetChild(1).gameObject;
                skillDescription.GetComponent<TextMeshProUGUI>().text =
                    "+5% Lightning field range.\n+1s Lightning field duration.";
            } else if (Level <= 5)
            {
                mage.IncreaseLightningStormRange();
                mage.IncreaseLightningStormDuration();
                if (Level == 5)
                {
                    GameObject skillDescription = gameObject.transform.GetChild(1).gameObject;
                    skillDescription.GetComponent<TextMeshProUGUI>().text =
                        "+5% Lightning Field range\n+1 Lightning Field damage.";
                }
            } else 
            {
                mage.IncreaseLightningStormRange();
                mage.IncreaseLightningStormDamage();
            }
            OnSelected(EventArgs.Empty);
        });
    }

    public override void Reset() 
    {
        GameObject skillDescription = gameObject.transform.GetChild(1).gameObject;
        skillDescription.GetComponent<TextMeshProUGUI>().text =
            "Every 10 seconds, lightning stikes the ground, creating a lightning field that damages enemies.";
    }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
