using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhichGame : MonoBehaviour
{
    [SerializeField]
    private Button buttonCross, buttonCircle;
    private FigureType yourFigure = FigureType.CROSS;
    private FigureType enemyFigure = FigureType.CIRCLE;

    private void Start()
    {
        buttonCross.onClick.Invoke();
    }

    public void OnPressCross()
    {
        buttonCross.interactable = false;
        buttonCircle.interactable = true;

        yourFigure = FigureType.CROSS;
        enemyFigure = FigureType.CIRCLE;
    }

    public void OnPressCircle()
    {
        buttonCircle.interactable = false;
        buttonCross.interactable = true;

        yourFigure = FigureType.CIRCLE;
        enemyFigure = FigureType.CROSS;
    }

    public FigureType YourFigure
    {
        get
        {
            return yourFigure;
        }
    }
    public FigureType EnemyFigure
    {
        get
        {
            return enemyFigure;
        }
    }
    public FigureType TIE
    {
        get
        {
            return TIE;
        }
    }
}
