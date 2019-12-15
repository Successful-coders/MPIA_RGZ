using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFigure : MonoBehaviour
{
    [SerializeField]
    private WhichSize whichSize;
    [SerializeField]
    private FieldGenerator fieldGenerator;
    [SerializeField]
    private Transform figureStack;
    [SerializeField]
    private GameObject cross, circle;

    private void SetFigureSize()
    {
        float padding = fieldGenerator.Offset * 0.2f;
        cross.GetComponent<RectTransform>().sizeDelta = new Vector2(fieldGenerator.Offset - padding, fieldGenerator.Offset - padding);
        circle.GetComponent<RectTransform>().sizeDelta = cross.GetComponent<RectTransform>().sizeDelta;
    }

    public Vector2 ClickPostionToIndexes(Vector2 clickPosition)
    {
        int xPos, yPos;

        if (whichSize.FieldSize % 2 == 1)
        {
            if (clickPosition.x > 0)
            {
                xPos = Mathf.FloorToInt((clickPosition.x + (fieldGenerator.Offset / 2)) / fieldGenerator.Offset);
            }
            else
            {
                xPos = Mathf.CeilToInt((clickPosition.x - (fieldGenerator.Offset / 2)) / fieldGenerator.Offset);
            }

            if (clickPosition.y > 0)
            {
                yPos = Mathf.FloorToInt((clickPosition.y + (fieldGenerator.Offset / 2)) / fieldGenerator.Offset);
            }
            else
            {
                yPos = Mathf.CeilToInt((clickPosition.y - (fieldGenerator.Offset / 2)) / fieldGenerator.Offset);
            }
        }
        else
        {
            if (clickPosition.x > 0)
            {
                xPos = Mathf.FloorToInt(clickPosition.x / fieldGenerator.Offset);
            }
            else
            {
                xPos = Mathf.CeilToInt(clickPosition.x / fieldGenerator.Offset) - 1;
            }

            if (clickPosition.y > 0)
            {
                yPos = Mathf.FloorToInt(clickPosition.y / fieldGenerator.Offset);
            }
            else
            {
                yPos = Mathf.CeilToInt(clickPosition.y / fieldGenerator.Offset) - 1;
            }
        }

        xPos += whichSize.FieldSize / 2;
        yPos += whichSize.FieldSize / 2;

        if (xPos >= 0 && xPos < whichSize.FieldSize && yPos >= 0 && yPos < whichSize.FieldSize)
        {
            return new Vector2(xPos, yPos);
        }
        else
        {
            throw new System.Exception("OutOfBoundsFieldClick");
        }
    }

    public void Clear()
    {
        for (int i = 0; i < figureStack.childCount; i++)
        {
            GameObject.Destroy(figureStack.GetChild(i).gameObject);
        }
    }

    public void Put(FigureType figureType, int xPos, int yPos)
    {
        SetFigureSize();

        Vector3 startPoint = new Vector3((-fieldGenerator.WorkScreenHeight / 2) + (fieldGenerator.Offset / 2),
            (-fieldGenerator.WorkScreenHeight / 2) + (fieldGenerator.Offset / 2), 0);
        Vector3 position = new Vector3(startPoint.x + xPos * fieldGenerator.Offset,
            startPoint.y + yPos * fieldGenerator.Offset, 0);

        GameObject newFigure = Instantiate(figureType == FigureType.CROSS ? cross : circle, figureStack);
        newFigure.transform.localPosition = position;
    } 

    public void Put(FigureType figureType, Vector2 indexes)
    {
        Put(figureType, (int)indexes.x, (int)indexes.y);
    }
}
