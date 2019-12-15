using UnityEngine;

public class MenuBtn : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    public void OnPressMenu()
    {
        gameController.ReturnToMenu();
    }
}
