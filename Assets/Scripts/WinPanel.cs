using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField]
    private Text winText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Appear(string whoWin)
    {
        winText.text = whoWin;
        gameObject.SetActive(true);
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
    }
}
