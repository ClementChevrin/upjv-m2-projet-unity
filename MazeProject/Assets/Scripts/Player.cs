using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 2.0f;

    [SerializeField]
    private CharacterController characterController;

    private int keysCollected;

    private int totalKeys;

    [SerializeField]
    private Text keyCountText;

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
        UpdateKeyCountUI();
    }

    // Update is called once per frame
    void Update()
    {
        // Direction de la cam�ra
        Vector3 cameraForward = Camera.main.transform.forward;

        // Suppression du Y pour rester � l'horizontal
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
        UpdateKeyCountUI();
    }

    private void UpdateKeyCountUI()
    {
        Debug.Log("Cl�s obtenues : " + this.keysCollected + "/" + this.totalKeys);
        if (keyCountText != null)
        {
            keyCountText.text = "Cl�s obtenues : " + keysCollected + "/" + totalKeys;
        }
    }
}
