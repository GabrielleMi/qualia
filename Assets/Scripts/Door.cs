using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    #region Properties

    [SerializeField] private GameObject _door;
    [SerializeField] private Transform _wpClosed;
    [SerializeField] private Transform _wpOpen;

    private AudioSource _audio;
    private bool _isMoving = false;
    private bool _isOpen = false;
    private float _speed = 0.1f;

    #endregion Properties

    #region Private methods

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _door.transform.position = new Vector3(_door.transform.position.x, _wpClosed.position.y, _door.transform.position.z);
    }

    IEnumerator MoveDoor()
    {
        _audio.Play();

        float positionY = _isOpen ? _wpClosed.position.y : _wpOpen.position.y;

        while (_door.transform.position.y != positionY && _isMoving)
        {
            Vector3 target = new Vector3(_door.transform.position.x, positionY, _door.transform.position.z);
            _door.transform.position = Vector3.MoveTowards(_door.transform.position, target, _speed);
            yield return null;
        }

        _isOpen = !_isOpen;
        _isMoving = false;
        StartCoroutine(AudioFadeOut.FadeOut(_audio, 0.1f));
    }

    #endregion Private methods

    #region Public methods

    public void OpenDoor(bool open)
    {
        _isOpen = !open;
        _isMoving = true;
        StartCoroutine(MoveDoor());
    }

    #endregion Public methods

}
