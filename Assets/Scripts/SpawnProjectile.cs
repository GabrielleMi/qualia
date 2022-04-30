using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{

    #region Properties

    [SerializeField] private GameObject _canon1;
    [SerializeField] private GameObject _canon2;
    [SerializeField] private GameObject _effect;

    private bool _isUsingCanon1 = true;

    private Vector3 _spawnPoint;
    private Vector3 _target;

    #endregion Properties

    #region Private methods

    void Start()
    {
        _spawnPoint = _isUsingCanon1 ? _canon1.transform.position : _canon2.transform.position;
    }

    void SwitchSpawnPoint()
    {
        _isUsingCanon1 = !_isUsingCanon1;
        _spawnPoint = _isUsingCanon1 ? _canon1.transform.position : _canon2.transform.position;
    }

    void SpawnEffect()
    {
        GameObject projectile = Instantiate(_effect, _spawnPoint, Quaternion.identity);
        projectile.GetComponent<Projectile>().Target = _target;
        SwitchSpawnPoint();
    }

    #endregion Private methods

    #region Public methods

    public void Shoot(Vector3 target)
    {
        _target = target;
        SpawnEffect();
    }

    #endregion Public methods
}
