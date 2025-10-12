using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public Transform player;
    public Vector3 cameraOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + cameraOffset;
    }
}