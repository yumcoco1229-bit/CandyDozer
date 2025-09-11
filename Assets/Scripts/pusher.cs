using UnityEngine;

public class pusher : MonoBehaviour
{
    Vector3 startPosition;
    public float amplitude;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.localPosition; 
    }

    // Update is called once per frame
    void Update()
    {
        float z = amplitude * Mathf.Sin(Time.time * speed);
        transform.localPosition = startPosition + new Vector3(0, 0, z);
    }
}
