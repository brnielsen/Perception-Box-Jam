using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public bool rotate = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
