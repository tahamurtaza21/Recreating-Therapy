using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Umbrella : MonoBehaviour
{

    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] Transform player;

    public float height = 1f;
    public float distance = 2f;

    private Vector3 offsetX;
    private Vector3 offsetY;

    private float offset = 5f;

    void Start() 
    {
        offsetX = new Vector3(0,height,distance);
        offsetY = new Vector3(0,0,distance);    
    }

    void Update()
    {
        RotateUmbrella();
    }

    /* void RotateUmbrella()
    {
        Debug.Log("Rotating");
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    } */

    void RotateUmbrella()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        offsetX = Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * rotateSpeed, Vector3.up) * offsetX;
        offsetY = Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * rotateSpeed, Vector3.right) * offsetY;


        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);
        transform.position = player.position + offsetX; 
        transform.LookAt(player.position); 
    }
}
