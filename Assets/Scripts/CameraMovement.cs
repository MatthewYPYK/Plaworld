using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField][Range(0,1)]
    private float upPad = 0, rightPad = 0, downPad = 0, leftPad = 0, padding = 0;
    [SerializeField]
    private float cameraSpeed = 0;
    private float xMax;
    private float yMin;
    private Bounds boundingBox;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        SetCam(boundingBox);
        return;
        // GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin, 0), -10);
    }

    public void SetCam(Bounds _boundingBox)
    {
        boundingBox = _boundingBox;
        Camera camera = Camera.main;
        float screenAspect = Screen.width / (float)Screen.height;
        float w = boundingBox.size.x / (1 - (rightPad + leftPad + 2*padding));
        float h = boundingBox.size.y / (1 - (upPad + downPad + 2*padding));
        float x = boundingBox.center.x / (1 - (rightPad - leftPad));
        float y = boundingBox.center.y / (1 - (downPad - upPad));
        float boundAspect = w / h;
        camera.orthographicSize = (boundAspect > screenAspect) ? (w / screenAspect) / 2 : h / 2;
        camera.transform.position = new Vector3(x, y, camera.transform.position.z);

    }
    public void SetLimits(Vector3 maxTile)
    {
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMax = maxTile.x - wp.x;
        yMin = maxTile.y - wp.y;

    }
}
