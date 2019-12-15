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
    private MenuBtn menuButton;
    [SerializeField]
    private FieldGenerator fieldGenerator;
    [SerializeField]
    private PlaceFigure placeFigure;
    [SerializeField]
    private Transform figureStack;
    [SerializeField]
    private Bot bot;

    private bool isGameStart;
    private int[,] field;

    public void StartGame()
    {
        isGameStart = true;
        SetMenuActive(false);

        fieldGenerator.Generate(whichSize.FieldSize);

        field = new int[whichSize.FieldSize, whichSize.FieldSize];
        CreateEmptyField();

        if (whichFirst.SelectedFigure == whichGame.EnemyFigure)
        {
            Vector2 enemyIndexes = bot.NextTurn();
            placeFigure.Put(whichGame.EnemyFigure, enemyIndexes);
            field[(int)enemyIndexes.x, (int)enemyIndexes.y] = (int)whichGame.EnemyFigure;
        }

        StartCoroutine(GameCoroutine());
    }

    public void ReturnToMenu()
    {
        isGameStart = false;
        SetMenuActive(true);

        fieldGenerator.Clear();
        placeFigure.Clear();

        StopAllCoroutines();
    }

    private void SetMenuActive(bool isActive)
    {
        whichSize.gameObject.SetActive(isActive);
        whichFirst.gameObject.SetActive(isActive);
        whichGame.gameObject.SetActive(isActive);
        startButton.gameObject.SetActive(isActive);
        menuButton.gameObject.SetActive(!isActive);
    }

    IEnumerator GameCoroutine()
    {
        while(isGameStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 clickPosition = figureStack.InverseTransformPoint(ray.GetPoint(0));

                Vector2 yourIndexes;
                Vector2 enemyIndexes;
                try
                {
                    yourIndexes = placeFigure.ClickPostionToIndexes(clickPosition);
                }
                catch
                {
                    break;
                }

                if (field[(int)yourIndexes.x, (int)yourIndexes.y] == -1)
                {
                    placeFigure.Put(whichGame.YourFigure, yourIndexes);
                    field[(int)yourIndexes.x, (int)yourIndexes.y] = (int)whichGame.YourFigure;
                    
                    if((FigureType)CheckWinSituation() == whichGame.EnemyFigure)
                    {
                        Debug.Log("Enemy win");
                    }
                    else if((FigureType)CheckWinSituation() == whichGame.YourFigure)
                    {
                        Debug.Log("You win");
                    }

                    if (!HasFieldPlace())
                    {
                        isGameStart = false;
                        break;
                    }

                    enemyIndexes = bot.NextTurn();
                    placeFigure.Put(whichGame.EnemyFigure, enemyIndexes);
                    field[(int)enemyIndexes.x, (int)enemyIndexes.y] = (int)whichGame.EnemyFigure;

                    if ((FigureType)CheckWinSituation() == whichGame.EnemyFigure)
                    {
                        Debug.Log("Enemy win");
                    }
                    else if ((FigureType)CheckWinSituation() == whichGame.YourFigure)
                    {
                        Debug.Log("You win");
                    }

                    if (!HasFieldPlace())
                    {
                        isGameStart = false;
                        break;
                    }
                }
                else
                {
                    if (!HasFieldPlace())
                    {
                        isGameStart = false;
                        break;
                    }
                }
            }

            yield return null;
        }

        Debug.Log("Game over");
    }

    private bool HasFieldPlace()
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if(field[i, j] == -1)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void CreateEmptyField()
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                field[i, j] = -1;
            }
        }
    }

    private int CheckWinSituation()
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                //TODO
            }
        }

        return -1;
    }

    public int[,] Field
    {
        get
        {
            return field;
        }
        set
        {
            field = value;
        }
    }
}

public enum FigureType
{
    CROSS,
    CIRCLE
}
