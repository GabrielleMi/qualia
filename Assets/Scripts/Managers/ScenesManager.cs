using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private const string TUTORIAL_SCENE_NAME = "Tutoriel";
    public static ScenesManager Instance;

    private string _currentScene = "Level1";
    private bool _tutorialIsComplete = false;

    public bool TutorialIsComplete
    {
        get { return _tutorialIsComplete; }
        set { _tutorialIsComplete = value; }
    }

    void Awake()
    {
        if (Instance == null)
        { 
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void ChangeScene(string sceneName)
    {
        if ((sceneName == TUTORIAL_SCENE_NAME && !_tutorialIsComplete) || sceneName != TUTORIAL_SCENE_NAME)
        {
            SceneManager.LoadScene(sceneName);
        }
        else if (sceneName == TUTORIAL_SCENE_NAME && _tutorialIsComplete)
        {
            SceneManager.LoadScene(_currentScene);
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
