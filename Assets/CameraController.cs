using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 1;
    public float scrollSens = 1;

    // Update is called once per frame
    void Update()
    {
        Vector3 movechange = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")) * -speed * Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            transform.position += movechange;
        }
        Vector3 change = new Vector3(0, -Input.mouseScrollDelta.y, 0) * scrollSens * Time.deltaTime;
        if ((transform.position + change).y > 0.3054302f && (transform.position + change).y < 4.5f)
        {
            transform.position += change;
        }
    }
}
