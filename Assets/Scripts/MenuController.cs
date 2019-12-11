using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGameButton ()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
