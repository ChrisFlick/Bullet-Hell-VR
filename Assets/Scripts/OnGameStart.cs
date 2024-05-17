using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnGameStart : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.SetInt("score", 0); // 0 out the score at the start of the app

        int mainMenuIndex = 1;

        SceneManager.LoadScene(mainMenuIndex, LoadSceneMode.Single);
    }

}
