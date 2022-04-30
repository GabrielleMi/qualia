using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string _nextScene;
    private Button yourButton;

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if (_nextScene != "Quit")
        {
            ScenesManager.Instance.ChangeScene(_nextScene);
        }
        else
        {
            ScenesManager.Instance.QuitGame();
        }
    }
}
