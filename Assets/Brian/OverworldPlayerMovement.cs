using UnityEngine;

public class OverworldPlayerMovement : MonoBehaviour
{
   public float speed = 10f;
   public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {   
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movementDirection = new Vector3(0f , 0f, verticalInput).normalized;
        // Move the player
        transform.Translate(movementDirection * speed * Time.deltaTime);

        //rotate the player
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        
    }
}
