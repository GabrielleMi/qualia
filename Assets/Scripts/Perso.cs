using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perso : MonoBehaviour
{
    [System.Serializable]
    public class FloorInfo
    {
        public float x;
        public float y;
        public float z;
        public float rx;
        public float ry;
        public float rz;
        public float type;

        public FloorInfo(Transform pos, bool isClone)
        {
            x = pos.position.x;
            y = pos.position.y;
            z = pos.position.z;
            rx = pos.rotation.x;
            ry = pos.rotation.y;
            rz = pos.rotation.y;
            type = isClone ? 1 : 0;
        }

    }

    #region Properties

    [SerializeField] protected Color _color;
    [SerializeField] protected Light _light;
    [SerializeField] protected SkinnedMeshRenderer _screen;
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected Material _screenMaterial;

    protected float _health;
    protected bool _isGrounded = false;
    protected bool _isMagnetAttracted = false;
    protected float _maxHealth = 5;
    protected Vector3 _upside;
    protected Vector3 _upsideDown;

    #endregion Properties

    #region Setters & Getters

    public bool IsGrounded {
        get { return _isGrounded; }
        set { _isGrounded = value;  }
    }

    public bool IsMagnetAttracted
    {
        get { return _isMagnetAttracted; }
        set { _isMagnetAttracted = value; }
    }

    #endregion Setters & Getters

    #region Private methods

    protected virtual void Start()
    {
        _screen.material.SetColor("_EmissionColor", _color);
        _light.color = _color;
        _health = _maxHealth;
    }

    protected virtual void FixedUpdate()
    {
        if (_isMagnetAttracted)
        {
            InverseGravity();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Magnet")
        {
            if (other.gameObject.GetComponent<Magnet>().IsOn)
            {
                ToggleMagneticInfluence();
            }

        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Magnet")
        {
            ToggleMagneticInfluence();
        }
    }

    protected void InverseGravity()
    {
        _rb.AddForce(Physics.gravity * -1, ForceMode.Acceleration);
    }

    protected void ToggleMagneticInfluence()
    {
        if (_isMagnetAttracted)
        {
            _isMagnetAttracted = false;
            _rb.useGravity = true;
        }
        else if (!_isMagnetAttracted)
        {
            _rb.useGravity = false;
            _isMagnetAttracted = true;
        }

        Attracted();
    }

    protected virtual void Attracted(){}
    protected virtual void Damaged(){}
    protected virtual void Destroyed(){}

    #endregion Private methods

    #region Public methods

    public void Dmg()
    {
        _health--;

        if (_health <= 0)
        {
            Destroyed();
        }
        else
        {

            float percent = (_health / _maxHealth) * 100;
            Damaged();
        }
    }

    #endregion Public methods

}
