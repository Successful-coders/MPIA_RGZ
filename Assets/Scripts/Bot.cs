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
        //исправил на 5 чувачечков :D
        Vector2 enemyIndexes;
        enemyIndexes.x = 0;
        enemyIndexes.y = 0;
        int bestScore = int.MinValue;
        for (int i = 0; i < whichSize.FieldSize; i++)
        {
            for (int j = 0; j < whichSize.FieldSize; j++)
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

    private int minimax(int depth, bool isMaximizing)
    {
        if (gameController.IsWinSituation(gameController.whichGame.EnemyFigure))
        {
            return +10;
        }
        else if(gameController.IsWinSituation(gameController.whichGame.YourFigure))
        {
            return -10;
        }

        if (depth == 3) return -1;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < whichSize.FieldSize; i++)
            {
                for (int j = 0; j < whichSize.FieldSize; j++)
                {
                    // Is the spot available?
                    if (gameController.Field[i, j] == -1)
                    {
                        gameController.Field[i, j] = (int)gameController.whichGame.EnemyFigure;
                        int score = minimax(depth + 1, false);
                        gameController.Field[i, j] = -1;
                        bestScore = Mathf.Max(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < whichSize.FieldSize; i++)
            {
                for (int j = 0; j < whichSize.FieldSize; j++)
                {
                    // Is the spot available?
                    if (gameController.Field[i, j] == -1)
                    {
                        gameController.Field[i, j] = (int)gameController.whichGame.YourFigure;
                        int score = minimax(depth + 1, true);
                        gameController.Field[i, j] = -1;
                        bestScore = Mathf.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }
}
