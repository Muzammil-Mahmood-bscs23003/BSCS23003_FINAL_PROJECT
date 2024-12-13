using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class frommain : MonoBehaviour
{
    public void LoadSampleScene()
    {
        Collectible.score = 0;
        Collectible.islastdiamond = false;
        Collectible.totalGemCount = 0;
        GameController.isLast = false;

        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMainScreen()
    {
        Collectible.score = 0;
        Collectible.islastdiamond = false;
        Collectible.totalGemCount = 0;
        GameController.isLast = false;

        SceneManager.LoadScene("MainScreen");
    }
}
