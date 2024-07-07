using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class companionMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject playerPoint1;
    public GameObject playerPoint2;
    public GameObject player;
    public GameObject playerArrow;

    private float shortestDistance = 10000;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 firstPos = playerPoint1.transform.position;
        Vector2 secondPos = playerPoint2.transform.position;
        Vector2 companionPos = this.transform.position;
        shortestDistance = Vector2.Distance(firstPos, companionPos);
        if (Vector2.Distance(secondPos, companionPos) < shortestDistance)
        {
            agent.SetDestination(playerPoint2.transform.position);
        }
        else
        {
            agent.SetDestination(playerPoint1.transform.position);
        }

        RotateTowardsTarget(playerArrow.transform.position);
    }
    void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }
}
