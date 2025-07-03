using UnityEngine;

public class ClientBobbing : MonoBehaviour
{
    public float bobbingHeight = 0.1f; // How high to bob
    public float bobbingSpeed = 5f;    // How fast to bob

    private float initialY;
    private Client clientScript;

    void Start()
    {
        initialY = transform.position.y;
        clientScript = GetComponent<Client>();
    }

    void Update()
    {
        if (clientScript == null)
            return;

        // Bob only if moving to target or leaving
        if (!clientScript.HasReachedTarget() || clientScript.IsLeaving())
        {
            Vector3 pos = transform.position;
            pos.y = initialY + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
            transform.position = new Vector3(pos.x, pos.y, pos.z);
        }
    }
}
