using UnityEngine;

public class Tendril : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private bool isActive = false;

    private void Update()
    {
        if (isActive)
        {
            Extend();
        }
    }

    public void Extend()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + speed * Time.deltaTime);

    }


    public void Activate()
    {
        isActive = true;
    }
}
