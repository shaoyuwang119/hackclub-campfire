using UnityEngine;

public class Bubble : MonoBehaviour
{
    float time;
    float speed;
    float scale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(true);
        speed = Random.Range(0.0025f, 0.005f);
        float scale = Random.Range(0.25f, 0.4f);
        transform.localScale = new Vector3(scale, scale, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y += speed;
        transform.position = pos;
        time += Time.deltaTime;
        if (time > 4f)
        {
            Destroy(gameObject);
        }
    }
}
