using UnityEngine;

public class DecorAnimation : MonoBehaviour
{
    public Sprite[] frames;
    public float frameRate = 15f;

    private SpriteRenderer sr;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        currentFrame = Random.Range(0, frames.Length);
        sr.sprite = frames[currentFrame];

        timer = Random.Range(0f, 1f / frameRate);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;
            sr.sprite = frames[currentFrame];
        }
    }
}