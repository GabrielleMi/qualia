using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PersoMainTag
{
    public static string persoTag = "CharacterBody";
}

public class PersoMain : Perso
{
    #region Properties

    private const string ANIM_GRAB_NAME = "isGrabbing";
    private const string ANIM_JUMP_NAME = "isJumping";
    private const float RIGHT_ANGLE = 90.0f;

    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _characterBody;
    [SerializeField] private Transform _checkpoint;
    [SerializeField] private Color _dangerColor;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private float _jumpForce = 65;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private MagnetRotate _magnetRotate;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _sprintSpeed = 10;

    private bool _canMove = true;
    private float _currentSpeed;
    private int _dir = 1;
    private Color _healthInitColor;
    private Vector3 _healthBarInitWidth;
    private float _initVelocityY;
    private bool _isGrabbing = false;
    private float _rayDistance = 0.1f;
    private GameObject _toGrab;

    #endregion Properties

    #region Getters & Setters

    public Transform CheckPoint
    {
        get { return _checkpoint; }
        set { _checkpoint = value; }
    }

    public float Speed
    {
        get { return _currentSpeed; }
    }

    #endregion Getters & Setters

    #region Private methods

    override protected void Start()
    {
        base.Start();

        _healthInitColor = _healthBar.GetComponent<Image>().color;
        _isGrounded = false;
        _rb.isKinematic = false;
        _currentSpeed = _speed;
        _initVelocityY = _rb.velocity.y;

    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !_isGrabbing)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Clone();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (_toGrab != null)
            {
                _isGrabbing = true;

                if (_toGrab.GetComponent<FixedJoint>() == null)
                {
                    FixedJoint joint = _toGrab.AddComponent<FixedJoint>();

                    joint.connectedBody = _rb;
                    _toGrab.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionX;
                    _anim.SetBool(ANIM_GRAB_NAME, _isGrabbing);
                    _currentSpeed = 1;
                }
            }
        }
        else
        {
            if (_isGrabbing)
            {
                _isGrabbing = false;

                if (_toGrab.GetComponent<FixedJoint>().connectedBody != null)
                {
                    Destroy(_toGrab.GetComponent<FixedJoint>());
                    _toGrab.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
                    _anim.SetBool(ANIM_GRAB_NAME, _isGrabbing);
                    _currentSpeed = _speed;
                }
            }
        }
    }

    protected override void FixedUpdate()
    {
        if (!_isGrounded)
        {
            RaycastCheck();
        }

        base.FixedUpdate();

        Move();
    }

    protected override void Attracted()
    {
        _magnetRotate.Rotate();
    }

    void Move()
    {
        if (_canMove)
        {
            float hAxis = Input.GetAxis("Horizontal");
            float velX = hAxis * _currentSpeed;

            _rb.velocity = new Vector3(velX, _rb.velocity.y, 0.0f);
            _anim.SetFloat("VelX", velX / _sprintSpeed);

            if ((hAxis < 0 && _dir == 1 || hAxis > 0 && _dir == -1) && !_isGrabbing)
            {
                StartCoroutine(Turn());
            }
        }

    }

    void Jump()
    {
        if (_canMove && _isGrounded)
        {
            _isGrounded = false;
            _rb.AddForce(0, _jumpForce * (_isMagnetAttracted ? -1 : 1), 0, ForceMode.Impulse);
            _anim.SetBool(ANIM_JUMP_NAME, true);
        }
    }

    void RaycastCheck()
    {

        if (Physics.Raycast(_characterBody.transform.position, _characterBody.transform.TransformDirection(-Vector3.up), _rayDistance, _layers))
        {
            _isGrounded = true;
            _anim.SetBool(ANIM_JUMP_NAME, false);
        }
        else
        {
            Vector3 forward = _characterBody.transform.TransformDirection(-Vector3.up) * _rayDistance;
        }
    }

    void Clone()
    {
        if (_isGrabbing)
        {
            _toGrab.GetComponent<FixedJoint>().connectedBody = null;
            _toGrab.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            _isGrabbing = false;
        }

        GameManager.Instance.SpawnNewClone(transform);
    }

    void ResetPos()
    {
        transform.position = _checkpoint.position;
    }

    void SetHealthBar(float healthScale)
    {
        _healthBar.transform.localScale = new Vector3(healthScale, 1.0f, 1.0f);
        _healthBar.GetComponent<Image>().color = Color.Lerp(Color.red, _healthInitColor, healthScale);
    }

    protected override void Damaged()
    {
        float healthScale = _health / _maxHealth;
        SetHealthBar(healthScale);
    }

    protected override void Destroyed()
    {
        base.Destroyed();
        _health = _maxHealth;
        GameManager.Instance.NewDeath();
        transform.position = _checkpoint.position;
        SetHealthBar(1.0f);  
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if ((other.gameObject.tag == PersoMainTag.persoTag || other.gameObject.tag == "Drone" ) && _isGrounded)
        {
            _toGrab = other.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);

        if (other.gameObject.tag == PersoMainTag.persoTag || other.gameObject.tag == "Drone")
        {
            _toGrab = null;
        }
    }

    public void Die()
    {
        Destroyed();
    }

    #endregion Private methods

    #region Coroutines

    IEnumerator Turn()
    {
        _dir = _dir * -1;
        
        while (!(_characterBody.transform.localRotation.y >= (RIGHT_ANGLE - 1) * _dir && _characterBody.transform.localRotation.y <= (RIGHT_ANGLE + 1) * _dir))
        {
            _characterBody.transform.localRotation = 
                Quaternion.RotateTowards(_characterBody.transform.localRotation, 
                Quaternion.Euler(_characterBody.transform.localRotation.eulerAngles.x, RIGHT_ANGLE * _dir, _characterBody.transform.localRotation.eulerAngles.z), 5.0f);
            yield return null;
        }
    }

    #endregion Coroutines
}
