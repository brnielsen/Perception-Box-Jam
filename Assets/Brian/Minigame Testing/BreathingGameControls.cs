using System;
using Rive;
using Rive.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreathingGameControls : MonoBehaviour
{
    [SerializeField] private RiveWidget _riveWidget;

    [SerializeField] private int _requiredBreaths = 3;

    public string sceneToLoad = "GameScene";

    private int _currentBreaths = 0;

    private StateMachine _stateMachine;

    private SMITrigger _breatheTrigger;

    void OnEnable()
    {
        _riveWidget.OnRiveEventReported += OnRiveEventReported;
    }

    void OnDisable()
    {
        _riveWidget.OnRiveEventReported -= OnRiveEventReported;
    }

    private void OnRiveEventReported(ReportedEvent @event)
    {
        Debug.Log(@event.Name);
        if (@event.Name == "OnBreatheComplete")
        {
            _currentBreaths++;
            if (_currentBreaths >= _requiredBreaths)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

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
