using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 10f; 
    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        if (WayPoint.points.Length > 0)
        {
            target = WayPoint.points[0];
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        RotateTowardsTarget(dir);
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }
    }

    void RotateTowardsTarget(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= WayPoint.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = WayPoint.points[waypointIndex];
    }
}