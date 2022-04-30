using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    #region Properties

    [SerializeField] private float _elevatorSpeed = 2.0f;
    [SerializeField] private Switch _switch;
    [SerializeField] private Transform _wpDown;
    [SerializeField] private Transform _wpUp;

    private AudioSource _audio;
    private bool _isMoving = false;
    private bool _isUp = true;

    #endregion Properties

    #region Private methods

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        transform.position = new Vector3(transform.position.x, _isUp ? _wpUp.position.y : _wpDown.position.y, transform.position.z);
    }

    IEnumerator MoveLift()
    {
        
        float targetY = _isUp ? _wpDown.position.y : _wpUp.position.y;

        _audio.Play();

        while (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), _elevatorSpeed * Time.deltaTime);

            if (transform.position.y == targetY)
            {
                _isMoving = false;
                _isUp = !_isUp;
                _switch.CanPress = true;
            }

            yield return null;
        }

        _audio.Stop();
    }

    #endregion Private methods

    #region Public methods

    public void LiftRequest()
    {
        if (!_isMoving)
        {
            _isMoving = true;
            StartCoroutine(MoveLift());
        }
    }

    #endregion Public methods
}
