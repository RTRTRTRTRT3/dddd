using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 60f;
    public float acceleration = 3f; 
    public float rotationSpeed = 5f;

    private Rigidbody rb;
    private float currentSpeed = 0f; 
    private int currentWaypointIndex = 0; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
        if (waypoints == null || waypoints.Length == 0)
        {
            waypoints = new Transform[5];
            for (int i = 0; i < 5; i++)
            {
                waypoints[i] = GameObject.Find("waypoint" + (i + 1)).transform;
            }
        }
    }

    void Update()
    {
      
        float distance = Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position);

        currentSpeed += acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, speed); 

        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (distance > 1f) 
        {
        }
        else
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 
        }
    }
}
