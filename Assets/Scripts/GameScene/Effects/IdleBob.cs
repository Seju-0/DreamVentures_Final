using UnityEngine;

public class IdleBob : MonoBehaviour
{
    public float amplitude = 10f;     // How high it bobs (in units)
    public float frequency = 1f;      // How fast it bobs

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = startPos + new Vector3(0f, newY, 0f);
    }
}
