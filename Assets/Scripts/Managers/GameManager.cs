using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using Kino;

[System.Serializable]
public class GameInfo
{
    //public FloorInfo[] floors;

   //public ConstructionInfo(List<GameObject> myFloors)
    //{
        //floors = new FloorInfo[myFloors.Count];
        //for (int i = 0; i < myFloors.Count; i++)
        //{
            //floors[i] = myFloors[i].GetComponent<Floor>().GetInfo();
        //}
    //}

}

[System.Serializable]
public class GamePaused : UnityEvent<bool>{}

public class GameManager : MonoBehaviour
{
    #region Properties

    private static GameManager _instance = null;

    public UnityEvent UpdateDeaths;
    public GamePaused _pauseEvent;

    [SerializeField] private GameObject _body;

    private GameObject _clone = null;
    private bool _cloneSpawned = false;
    private enum GameStates { Menu, Pause, Jeu }
    private GameStates _currentGameState = GameStates.Menu;

    #endregion Properties

    #region Getters & Setters

    public bool CloneSpawned
    {
        get { return _cloneSpawned; }
    }

    #endregion Getters & Setters

    #region Instance
    public static GameManager Instance { get { return _instance; } }

    #endregion Instance
    
    #region Private Methods

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    void Update()
    {
        if (!(_currentGameState == GameStates.Menu) && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (_currentGameState == GameStates.Jeu)
        {
            _currentGameState = GameStates.Pause;
            _pauseEvent.Invoke(true);
        }
        else
        {
            _currentGameState = GameStates.Jeu;
            _pauseEvent.Invoke(false);
        }
    }

    #endregion Private Methods

    #region Public Methods

    public void SpawnNewClone(Transform spawnPoint)
    {
        NewDeath();
        
        if (_clone == null)
        {
            GameObject newBody = Instantiate(_body, spawnPoint.position, Quaternion.Euler(spawnPoint.rotation.eulerAngles.x, spawnPoint.rotation.eulerAngles.y, 0.0f));
            _clone = newBody;
            _cloneSpawned = true;
        }
        else
        {
            Destroy(_clone);
            _clone = null;
            _cloneSpawned = false;
        }
        
        
    }

    public void NewDeath()
    {
        UpdateDeaths.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion Public Methods

}
