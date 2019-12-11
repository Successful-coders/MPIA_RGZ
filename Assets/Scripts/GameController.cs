using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private WhichSize whichSize;
    [SerializeField]
    private WhichFirst whichFirst;
    [SerializeField]
    private WhichGame whichGame;
    [SerializeField]
    private StartBtn startButton;
    [SerializeField]
    private FieldGenerator fieldGenerator;

    public void StartGame()
    {
        ClearScene();

        fieldGenerator.Generate(whichSize.FieldSize);
    }

    private void ClearScene()
    {
        whichSize.gameObject.SetActive(false);
        whichFirst.gameObject.SetActive(false);
        whichGame.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
    }
}

public enum FigureType
{
    CROSS,
    CIRCLE
}
