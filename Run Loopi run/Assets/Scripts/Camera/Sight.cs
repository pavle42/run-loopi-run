using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public float maxDistance = 100f;

    private Hoverable currentlyHovered;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            Hoverable target = hit.collider.gameObject.GetComponent<Hoverable>();

            if (target != null)
            {
                if (target != currentlyHovered)
                {
                    currentlyHovered?.OnRayExit();
                    currentlyHovered = target;
                    currentlyHovered.OnRayEnter();
                }
            }
            else if (currentlyHovered != null)
            {
                currentlyHovered.OnRayExit();
                currentlyHovered = null;
            }
        }
        else
        {
            if (currentlyHovered != null)
            {
                currentlyHovered.OnRayExit();
                currentlyHovered = null;
            }
        }
    }
}
