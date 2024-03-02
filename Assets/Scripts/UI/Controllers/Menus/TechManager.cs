﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TechManager : MonoBehaviour, IDragHandler
{
    [Header("Main Stuff")]
    public static TechManager techTree;
    public List<int> unlockedItems = new List<int>();

    [Header("Full Color")]

    public ColorBlock fullColor;

    [Header("Full Color")]
    public ColorBlock notFullColor;
    public bool deployed = false;

    public void Start()
    {
        techTree = this;
    }

    public void StartUnlocks(List<int> unlocks)
    {
        foreach (int unlock in unlocks)
            UnlockBlock(unlock);
    }
    
    public void UnlockBlock(int block)
    {
        TechStack[] techStacks = transform.GetComponentsInChildren<TechStack>(true);
        foreach (TechStack techStack in techStacks) techStack.UnlockedItem(block);
    }

    public void Update()
    {
        if (GInput.GetKeyDown(KeyCode.Tab))
            deployed = !deployed;

        GetComponent<Image>().enabled = deployed;
        GetComponent<Image>().raycastTarget = deployed;

        transform.parent.GetComponent<Image>().enabled = deployed;
        transform.parent.GetComponent<Image>().raycastTarget = deployed;

        if (deployed)
            transform.parent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        else
            transform.parent.GetComponent<RectTransform>().anchoredPosition = Vector2.left * 100000;
    }

    public void OnDrag(PointerEventData data)
    {
        GetComponent<RectTransform>().anchoredPosition += data.delta / MenuController.menuController.canvas.scaleFactor;
    }
}