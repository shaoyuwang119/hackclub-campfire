using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    new Transform camera;

    [SerializeField]
    float movement_speed = 15f;

    int score;

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


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit something!");
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1f;
                //TODO: add death animation / YOU DIED ui if time allows
            } else if (collision.gameObject.TryGetComponent<Crystal>(out Crystal crystal))
            {
                Debug.Log("Got a crystal");
                Destroy(crystal);
                score += 1;
            }
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
