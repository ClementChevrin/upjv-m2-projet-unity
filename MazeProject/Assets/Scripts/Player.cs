using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Direction de la caméra
        Vector3 cameraForward = Camera.main.transform.forward;

        // Suppression du Y pour rester à l'horizontal
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // Player movement
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += cameraForward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= cameraForward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= Camera.main.transform.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Camera.main.transform.right * moveSpeed * Time.deltaTime;
        }

        
    }

    public void PlacePlayer(Vector3 position)
    {
        transform.position = position;
    }
}
