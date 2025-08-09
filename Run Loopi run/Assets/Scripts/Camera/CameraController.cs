using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Transform camPos;

    void LateUpdate()
    {
        if (!Tutorial.finished) return;

        transform.position = camPos.position;

        Vector2 look = player.Look;
        transform.rotation = Quaternion.Euler(look.y, look.x, 0);
    }
}