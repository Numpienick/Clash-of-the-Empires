using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10;
    public Vector2 panLimit;
    public float scrollSpeed = 20;
    public float minY = 20f;
    public float maxY = 200f;
    public float pivotSpeed = 100f;

    void Update()
    {

        Vector3 pos = transform.position;
        //Movement(wasd)
        transform.Translate(Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime * Vector3.right);
        transform.Translate(Input.GetAxis("Vertical") * panSpeed * Time.deltaTime * Vector3.forward);
        //Rotate camera(qe)
        transform.Rotate(Input.GetAxis("Rotate") * pivotSpeed * Vector3.down * Time.deltaTime);

        //Scroll/zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos = transform.position;
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        //Clamp camera
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}
