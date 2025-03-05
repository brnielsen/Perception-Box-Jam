using System;
using Rive;
using Rive.Components;
using UnityEngine;

public class RiveInputTest : MonoBehaviour
{
    [SerializeField] private RiveWidget _riveWidget;
    StateMachine _stateMachine;
    SMITrigger _idleTrigger;
    SMITrigger _runTrigger;
    SMITrigger _yogaTrigger;
    SMITrigger _strikeTrigger;

    [Header("Movement References")]
    [SerializeField] private RectTransform _playerRectTransform;
    [SerializeField] private PlayerBulletHeck _playerBulletHeck;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private bool _canWin;


    bool _isRunning;

    private void Start()
    {
        _stateMachine = _riveWidget.StateMachine;
        _playerBulletHeck = FindFirstObjectByType<PlayerBulletHeck>();

        foreach (var input in _stateMachine.Inputs())
        {
            Debug.Log(input.Name);
            if (input.Name == "Run Trigger")
            {
                _runTrigger = _stateMachine.GetTrigger(input.Name);
            }

            if (input.Name == "Idle Trigger")
            {
                _idleTrigger = _stateMachine.GetTrigger(input.Name);
            }

            if (input.Name == "Strike Trigger")
            {
                _strikeTrigger = _stateMachine.GetTrigger(input.Name);
            }

            if (input.Name == "Yoga Trigger")
            {
                _yogaTrigger = _stateMachine.GetTrigger(input.Name);
            }
        }

        GetComponent<PlayerBulletHeck>().OnPlayerHit += PlayerBulletHeck_OnPlayerHit;

    }


    public void PlayerBulletHeck_OnPlayerHit(object sender, EventArgs e)
    {
        TakeHit();
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (_isRunning == false)
            {
                _isRunning = true;
                _runTrigger.Fire();
                _playerBulletHeck.YogaMode = false;

            }
            _playerRectTransform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * _moveSpeed, 0, 0));
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                _playerRectTransform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                _playerRectTransform.localScale = new Vector3(1, 1, 1);
            }

        }
        else
        {
            if (_isRunning == true)
            {
                _isRunning = false;
                _idleTrigger.Fire();
                _playerBulletHeck.YogaMode = false;

            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoYoga();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (_canWin == false)
            {
                return;
            }
            _idleTrigger.Fire();
            _playerBulletHeck.YogaMode = false;

        }
    }

    [ContextMenu("Yoga Trigger")]
    public void DoYoga()
    {
        if (_canWin == false)
        {
            return;
        }
        _yogaTrigger.Fire();
        _playerBulletHeck.YogaMode = true;
    }

    [ContextMenu("Hit test")]
    public void TakeHit()
    {
        _strikeTrigger.Fire();
        _playerBulletHeck.YogaMode = false;

    }


}
