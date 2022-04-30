using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    #region Properties

    public UnityEvent OnTrigger;

    [SerializeField] private Material _canPressMaterial;
    [SerializeField] private Material _cannotPressMaterial;
    [SerializeField] private List<MeshRenderer> _buttons;

    private AudioSource _audio;
    private bool _canPress = true;
    private bool _checkInteraction = false;
    
    #endregion Properties

    public bool CanPress
    {   set {
            _canPress = value;
            ChangeButtons();
        }
        get { return _canPress; }
    }

    #region Private methods
    
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (_checkInteraction)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            {
                OnTrigger.Invoke();

                if(_canPress)
                {
                    _audio.Play();
                }

                _canPress = false;
                ChangeButtons();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PersoMainTag.persoTag) _checkInteraction = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == PersoMainTag.persoTag) _checkInteraction = false;
    }

    void ChangeButtons()
    {
        bool canPressMaterial = _canPress? _canPressMaterial : _cannotPressMaterial;

        foreach (MeshRenderer button in _buttons)
        {
            button.material = canPressMaterial;
        }
    }

    #endregion Private methods

}
