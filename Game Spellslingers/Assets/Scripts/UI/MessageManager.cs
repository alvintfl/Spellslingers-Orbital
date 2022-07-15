using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    private String prev;
    private bool isPrinting;
    private void Start()
    {
        Item.PickUp += Print;
        Barrier.Collide += Print;
        SummoningCircle.Summon += Print;
        PauseMenu.instance.PauseEvent += Print;
        PauseMenu.instance.ResumeEvent += EndPrint;
        this.prev = "Controls: wasd for movement, mouse to aim\nShortcuts: c for character sheet, esc to pause";
        this.isPrinting = true;
        Invoke("EndPrint", 10f);
    }

    private void OnDestroy()
    {
        Item.PickUp -= Print;
        Barrier.Collide -= Print;
        SummoningCircle.Summon -= Print;
        PauseMenu.instance.PauseEvent -= Print;
        PauseMenu.instance.ResumeEvent -= EndPrint;
    }

    private void Print(Item item, EventArgs e)
    {
        CancelInvoke();
        String message = item.ToString();
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = message;
        this.prev = message;
        this.isPrinting = true;
        PlaySound();
        Invoke("EndPrint", 20f);
    }
    private void Print(Barrier barrier, EventArgs e)
    {
        CancelInvoke();
        String message = barrier.ToString();
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = message;
        this.prev = message;
        this.isPrinting = true;
        PlaySound();
        Invoke("EndPrint", 10f);
    }

    private void Print(SummoningCircle summon, EventArgs e)
    {
        CancelInvoke();
        String message = summon.ToString();
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = message;
        this.prev = message;
        this.isPrinting = true;
        PlaySound();
        Invoke("EndPrint", 10f);
    }

    private void Print(PauseMenu sender, EventArgs e)
    {
        CancelInvoke();
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "PAUSED";
        PlaySound();
    }

    private void PlaySound()
    {
        AudioManager.instance.Play("UI_buttonclick");
    }

    private void EndPrint()
    {
        this.isPrinting = false;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }

    private void EndPrint(PauseMenu sender, EventArgs e)
    {
        if (this.isPrinting)
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = this.prev;
            Invoke("EndPrint", 10f);
        } else
        {
            EndPrint();
        }
    }
}
