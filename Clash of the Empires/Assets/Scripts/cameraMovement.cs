using UnityEngine;

public class cameraMovement : MonoBehaviour {

    public float panSpeed = 20f;
    public float panBorderThickness = 10;
    public Vector2 panLimit;
    public float scrollSpeed = 20;
    public float minY = 20f;
    public float maxY = 200f;
    public float pivotSpeed = 100f;

    void Update () {
        //panSpeed = 20f;
        Vector3 pos = transform.position;

		if (Input.GetKey("w"))
        {
            transform.Translate (panSpeed *  Vector3.back * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
           transform.Translate (panSpeed *  Vector3.forward * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            transform.Translate (panSpeed * Vector3.right * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            transform.Translate (panSpeed * Vector3.left * Time.deltaTime);
        }

        if (Input.GetKey("q"))
        {
            transform.Rotate (pivotSpeed * Vector3.down * Time.deltaTime);
        }

        if (Input.GetKey("e"))
        {
            transform.Rotate (pivotSpeed * Vector3.up * Time.deltaTime);
        }



        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f *  Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;

	}
}
