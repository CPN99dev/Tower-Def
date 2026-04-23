using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public static Transform[] points;

    void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    void OnDrawGizmos()
    {
        if (transform.childCount < 2) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.2f);
        }
        Gizmos.DrawSphere(transform.GetChild(transform.childCount - 1).position, 0.2f);
    }
}