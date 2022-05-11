using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PressedEvent : UnityEvent<bool> { }

public class PressurePlate : MonoBehaviour
{

    #region Properties
    
    public PressedEvent pressedEvent;

    [SerializeField] private List<Renderer> _colorsToChange;
    [SerializeField] private Transform _plate;

    private float _currentWeight = 0.0f;
    private float _initPlateY;
    private bool _isPressed = false;
    private float _requiredWeight = 5.0f;

    #endregion Properties

    #region Private methods

    void Start()
    {
        _initPlateY = _plate.position.y;
    }

    void OnCollisionEnter(Collision collision)
    {
        bool isPerso = collision.gameObject.CompareTag(PersoMainTag.persoTag);

        if (isPerso || collision.gameObject.CompareTag("Drone"))
        {
            _currentWeight += collision.gameObject.GetComponent<Rigidbody>().mass;

            CheckWeight();

        }
    }

    void OnCollisionExit(Collision collision)
    {
        bool isPerso = collision.gameObject.CompareTag(PersoMainTag.persoTag);

        if (isPerso || collision.gameObject.CompareTag("Drone"))
        {
            _currentWeight -= collision.gameObject.GetComponent<Rigidbody>().mass;

            CheckWeight();
        }
    }

    void CheckWeight()
    {
        if (_currentWeight >= _requiredWeight && !_isPressed)
        {
            _isPressed = true;
            Press();
        }
        else if (_currentWeight < _requiredWeight)
        {
            _isPressed = false;
            Press();
        }
    }

    void Press()
    {
        pressedEvent.Invoke(_isPressed);
        Vector3 platePos = new Vector3(_plate.position.x, _isPressed ? _initPlateY - 0.08f : _initPlateY, _plate.position.z);
        _plate.position = platePos;
    }

    #endregion Private methods

    #region Public methods

    public void SetColours(Color color)
    {
        foreach (Renderer renderer in _colorsToChange)
        {
            renderer.material.color = color;
            renderer.material.SetColor("_EmissionColor", color);
        }
    }

    #endregion Public methods

}
