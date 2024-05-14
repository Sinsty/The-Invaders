using UnityEngine;

public class CameraStabilization : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0), 1);
    }
}
