using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField]
    private WhichSize whichSize;
    [SerializeField]
    private GameController gameController;
    [SerializeField]

    //[SerializeField]
    //private WhichGame whichGame;
  

    public Vector2 NextTurn()
    {
        //исправить на 5 чувачечков
        Vector2 enemyIndexes;
        enemyIndexes.x = 0;
        enemyIndexes.y = 0;
        int bestScore = -9999;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                // Is the spot available?
                if (gameController.Field[i, j] == -1)
                {
                    gameController.Field[i, j] = (int) gameController.whichGame.EnemyFigure;
                    int score = minimax(0, true);
                    gameController.Field[i, j] = -1;
                    if (score > bestScore)
                    {
                        bestScore = score;
                        enemyIndexes.x = i;
                        enemyIndexes.y = j;
                    }
                }
            }
        }
        return enemyIndexes;
    }

    int[] scores = new int[3] { 10, -10, 0 };
    private int minimax(int depth, bool isMaximizing)
    {
        int result;
        if (isMaximizing)
        {
            result = gameController.IsWinSituation(gameController.whichGame.EnemyFigure);// ERROR?
            if(result!=-1)
            {
                return scores[result];
            }
            
        }
        else
        {
            result = gameController.IsWinSituation(gameController.whichGame.YourFigure);
            if (result != -1)
            {
                return scores[result];
            }
        }
        //int result = gameController.checkWinner();
        //if (result != -1)
        //{
        //    return scores[result];
        //}
        
        if (isMaximizing)
        {
            int bestScore = -999;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    // Is the spot available?
                    if (gameController.Field[i, j] == -1)
                    {
                        gameController.Field[i, j] = (int)gameController.whichGame.EnemyFigure;
                        int score = minimax(depth + 1, false);
                        gameController.Field[i, j] = -1;
                        bestScore = Math.Max(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = 999;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    // Is the spot available?
                    if (gameController.Field[i, j] == -1)
                    {
                        gameController.Field[i, j] = (int)gameController.whichGame.YourFigure;
                        int score = minimax(depth + 1, true);
                        gameController.Field[i, j] = -1;
                        bestScore = Math.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }
}
