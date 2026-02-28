using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 origin;
    Vector3 actual_target;

    [SerializeField]
    Transform target;

    float t = 0f;
    float speed = 0.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = transform.position;
        actual_target = target.position;
        Debug.Log($"{origin} {actual_target}");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Mathf.Lerp(origin.x, actual_target.x, t);
        float y = Mathf.Lerp(origin.y, actual_target.y, t);
        t += Time.fixedDeltaTime * speed;
        transform.position = new Vector3(x, y, 0);

        if (t >= 1f)
        {
            Vector3 temp = origin;
            origin = actual_target;
            actual_target = temp;
            t = 0;
        }

    }
}
