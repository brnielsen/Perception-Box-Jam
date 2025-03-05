using Rive;
using Rive.Components;
using UnityEngine;

public class BreathingGameControls : MonoBehaviour
{
    [SerializeField] private RiveWidget _riveWidget;

    private StateMachine _stateMachine;

    private SMITrigger _breatheTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _stateMachine = _riveWidget.StateMachine;

        foreach (var input in _stateMachine.Inputs())
        {
            Debug.Log(input.Name);
            if (input.Name == "Start Breathing")
            {
                _breatheTrigger = _stateMachine.GetTrigger(input.Name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _breatheTrigger.Fire();
        }
    }
}
