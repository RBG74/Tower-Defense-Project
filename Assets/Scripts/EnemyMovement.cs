using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed = 2.5f;

    private Transform target;
    [HideInInspector]
    public int waypointIndex = 0;
    [HideInInspector]
    public float distanceToWaypoint;

	void Start () {
        target = Waypoints.instances[0];
	}

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        distanceToWaypoint = Vector3.Distance(transform.position, target.position);

        if (distanceToWaypoint < .15f)
        {
            if (waypointIndex >= Waypoints.instances.Length-1)
            {
                //Arrivé au bout du path
                Destroy(gameObject);
                //TODO: handle some sort of score
            }
            else
            {
                waypointIndex++;
                target = Waypoints.instances[waypointIndex];
            }
        }
    }
}
