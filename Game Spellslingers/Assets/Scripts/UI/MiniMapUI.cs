using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapUI : MonoBehaviour
{
    private GameObject itemIcon;
    private GameObject bossIcon;
    private GameObject miniMap;
    private float westBorder, southBorder, eastBorder, northBorder;
    private Location curr;
    private Color dark;
    private Color bright;
    private int progressionCount;
    private int maxProgressionCount;

    private void Start()
    {
        this.miniMap = transform.GetChild(0).gameObject;
        this.westBorder = -210f;
        this.southBorder = -247.8f;
        this.eastBorder = 155.9f;
        this.northBorder = 33.23f;
        this.curr = Location.Centre;
        this.bright = new Color32(255, 255, 255, 200);
        this.dark = new Color32(144, 144, 144, 127);
        SetColor(this.curr, this.bright);
        this.progressionCount = 0;
        this.maxProgressionCount = 4;
        Item.PickUp += UpdateItemIcons;
        Item.Spawned += UpdateBossIcons;

        this.itemIcon = transform.GetChild(1).gameObject;
        this.itemIcon.GetComponent<Image>().enabled = true;
        this.itemIcon.SetActive(false);
        this.bossIcon = transform.GetChild(2).gameObject;
        this.bossIcon.GetComponent<Image>().enabled = true;
        this.bossIcon.SetActive(false);
        AddItemIcon(this.progressionCount);
    }

    private void OnDestroy()
    {
        Item.PickUp -= UpdateItemIcons;
        Item.Spawned -= UpdateBossIcons;
    }

    private enum Location
    {
        Centre, West, South, East, North
    }

    private void UpdateItemIcons(Item item, EventArgs eventArgs)
    {
        foreach(Transform child in GetLocationObject(this.progressionCount).transform)
        {
            child.gameObject.SetActive(false);
        }
        if (this.progressionCount != this.maxProgressionCount)
        {
            this.progressionCount++;
            AddBossIcon(this.progressionCount);
        }
    }

    private void UpdateBossIcons(Item item, EventArgs eventArgs)
    {
        foreach(Transform child in GetLocationObject(this.progressionCount).transform)
        {
            child.gameObject.SetActive(false);
        }
        if (this.progressionCount <= this.maxProgressionCount)
        {
            AddItemIcon(this.progressionCount);
        }
    }

    private GameObject GetLocationObject(Location location)
    {
        return this.miniMap.transform.GetChild((int) location).gameObject;
    }
    private GameObject GetLocationObject(int location)
    {
        return this.miniMap.transform.GetChild(location).gameObject;
    }

    private void SetColor(Location location, Color color)
    {
        GetLocationObject(location).GetComponent<Image>().color = color;
    }

    private void Update()
    {
        UpdatePlayerPosition();
    }

    private void UpdatePlayerPosition()
    {
        Vector3 playerPosition = Player.instance.transform.position;
        SetColor(this.curr, this.dark);
        if (playerPosition.x < this.westBorder)
        {
            SetColor(Location.West, this.bright);
            this.curr = Location.West;
        }
        else if (playerPosition.x > this.eastBorder)
        {
            SetColor(Location.East, this.bright);
            this.curr = Location.East;
        }
         else if (playerPosition.y < this.southBorder)
        {
            SetColor(Location.South, this.bright);
            this.curr = Location.South;
        } else if (playerPosition.y > this.northBorder)
        {
            SetColor(Location.North, this.bright);
            this.curr = Location.North;
        } else
        {
            SetColor(Location.Centre, this.bright);
            this.curr = Location.Centre;
        }
    }

    private void AddItemIcon(int location)
    {
        Transform parent = GetLocationObject(location).transform;
        this.itemIcon.transform.SetParent(parent);
        this.itemIcon.transform.position = parent.position;
        this.itemIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
        this.itemIcon.SetActive(true);
    }
    private void AddBossIcon(int location)
    {
        Transform parent = GetLocationObject(location).transform;
        this.bossIcon.transform.SetParent(parent);
        this.bossIcon.transform.position = parent.position;
        this.bossIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
        this.bossIcon.SetActive(true);
    }
}
