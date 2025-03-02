using Rive;
using Rive.Components;
using UnityEngine;

public class RiveInputTest : MonoBehaviour
{
    [SerializeField] private RiveWidget _riveWidget;
    StateMachine _stateMachine;
    SMITrigger _idleTrigger;
    SMITrigger _runTrigger;

    [Header("Movement References")]
    [SerializeField] private RectTransform _playerRectTransform;
    [SerializeField] private float _moveSpeed = 10f;

    bool _isRunning;

    private void Start()
    {
        _stateMachine = _riveWidget.StateMachine;

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
        }

    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (_isRunning == false)
            {
                _isRunning = true;
                _idleTrigger.Fire();
            }
            _playerRectTransform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * _moveSpeed, 0, 0));
            if(Input.GetAxisRaw("Horizontal") > 0){
                _playerRectTransform.localScale = new Vector3(-1, 1, 1);
            }else{
                _playerRectTransform.localScale = new Vector3(1, 1, 1);
            }

        }
        else
        {
            if (_isRunning == true)
            {
                _isRunning = false;
                _idleTrigger.Fire();
            }

        }
    }

    [ContextMenu("Run Trigger")]
    public void SetTrigger()
    {
        SMITrigger sMITrigger = _stateMachine.GetTrigger("RunTrigger");
        sMITrigger.Fire();
    }
}
