using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movable : MonoBehaviour
{
    [Header("Hold & Rotation Settings")]
    public float holdDistance = 2f;
    public float rotateSpeed = 200f;

    Rigidbody rb;
    Hoverable hoverable;
    bool grabbed;
    bool originalGravity;
    Camera cam;
    const float margin = 0.5f;

    float scrollBuffer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hoverable = GetComponent<Hoverable>();
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!grabbed && hoverable != null && hoverable.isHovered)
                Grab();
        }
        if (Input.GetMouseButtonUp(1) && grabbed)
        {
            Release();
        }

        if (grabbed)
            scrollBuffer += Input.mouseScrollDelta.y;
    }

    void FixedUpdate()
    {
        if (!grabbed) return;

        HandleHolding();
        HandleScrollRotation();
    }

    void Grab()
    {
        grabbed = true;
        originalGravity = rb.useGravity;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Release()
    {
        grabbed = false;
        rb.useGravity = originalGravity;
        scrollBuffer = 0f;
    }

    void HandleHolding()
    {
        Vector3 origin = cam.transform.position;
        Vector3 dir = cam.transform.forward;
        float dist = holdDistance;

        RaycastHit[] hits = Physics.RaycastAll(origin, dir, holdDistance + margin,
                                               ~0, QueryTriggerInteraction.Ignore);
        if (hits.Length > 0)
        {
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
            foreach (var h in hits)
            {
                if (h.rigidbody == rb || h.collider.transform.IsChildOf(transform)) continue;
                dist = Mathf.Max(h.distance - margin, 0.5f);
                break;
            }
        }

        rb.MovePosition(origin + dir * dist);
    }

    void HandleScrollRotation()
    {
        if (Mathf.Abs(scrollBuffer) < 0.001f) return;

        float deltaDegrees = scrollBuffer * rotateSpeed;
        Quaternion delta = Quaternion.Euler(0f, deltaDegrees, 0f);

        rb.MoveRotation(rb.rotation * delta);
        scrollBuffer = 0f;
    }
}
