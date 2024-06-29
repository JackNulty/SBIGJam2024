using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotatorScript : MonoBehaviour
{
    public Transform arrow;
    public Transform player;
    public float circleRadius = 1.7f; 

    void Update()
    {
        RotateArrowTowardsMouse();
    }

    void RotateArrowTowardsMouse()
    {
        if (arrow == null || player == null)
        {
            Debug.LogError("Arrow or Player Transform is not assigned.");
            return;
        }

        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the mouse position from screen space to world space
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.transform.position.y));

        // Convert to Vector2 for 2D context
        Vector2 playerPosition = new Vector2(player.position.x, player.position.y);
        Vector2 mousePosition = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

        // Calculate the direction from the player to the mouse position
        Vector2 direction = (mousePosition - playerPosition).normalized;

        // Calculate the new position of the arrow, maintaining a uniform distance from the player
        Vector2 positionOnCircle = playerPosition + direction * circleRadius;

        // Set the arrow's position to the new calculated position
        arrow.position = positionOnCircle;

        // Calculate the rotation to look at the mouse position, locking the z-axis rotation
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        arrow.rotation = targetRotation;
        Debug.LogError(targetRotation);
    }
}
