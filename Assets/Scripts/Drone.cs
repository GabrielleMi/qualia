using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    #region Properties

    [SerializeField] private List<Spin> _helices;
    [SerializeField] private bool _isStill = true;
    [SerializeField] private GameObject _light;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private Transform _wp1;
    [SerializeField] private Transform _wp2;

    private Vector3 _currentPos;
    private bool _isFollowing = false;
    private bool _isShooting = false;
    private LayerMask _layerMask = ((1 << 9) | (1 << 10) | (1 << 11));
    private float _maxDistance = 10.0f;
    private GameObject _player;
    private Vector3 _rayDirection;
    private Vector3 _targetPos;

    private enum Status { Patrolling, Checking, Searching, Attacking, Deactivated }
    private Status _currentStatus;

    #endregion Properties

    #region Private methods

    void Start()
    {
        _currentStatus = Status.Patrolling;
        _currentPos = transform.position;
        
        if (!_isStill)
        {
            StartCoroutine(Patrol());
        }
    }

    void FixedUpdate()
    {
        if (_currentStatus == Status.Checking)
        {
            RaycastCheck();
        }
        else if (_currentStatus == Status.Attacking)
        {
            if (!_isShooting)
            {
                StartCoroutine(Shoot());
            }

            LookAtTarget();
            RaycastCheck();
        }
    }

    void RandomPos()
    {
        _targetPos = new Vector3(UnityEngine.Random.Range(_wp1.position.x, _wp2.position.x), 
            UnityEngine.Random.Range(_wp1.position.y, _wp2.position.y), 
            UnityEngine.Random.Range(_wp1.position.z, _wp2.position.z));
    }

    void RaycastCheck()
    {
        RaycastHit hit;
        Vector3 direction = _player.transform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit, _maxDistance, _layerMask))
        {
            _currentStatus = (hit.collider.tag == PersoMainTag.persoTag) ? Status.Attacking : Status.Checking;
            
            if (_currentStatus == Status.Attacking && !_isFollowing)
            {
                // _isFollowing = true;
                // StartCoroutine("Follow");
            }
        }
    }

    Vector3 RandomizeTarget(Vector3 target)
    {
        Vector3 result;
        float randomDeltaX = Random.Range(-0.2f, 0.2f);
        float randomDeltaY = Random.Range(-0.2f, 0.8f);
        float randomDeltaZ = Random.Range(-0.2f, 0.2f);

        result = new Vector3(target.x + randomDeltaX, target.y + randomDeltaY, target.z + randomDeltaZ);
        return result;
    }

    void LookAtTarget()
    {
        _light.GetComponent<LookAt>().LockTarget(_player.transform.position);
    }

    #region Coroutines

    IEnumerator Patrol()
    {
        while (_currentStatus == Status.Patrolling)
        {
            RandomPos();

            while (transform.position != _targetPos && _currentStatus == Status.Patrolling)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPos, 0.05f);
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
        }
    }

    IEnumerator Follow()
    {
        while (_isFollowing)
        {
            while (transform.position.x != _player.transform.position.x)
            {
                Vector3 target = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, target, (_player.GetComponent<PersoMain>().Speed - 4.0f));

                yield return null;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator Shoot()
    {
        _isShooting = true;

        while (_player != null && _currentStatus == Status.Attacking)
        {
            GetComponent<SpawnProjectile>().Shoot(RandomizeTarget(_player.transform.position));

            yield return new WaitForSeconds(0.6f);
        }

        _isShooting = false;
    }

    #endregion Coroutines

    #endregion Private methods

    #region Public methods

    public void Deactivate()
    {
        _currentStatus = Status.Deactivated;

        foreach (Spin helice in _helices)
        {
           helice.StopSpin();
        }

        _rb.useGravity = true;
        _light.GetComponent<Light>().enabled = false;
        enabled = false;
    }

    public void DetectPerso(GameObject target)
    {
        _player = target;
        
        if (_currentStatus == Status.Patrolling)
        {
            _currentStatus = Status.Checking;
        }
    }

    #endregion Public methods
}
