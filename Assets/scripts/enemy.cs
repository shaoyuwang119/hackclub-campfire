using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 origin;
    Vector3 actual_target;

    [SerializeField]
    Transform target;

    [SerializeField]
    SpriteRenderer sprite;

    float t = 0f;
    float speed = 0.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = transform.position;
        actual_target = target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Mathf.Lerp(origin.x, actual_target.x, t);
        float y = Mathf.Lerp(origin.y, actual_target.y, t);
        t += Time.fixedDeltaTime * speed;
        transform.position = new Vector3(x, y, 0);

        bool dir = origin.x > actual_target.x;
        sprite.flipX = dir;

        if (t >= 1f)
        {
            Vector3 temp = origin;
            origin = actual_target;
            actual_target = temp;
            t = 0;
        }

    }
}
