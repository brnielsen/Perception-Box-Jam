using System;
using System.Collections;
using Rive;
using Rive.Components;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreathingGameControls : MonoBehaviour
{
    [SerializeField] private RiveWidget _riveWidget;

    [SerializeField] private int _requiredBreaths = 3;
    [SerializeField] private TextMeshProUGUI _breathsText;

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
            _breathsText.text = $"Breaths: {_currentBreaths}";

            if (_currentBreaths >= _requiredBreaths)
            {
                StartCoroutine(EndScene());
            }
        }
    }

    private IEnumerator EndScene()
    {
        _breathsText.text = "Now with a new frame of mind approach Olivia and see what she has to say";
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneToLoad);
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
