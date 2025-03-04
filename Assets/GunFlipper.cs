using UnityEngine;

public class GunFlipper : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private void Update()
    {
        if (_playerTransform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Debug.Log("Gun Flipped: " + transform.localScale.x);
    }
}

