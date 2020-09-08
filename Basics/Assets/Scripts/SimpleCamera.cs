using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    [SerializeField] private float moveSpeed = 3;
    private float angleX, angleY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        angleX += mouseX * Time.fixedDeltaTime * sensX;
        angleY += mouseY * Time.fixedDeltaTime * sensY;
        angleY = Mathf.Clamp(angleY, -89f, 89f);

        transform.rotation = Quaternion.Euler(-angleY, angleX, 0);

        transform.position += moveSpeed * Time.deltaTime * 
            (Input.GetAxis("Vertical") * transform.forward +
            Input.GetAxis("Horizontal") * transform.right).normalized;
    }
}
