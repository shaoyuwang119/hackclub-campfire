using UnityEngine;

public class enemy : MonoBehaviour
{
    Vector3 origin;
    Vector3 target;

    float t = 0f;
    float speed = 0.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = transform.position;
        target = new Vector3(origin.x + 10, origin.y, origin.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Mathf.Lerp(origin.x, target.x, t);
        float y = Mathf.Lerp(origin.y, target.y, t);
        t += Time.fixedDeltaTime * speed;
        transform.position = new Vector3(x, y, 0);

        if (t >= 1f)
        {
            Vector3 temp = origin;
            origin = target;
            target = temp;
            t = 0;
        }

    }
}
