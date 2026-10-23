using UnityEngine;

public class LookAtCarema : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookInverted,
        CameraForward,
        CameraForwardInverted
    }

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookInverted:
                Vector3 direction = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + direction);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
