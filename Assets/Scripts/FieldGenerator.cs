﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject line;

    private int padding = 60;
    private float offset;
    private int workScreenHeight;

    private void Start()
    {
        SetLineSize();

        workScreenHeight = Screen.height - padding;
    }

    public void Generate(int fieldCount)
    {
        offset = workScreenHeight / fieldCount;

        Vector3 position = new Vector3(0, -workScreenHeight / 2 + offset, 0);
        Vector3 rotation = new Vector3(0, 0, 90);

        for (int i = 0; i < fieldCount - 1; i++)
        {
            GameObject newLine = Instantiate(line, transform);
            newLine.SetActive(true);

            newLine.transform.localPosition = position;
            newLine.transform.localEulerAngles = rotation;
            position += new Vector3(0, offset, 0);
        }

        position = new Vector3(-offset * ((fieldCount - 1) / 2), 0, 0);
        if (fieldCount % 2 == 1)
        {
            position += new Vector3(offset / 2, 0, 0);
        }

        rotation = new Vector3(0, 0, 0);

        for (int i = 0; i < fieldCount - 1; i++)
        {
            GameObject newLine = Instantiate(line, transform);
            newLine.SetActive(true);

            newLine.transform.localPosition = position;
            newLine.transform.localEulerAngles = rotation;
            position += new Vector3(offset, 0, 0);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void SetLineSize()
    {
        line.GetComponent<RectTransform>().sizeDelta = new Vector2(10, Screen.height - (padding * 2));
    }

    public float Offset
    {
        get
        {
            return offset;
        }
    }
    public int WorkScreenHeight
    {
        get
        {
            return workScreenHeight;
        }
    }
}
