using Rive;
using Rive.Components;
using UnityEngine;

public class RiveInputTest : MonoBehaviour
{
    [SerializeField] private RiveWidget _riveWidget;
    StateMachine _stateMachine;

    private void Start()
    {
        _stateMachine = _riveWidget.StateMachine;

        foreach (var input in _stateMachine.Inputs())
        {
            Debug.Log(input.Name);
        }
    }

[ContextMenu("Run Trigger")]
    public void SetTrigger()
    {
        SMITrigger sMITrigger = _stateMachine.GetTrigger("RunTrigger");
        sMITrigger.Fire();
    }
}
