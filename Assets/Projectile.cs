using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour
{
    public float Speed = 15f;
    public Vector2 Direction = Vector2.right;
    public float Lifetime = 2f;
    void Start()
    {
        // Debug.Log("Projectile created with direction: " + Direction + " and speed: " + Speed);
        // if (GetComponent<Rigidbody2D>() == null)
        // {
        //     gameObject.AddComponent<Rigidbody2D>().isKinematic = true;
        // }
        Destroy(gameObject, Lifetime);
    }

    void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
        // Debug.Log("Projectile moving in direction: " + Direction + " with speed: " + Speed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Projectile collided with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Solid"))
        {
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
