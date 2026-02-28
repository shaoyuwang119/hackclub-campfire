using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    new Transform camera;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    float movement_speed = 15f;

    [SerializeField]
    TMP_Text scoreDisplay;

    [SerializeField]
    TMP_Text crystalsDisplay;

    int crystals;
    int crystals_left;
    int crystals_base;

    List<GameObject> crystals_stored = new List<GameObject>();

    const float MIN_DIST = 4f;
    const int CRYSTALS_TOTAL = 5;

    Vector3 spawn_pos;
    Vector3 previous_tick_pos;

    void Start()
    {
        crystals = 0;
        crystals_base = 0;
        crystals_left = CRYSTALS_TOTAL;
        spawn_pos = transform.position;
        previous_tick_pos = spawn_pos;
        camera.position = new Vector3(transform.position.x, transform.position.y, camera.position.z);
    }

    void FixedUpdate()
    {
        UpdatePosition();
        UpdateCamera();
        UpdateDisplay();
    }

    void UpdatePosition()
    {
        int lr_movement = bti(Input.GetKey(KeyCode.D)) - bti(Input.GetKey(KeyCode.A));

        if (lr_movement != 0)
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, movement_speed * lr_movement, 0.02f);

        int ud_movement = bti(Input.GetKey(KeyCode.W)) - bti(Input.GetKey(KeyCode.S));

        if (ud_movement != 0)
            rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, movement_speed * ud_movement, 0.02f);

        if (rb.linearVelocityX != 0)
        {
            sprite.flipX = rb.linearVelocityX > 0;
        }

        float dist_base = Vector3.Distance(Base.singleton.transform.position, transform.position);
        float prev_dist_base = Vector3.Distance(Base.singleton.transform.position, previous_tick_pos);
        Debug.Log(dist_base);

        if (dist_base <= MIN_DIST && prev_dist_base > MIN_DIST)
        {
            Debug.Log("Close to base!");
            UpdateBase();
        }
        previous_tick_pos = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            transform.position = spawn_pos;
            crystals = 0;

            foreach (GameObject crystal_obj in crystals_stored)
            {
                crystal_obj.SetActive(true);
            }
            crystals_stored = new List<GameObject>();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Time.timeScale = 1f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Crystal>(out Crystal crystal))
        {
            crystals_stored.Add(crystal.gameObject);
            crystal.gameObject.SetActive(false);
            crystals += 1;
        }
    }

    void UpdateCamera()
    {
        float x = Mathf.Lerp(camera.position.x, transform.position.x, 0.1f);
        float y = Mathf.Lerp(camera.position.y, transform.position.y, 0.1f);
        camera.position = new Vector3(x, y, camera.position.z);
    }

    void UpdateDisplay()
    {
        crystalsDisplay.text = $"{crystals}";
        scoreDisplay.text = $"{crystals_base} / {crystals_left}";
    }

    void UpdateBase()
    {
        crystals_base += crystals;
        crystals_left -= crystals;
        crystals = 0;
        crystals_stored = new List<GameObject>();
    }

    int bti(bool b)
    {
        return b ? 1 : 0;
    }
}
