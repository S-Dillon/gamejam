using UnityEngine;

namespace TarodevController
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private ScriptableStats _stats;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _shootCooldown = 0.3f;
        private float _shootTimer;
        private bool _shootInput;
        private bool _isFacingRight = true;

        private void Update()
        {
            GatherInput();
            UpdateFacingDirection();
            HandleShooting();
            HandleShootCooldown();
        }

        private void GatherInput()
        {
            if (_shootTimer <= 0 && Input.GetButtonDown("Fire1"))
            {
                _shootInput = true;
            }
        }
        private void UpdateFacingDirection()
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
            _isFacingRight = true;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
            _isFacingRight = false;
            }
            // Debug.Log("Facing Right: " + _isFacingRight);
        }

        private void HandleShooting()
        {
            if (_shootInput)
            {
                _shootTimer = _shootCooldown;
                Shoot();
                _shootInput = false;
            }
        }

        private void Shoot()
        {
            var bulletObject = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
            var bullet = bulletObject.GetComponent<Projectile>();
            bullet.Direction = _isFacingRight ? Vector2.right : Vector2.left;
            // Debug.Log("Bullet Direction: " + bullet.Direction);
        }

        private void HandleShootCooldown()
        {
            if (_shootTimer > 0) _shootTimer -= Time.deltaTime;
        }
    }
}
