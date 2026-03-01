using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Vector3 origin;
    protected Vector3 actual_target;

    [SerializeField]
    protected Transform target;

    [SerializeField]
    protected SpriteRenderer sprite;

    protected float t = 0f;
    protected float speed = 0.3f;
    protected float z = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = transform.position;
        actual_target = target.position;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        float x = Mathf.Lerp(origin.x, actual_target.x, t);
        float y = Mathf.Lerp(origin.y, actual_target.y, t);
        t += Time.fixedDeltaTime * speed;
        transform.position = new Vector3(x, y, z);

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
