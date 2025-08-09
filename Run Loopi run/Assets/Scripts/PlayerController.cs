using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private CameraController cam;

    private Vector2 look;
    private Vector3 moveInput;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        if (!Tutorial.finished) return;

        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        look.x += Input.GetAxis("Mouse X");      // yaw
        look.y -= Input.GetAxis("Mouse Y");      // pitch (inverted)
        look.y = Mathf.Clamp(look.y, -70f, 70f);
    }

    void FixedUpdate()
    {
        if (!Tutorial.finished) return;

        Vector3 world = transform.TransformDirection(moveInput) * movementSpeed;
        rb.MovePosition(rb.position + world * Time.fixedDeltaTime);

        Quaternion target = Quaternion.Euler(0, look.x, 0);
        rb.MoveRotation(target);
    }

    public Vector2 Look => look;
}