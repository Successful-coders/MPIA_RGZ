using UnityEngine;

public class StartBtn : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    public void OnPressStart()
    {
        gameController.StartGame();
    }
}
