using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PickUpObject : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;

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
            OnPickUp?.Invoke(this, new OnPickUpEventArgs { sprite = _sprite });
            gameObject.SetActive(false);
        }
    }
}
