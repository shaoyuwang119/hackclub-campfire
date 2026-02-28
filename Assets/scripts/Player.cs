using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    void Update()
    {
        int lr_movement = bti(Input.GetKey(KeyCode.D)) - bti(Input.GetKey(KeyCode.A));

        if (lr_movement != 0)
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, 10f * lr_movement, 0.02f);

        int ud_movement = bti(Input.GetKey(KeyCode.W)) - bti(Input.GetKey(KeyCode.S));

        if (ud_movement != 0)
            rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, 10f * ud_movement, 0.02f);
    }

    int bti(bool b)
    {
        return b ? 1 : 0;
    }
}
