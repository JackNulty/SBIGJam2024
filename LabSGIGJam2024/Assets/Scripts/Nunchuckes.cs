using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nunchuckes : MonoBehaviour
{
    public Transform player;
    public float swingSpeed = 360f;
    public float swingDuration = 1.0f;
    public float despawnDelay = 0.5f;

    private float currentSwingTime = 0f;

    void Start()
    {
        StartCoroutine(SwingCoroutine());
    }

    IEnumerator SwingCoroutine()
    {
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
