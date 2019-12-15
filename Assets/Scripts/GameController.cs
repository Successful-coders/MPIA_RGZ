using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private const int WIN_COMBINATION_SIZE = 5;

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
    [SerializeField]
    private WinPanel winPanel;

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

                yourIndexes = placeFigure.ClickPostionToIndexes(clickPosition);
                if (yourIndexes.x == -1 || yourIndexes.y == -1)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }

                if (field[(int)yourIndexes.x, (int)yourIndexes.y] == -1)
                {
                    placeFigure.Put(whichGame.YourFigure, yourIndexes);
                    field[(int)yourIndexes.x, (int)yourIndexes.y] = (int)whichGame.YourFigure;

                    if (IsWinSituation(whichGame.YourFigure))
                    {
                        winPanel.Appear("Вы выйграли!");

                        isGameStart = false;
                        break;
                    }

                    if (!HasFieldPlace())
                    {
                        winPanel.Appear("Ничья!");

                        isGameStart = false;
                        break;
                    }

                    enemyIndexes = bot.NextTurn();
                    placeFigure.Put(whichGame.EnemyFigure, enemyIndexes);
                    field[(int)enemyIndexes.x, (int)enemyIndexes.y] = (int)whichGame.EnemyFigure;

                    if (IsWinSituation(whichGame.EnemyFigure))
                    {
                        winPanel.Appear("Бот выйграл!");

                        isGameStart = false;
                        break;
                    }

                    if (!HasFieldPlace())
                    {
                        winPanel.Appear("Ничья!");

                        isGameStart = false;
                        break;
                    }
                }
                else
                {
                    if (!HasFieldPlace())
                    {
                        winPanel.Appear("Ничья!");

                        isGameStart = false;
                        break;
                    }
                }
            }

            yield return null;
        }
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

    private bool IsWinSituation(FigureType figureType)
    {
        //Проверяем горизонталь и вертикаль
        int mainDiag = 0, supDiag = 0, horizontal = 0, vertical = 0;

        for (int i = 0; i < whichSize.FieldSize; i++)
        {
            horizontal = 0;
            vertical = 0;
            for (int j = 0; j < whichSize.FieldSize; j++)
            {
                if (field[i, j] == (int)figureType)
                {
                    horizontal++;
                }
                else if (horizontal == WIN_COMBINATION_SIZE)
                {
                    return true;
                }
                else
                {
                    horizontal = 0;
                }

                if (field[j, i] == (int)figureType)
                {
                    vertical++;
                }
                else if (vertical == WIN_COMBINATION_SIZE)
                {
                    return true;
                }
                else
                {
                    vertical = 0;
                }
            }
            if (horizontal == WIN_COMBINATION_SIZE || vertical == WIN_COMBINATION_SIZE)
            {
                return true;
            }
        }

        //Диагонали
        for (int j = 0; j < whichSize.FieldSize - WIN_COMBINATION_SIZE + 1; j++)
        {
            for (int k = 0; k < whichSize.FieldSize - WIN_COMBINATION_SIZE + 1; k++)
            {
                int indexJ = j;
                int indexK = k;

                mainDiag = 0;
                supDiag = 0;
                int boardIndex = indexK + (WIN_COMBINATION_SIZE - 1);
                for (int i = 0; i < WIN_COMBINATION_SIZE; i++, indexJ++, indexK++)
                {
                    if (field[(whichSize.FieldSize - 1) - indexJ, indexK] == (int)figureType)
                    {
                        mainDiag++;
                    }

                    if (field[(whichSize.FieldSize - 1) - indexJ, boardIndex - indexK] == (int)figureType)
                    {
                        supDiag++;
                    }
                }
                if (mainDiag == WIN_COMBINATION_SIZE || supDiag == WIN_COMBINATION_SIZE)
                {
                    return true;
                }
            }
        }

        return false;
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
