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
    TMP_Text score_display;

    [SerializeField]
    TMP_Text crystals_display;

    [SerializeField]
    GameObject pause_screen;

    [SerializeField]
    GameObject win_screen;

    [SerializeField]
    string main_menu_name = "main menu";

    public GameObject death_screen;
    public GameObject bubble;

    int crystals;
    int crystals_left;
    int crystals_base;

    Vector3 spawn_pos;

    List<GameObject> crystals_stored = new List<GameObject>();

    float previousWave;
    float bubble_timer;

    void Start()
    {
        crystals = 0;
        crystals_base = 0;
        crystals_left = Crystal.crystal_count;
        spawn_pos = transform.position;
        camera.position = new Vector3(transform.position.x, transform.position.y, camera.position.z);
    }

    void Update()
    {
        if (!death_screen.activeSelf && !win_screen.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
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
        {
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, movement_speed * lr_movement, 0.02f);
            sprite.flipX = lr_movement > 0 ? true : false;
        }

        int ud_movement = bti(Input.GetKey(KeyCode.W)) - bti(Input.GetKey(KeyCode.S));

        if (ud_movement != 0)
            rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, movement_speed * ud_movement, 0.02f);

        if (Mathf.Abs(rb.linearVelocityX) > 1f || Mathf.Abs(rb.linearVelocityY) > 1f)
        {
            if (bubble_timer > 0.5f)
            {
                Vector3 bubble_pos = gameObject.transform.position;
                bubble_pos.x += sprite.flipX ? -1 : 1;
                Instantiate(bubble, bubble_pos, Quaternion.identity);
                bubble_timer = 0;
            }
        }
        bubble_timer += Time.fixedDeltaTime;

        float currentWave = Mathf.Sin(Time.time) * 0.4f;
        float delta = currentWave - previousWave;
        // the below code bobs the sprite image instead of the whole sprite object
        //Vector3 pos = sprite.transform.position;
        //pos.y += delta;
        //sprite.transform.position = pos;

        rb.linearVelocityY += delta;
        previousWave = currentWave;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            transform.position = spawn_pos;
            camera.transform.position = new Vector3(spawn_pos.x, spawn_pos.y, camera.transform.position.z);
            crystals = 0;
            Timer.currentTime = 0;

            foreach (GameObject crystal_obj in crystals_stored)
            {
                crystal_obj.SetActive(true);
            }
            crystals_stored = new List<GameObject>();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Time.timeScale = 1f;

            Die();
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
        else if (collision.gameObject.TryGetComponent<BaseZone>(out BaseZone basezone))
        {
            Debug.Log("Close to base!");
            UpdateBase();
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
        score_display.text = $"{crystals_base}";
        crystals_display.text = $"{crystals} / {crystals_left}";
    }

    void UpdateBase()
    {
        crystals_base += crystals;
        crystals_left = Crystal.crystal_count - crystals_base;
        crystals = 0;
        crystals_stored = new List<GameObject>();

        if (crystals_left == 0)
            Win();
    }

    void Win()
    {
        win_screen.SetActive(true);
        Time.timeScale = 0f;
    }

    void Die()
    {
        death_screen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Respawn()
    {
        death_screen.SetActive(false);
        Time.timeScale = 1f;
    }

    int bti(bool b)
    {
        return b ? 1 : 0;
    }

    public void Unpause()
    {
        pause_screen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        SceneManager.LoadScene(main_menu_name);
    }

    void PauseToggle()
    {
        bool paused = pause_screen.activeSelf;

        pause_screen.SetActive(!paused);
        Time.timeScale = paused ? 1f : 0f;
    }
}
