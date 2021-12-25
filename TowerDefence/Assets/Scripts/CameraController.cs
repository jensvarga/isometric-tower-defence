using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 10f;
    [SerializeField] private GameObject cameraTrackObject;

    private float panBorderThickness = 100f;
    private bool enableMousePan = false;

    [SerializeField] private Vector2 panLimit;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = cameraTrackObject.transform.position;

        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow) || (Input.mousePosition.y >= Screen.height - panBorderThickness && enableMousePan))
        {
            position.z += panSpeed * Time.unscaledDeltaTime;
            position.x += panSpeed * Time.unscaledDeltaTime;
        }

        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) || (Input.mousePosition.y <= panBorderThickness && enableMousePan))
        {
            position.z -= panSpeed * Time.unscaledDeltaTime;
            position.x -= panSpeed * Time.unscaledDeltaTime;
        }

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || (Input.mousePosition.x <= panBorderThickness && enableMousePan))
        {
            position.z += panSpeed * Time.unscaledDeltaTime;
            position.x -= panSpeed * Time.unscaledDeltaTime;
        }

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || (Input.mousePosition.x >= Screen.width - panBorderThickness && enableMousePan))
        {
            position.z -= panSpeed * Time.unscaledDeltaTime;
            position.x += panSpeed * Time.unscaledDeltaTime;
        }

        position.x = Mathf.Clamp(position.x, -panLimit.x, panLimit.x);
        position.z = Mathf.Clamp(position.z, -panLimit.y, panLimit.y);

        cameraTrackObject.transform.position = Vector3.Lerp(cameraTrackObject.transform.position, position, Time.unscaledDeltaTime * 100f);
    }
}
