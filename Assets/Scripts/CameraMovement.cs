using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 0;
    private float xMax;
    private float yMin;
    private Bounds boundingBox;
    private float padding = 1.0f; // Padding around the bounding box
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        SetCam(boundingBox);
        return;
        GetInput();
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
        float boundAspect = boundingBox.size.x / boundingBox.size.y;
        camera.orthographicSize = (boundAspect > screenAspect) ? (boundingBox.size.x / screenAspect) / 2 : boundingBox.size.y / 2;
        camera.orthographicSize += padding;
        camera.transform.position = new Vector3(boundingBox.center.x, boundingBox.center.y, camera.transform.position.z);
    
    }
    public void SetLimits(Vector3 maxTile)
    {
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMax = maxTile.x - wp.x;
        yMin = maxTile.y - wp.y;

    }
}
