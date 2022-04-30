using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DeathsUpdate : MonoBehaviour
{
    private int _deathNb = 0;
    private Text _textComp;

    void Start()
    {
        _textComp = GetComponent<Text>();
        GameManager.Instance.UpdateDeaths.AddListener(ChangeDeathNb);
    }

    void ChangeDeathNb()
    {
        _deathNb++;
        _textComp.text = _deathNb.ToString();
    }
}
