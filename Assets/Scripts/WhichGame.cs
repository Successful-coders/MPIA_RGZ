using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhichGame : MonoBehaviour
{
    [SerializeField]
    private Button buttonCross, buttonCircle;
    private FigureType figure = FigureType.CROSS;

    private void Start()
    {
        buttonCross.onClick.Invoke();
    }

    public void OnPressCross()
    {
        buttonCross.interactable = false;
        buttonCircle.interactable = true;

        figure = FigureType.CROSS;
    }

    public void OnPressCircle()
    {
        buttonCircle.interactable = false;
        buttonCross.interactable = true;

        figure = FigureType.CIRCLE;
    }

    public FigureType SelectedFigure
    {
        get
        {
            return figure;
        }
    }
}
