using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 2.0f;

    [SerializeField]
    private CharacterController characterController;

    private int keysCollected;

    private int totalKeys;

    public MazeUIManager mazeUiManager;

    // Start is called before the first frame update
    void Start()
    {
        if(characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }

        if (transform.parent != null)
        {
            Maze maze = transform.parent.GetComponent<Maze>();
            if (maze != null)
            {
                totalKeys = maze.getNumberOfKeys();
                keysCollected = 0;
            }
        }
        mazeUiManager = FindObjectOfType<MazeUIManager>();
        //MazeUIManager mazeUiManager = FindObjectOfType<MazeUIManager>();
        mazeUiManager.UpdateKeyCount(keysCollected, totalKeys);
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
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement += cameraForward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement -= cameraForward;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement -= Camera.main.transform.right;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement += Camera.main.transform.right;
        }

        // Normalize the movement vector to avoid faster diagonal movement
        movement.Normalize();

        // Apply the movement to the character controller
        characterController.Move(movement * moveSpeed * Time.deltaTime);


    }

    public void CollectKey()
    {
        keysCollected += 1;
        mazeUiManager.UpdateKeyCount(keysCollected, totalKeys);
        if (keysCollected == totalKeys)
        {
            mazeUiManager.UpdateObjective();
        }
    }
}
