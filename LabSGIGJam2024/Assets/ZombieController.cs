using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    Vector3 currentTarget = new Vector3(0, 0, 0);
    NavMeshAgent agent;
    public Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        if(playerRB != null)
        {
            currentTarget = playerRB.transform.position;
        }
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(currentTarget);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (playerRB != null)
        {
            currentTarget = playerRB.transform.position;
        }
        if (currentTarget != null)
        {
            RotateTowardsTarget(currentTarget);
        }
        agent.SetDestination(currentTarget);
    }

    void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }


}
