using UnityEngine;

public class PlayerBulletHeck : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }
}
