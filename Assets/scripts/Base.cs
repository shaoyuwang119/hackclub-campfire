using UnityEngine;

public class Base : MonoBehaviour
{
    public static Base singleton = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
