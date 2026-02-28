using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    new Transform camera;

    [SerializeField]
    Transform sprite;

    [SerializeField]
    float movement_speed = 15f;

    Vector3 spawn_pos;

    void Start()
    {
        spawn_pos = transform.position;
    }

    void FixedUpdate()
    {
        UpdatePosition();
        UpdateCamera();
    }

    void UpdatePosition()
    {
        int lr_movement = bti(Input.GetKey(KeyCode.D)) - bti(Input.GetKey(KeyCode.A));

        if (lr_movement != 0)
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, movement_speed * lr_movement, 0.02f);

        int ud_movement = bti(Input.GetKey(KeyCode.W)) - bti(Input.GetKey(KeyCode.S));

        if (ud_movement != 0)
            rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, movement_speed * ud_movement, 0.02f);

        Vector2 norm_dir = rb.linearVelocity.normalized;
        if (norm_dir.magnitude > 0)
        {
            float angle = Mathf.Atan2(-norm_dir.x, norm_dir.y) * Mathf.Rad2Deg;
            sprite.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void UpdateCamera()
    {
        float x = Mathf.Lerp(camera.position.x, transform.position.x, 0.1f);
        float y = Mathf.Lerp(camera.position.y, transform.position.y, 0.1f);
        camera.position = new Vector3(x, y, camera.position.z);
    }

    int bti(bool b)
    {
        return b ? 1 : 0;
    }
}
