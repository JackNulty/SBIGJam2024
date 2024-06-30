using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingMelee : MonoBehaviour
{
    public Transform player;
    public float swingSpeed = 120f;
    public float swingDuration = 0.7f;
    public float despawnDelay = 0.2f;

    private float currentSwingTime = 0f;

    void Start()
    {
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position).normalized;
        direction.z = 0;
        transform.position = player.position + direction * 1.0f; // 1.0f is the distance in front of the player
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        StartCoroutine(SwingCoroutine());
    }

    IEnumerator SwingCoroutine()
    {
        transform.RotateAround(player.position, Vector3.forward, -35);
        while (currentSwingTime < swingDuration)
        {
            float angle = swingSpeed * Time.deltaTime;
            transform.RotateAround(player.position, Vector3.forward, angle);

            currentSwingTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(despawnDelay);

        Destroy(gameObject);
    }

    
}
