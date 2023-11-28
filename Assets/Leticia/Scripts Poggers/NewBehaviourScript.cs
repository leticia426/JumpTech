using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform player;
    public float MinX, MaxX, MinY, MaxY;
    public float timelerp;

    private void FixedUpdate()
    {
        Vector3 newPosition = player.position + new Vector3(0, 0, -10);
        newPosition = Vector3.Lerp(transform.position, newPosition, timelerp);

        // Limitando a posição em X e Y
        newPosition = new Vector3(Mathf.Clamp(newPosition.x, MinX, MaxX), Mathf.Clamp(newPosition.y, MinY, MaxY), newPosition.z);

        transform.position = newPosition;
    }
}