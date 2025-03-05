using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PickUpObject : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;

    [SerializeField][VariablePopup(true)] private string _gameCompletedVariable;

    public static event EventHandler<OnPickUpEventArgs> OnPickUp;
    public class OnPickUpEventArgs : EventArgs
    {
        public Sprite sprite;
    }

    void Start()
    {
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_gameCompletedVariable != null && DialogueLua.GetVariable(_gameCompletedVariable).asBool == true)
            {
                OnPickUp?.Invoke(this, new OnPickUpEventArgs { sprite = _sprite });
                gameObject.SetActive(false);
            }

            if (string.IsNullOrEmpty(_gameCompletedVariable))
            {
                OnPickUp?.Invoke(this, new OnPickUpEventArgs { sprite = _sprite });
                gameObject.SetActive(false);
            }

        }
    }
}
