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
    public GameObject weaponHolder;
    public GameObject meleeWeapon;

    static bool meleeStart = false;

    private float shortestDistance = 10000;

    float currentAngle = 0.0f;
    bool swingWeapon = false;

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
        if(meleeStart == true && swingWeapon == false)
        {
            meleeWeapon.gameObject.SetActive(true);
            meleeStart = false;

            currentAngle = -60.0f;
            weaponHolder.transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
            swingWeapon = true;
        }

        if (swingWeapon == true)
        {
            weaponHolder.transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
            currentAngle += 5;
            if (currentAngle > 60.0f)
            {
                meleeWeapon.gameObject.SetActive(false);
                swingWeapon = false;
            }
        }


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

    public static void MakeCompanionMelee()
    {
        meleeStart = true;
    }
}
