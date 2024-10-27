﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TechManager : MonoBehaviour, IDragHandler
{
    [Header("Main Stuff")]
    public static TechManager techTree;
    public TechStack[] stacks;
    public Dictionary<int, TechStack> techStacks = new Dictionary<int, TechStack>();
    public List<int> unlockedItems = new List<int>();
    public List<int> fullyUnlockedItems = new List<int>();

    [Header("Full Color")]

    public ColorBlock fullColor;

    [Header("Full Color")]
    public ColorBlock notFullColor;
    public bool deployed = false;

    public void Awake()
    {
        techTree = this;
        deployed = false;
        

        foreach (TechStack techStack in stacks)
        {
            techStacks.Add(techStack.tile, techStack);
            techStack.Start1();
        }
        //gameObject.SetActive(false);
    }

    public void Start()
    {
        if (DataSaver.CheckIfFileExists(Application.persistentDataPath + @"/worlds/" + GameManager.gameManagerReference.worldRootName + @"/tech.lgrsd"))
        {
            int[] items = ManagingFunctions.ConvertStringToIntArray(DataSaver.LoadStats(Application.persistentDataPath + @"/worlds/" + GameManager.gameManagerReference.worldRootName + @"/tech.lgrsd").SavedData);
            StartUnlocks(new List<int>(items));
        }
    }

    public void StartUnlocks(List<int> unlocks)
    {
        foreach (int unlock in unlocks)
        {
            UnlockBlock(unlock, true);
        }

    }

    public void UnlockBlock(int block, bool fully)
    {
        if (techStacks.TryGetValue(block, out TechStack stack))
        {
            stack.Unlock();
            if (fully)
                stack.UnlockFully();
        }
        else
        {
            unlockedItems.Add(block);
            fullyUnlockedItems.Add(block);
        }
       
    }

    public void Update()
    {
        //if (GInput.GetKeyDown(KeyCode.Tab) && (deployed || (!deployed && GameManager.gameManagerReference.InGame)))
        //{
        //    deployed = !deployed;
        //    GameManager.gameManagerReference.InGame = !deployed;
        //}
            

        GetComponent<Image>().enabled = deployed;
        GetComponent<Image>().raycastTarget = deployed;

        transform.parent.GetComponent<Image>().enabled = deployed;
        transform.parent.GetComponent<Image>().raycastTarget = deployed;

        if (deployed)
        {
            transform.parent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
             transform.localScale = Vector3.one * Mathf.Clamp(transform.localScale.x + (Input.mouseScrollDelta.y * 0.1f), 0.5f, 1.5f);
        }
        else
            transform.parent.GetComponent<RectTransform>().anchoredPosition = Vector2.left * 100000;
    }

    public void OnDrag(PointerEventData data)
    {
        GetComponent<RectTransform>().anchoredPosition += data.delta / MenuController.menuController.canvas.scaleFactor;
    }
}
