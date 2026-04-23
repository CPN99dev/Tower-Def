using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 20f;
    public float damage = 20f;

    public void Seek(Transform _target) => target = _target;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = (Vector2)target.position - (Vector2)transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        RotateTowardsTarget(dir);
    }

    void RotateTowardsTarget(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void HitTarget()
    {
        if (target.TryGetComponent<EnemyHealth>(out EnemyHealth health))
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}