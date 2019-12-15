using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField]
    private WhichSize whichSize;
    [SerializeField]
    private GameController gameController;

    public Vector2 NextTurn()
    {
        Vector2 position;
        do
        {
            position = new Vector2(Random.Range(0, whichSize.FieldSize), Random.Range(0, whichSize.FieldSize));
        }
        while (gameController.Field[(int)position.x, (int)position.y] != -1);

        return position;
    }
}
