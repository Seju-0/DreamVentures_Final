using UnityEngine;

public class ClientBobbing : MonoBehaviour
{
    public float bobbingHeight = 0.1f; 
    public float bobbingSpeed = 5f;    

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

        if (!clientScript.HasReachedTarget() || clientScript.IsLeaving())
        {
            Vector3 pos = transform.position;
            pos.y = initialY + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
            transform.position = new Vector3(pos.x, pos.y, pos.z);
        }
    }
}
