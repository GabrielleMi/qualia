using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Properties

    private const string SAVE_PARAM_GAME = "game";

    public static SaveManager instance;

    private bool _canLoad = false;
    private bool _hasSave;

    #endregion Properties

    #region Getters & Setters

    public bool CanLoad
    {
        get { return _canLoad; }
        set { _canLoad = value; }
    }

    public bool HasSave
    {
        get { return _hasSave; }
    }

    #endregion Getters & Setters

    #region Private methods

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        DontDestroyOnLoad(this);

        _hasSave = PlayerPrefs.GetString(SAVE_PARAM_GAME) != "";
    }

    #endregion Private methods

}
