using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrbsOfPower : Skill
{
    public OrbsOfPower() : base(10) { }
    
    public override void Start()
    {
        base.Start();
        Button.onClick.AddListener(() =>
        {
            Mage mage = (Mage) Player.instance;
            if (Level <= 4)
            {
                mage.CastLightningOrb();
                if (Level == 4)
                {
                    GameObject skillDescription = gameObject.transform.GetChild(1).gameObject;
                    skillDescription.GetComponent<TextMeshProUGUI>().text =
                        "+2 damage to all lightning orbs.\n+10% rotation speed.";
                }
            } else
            {
                LightningOrb.IncreaseRotationSpeed();
                mage.IncreaseLightningOrbDamage();
            }
            OnSelected(EventArgs.Empty);
        });
    }

    public override void Reset() 
    {
        LightningOrb.Reset();
        GameObject skillDescription = gameObject.transform.GetChild(1).gameObject;
        skillDescription.GetComponent<TextMeshProUGUI>().text =
            "+1 lightning orb that rotates around you.";
    }

    public override bool IsSignatureSkill()
    {
        return false;
    }
}
