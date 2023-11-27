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

    // Paramètres pour l'effet de mouvement de la tête
    private float bobbingSpeed = 0.01f;
    private float bobbingAmount = 0.8f;
    private float timer = 0.0f;

    // Différents sons
    public AudioClip collectKeySound;
    public AudioClip footstepsSound1;
    public AudioClip footstepsSound2;
    public bool footstepFirst = true;
    private float timeSinceLastFootstep = 0f;
    public float footstepDelay = 0.5f;

    private AudioSource audioSource;
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

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Si l'AudioSource n'est pas déjà attaché, ajoutez-le
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        mazeUiManager = FindObjectOfType<MazeUIManager>();
        //MazeUIManager mazeUiManager = FindObjectOfType<MazeUIManager>();
        mazeUiManager.UpdateKeyCount(keysCollected, totalKeys);
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

        // Effet de mouvement de la tête
        HeadBobbing();
    }

    void HeadBobbing()
    {
        if (Mathf.Abs(characterController.velocity.x) > 0.1f || Mathf.Abs(characterController.velocity.z) > 0.1f)
        {
            // Si le joueur est en mouvement
            float waveSlice = Mathf.Sin(timer);
            timer += bobbingSpeed;

            if (timer > Mathf.PI * 2)
            {
                timer = (timer + bobbingSpeed) % (Mathf.PI * 2);
            }

            if (waveSlice != 0)
            {
                float translateChange = waveSlice * bobbingAmount;
                float totalAxes = Mathf.Abs(characterController.velocity.x) + Mathf.Abs(characterController.velocity.z);
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                translateChange = totalAxes * translateChange;

                float newY = 0.6f + translateChange;

                // Appliquer le mouvement de la tête au GameObject du joueur
                transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
            }
            if (Time.time - timeSinceLastFootstep > footstepDelay)
            {
                footstepSound();
                timeSinceLastFootstep = Time.time;
            }

        }
        else
        {
            // Si le joueur est immobile, ramener la tête à la position d'origine
            transform.localPosition = new Vector3(transform.localPosition.x, 0.6f, transform.localPosition.z);
            timer = 0.0f;
        }
    }

    private void footstepSound() {
        if (footstepFirst)
        {
            audioSource.PlayOneShot(footstepsSound1);
            footstepFirst = false;
        }
        else
        {
            audioSource.PlayOneShot(footstepsSound2);
            footstepFirst = true;
        }
    }
    public void CollectKey()
    {
        keysCollected += 1;
        mazeUiManager.UpdateKeyCount(keysCollected, totalKeys);
        if (collectKeySound != null)
        {
            audioSource.PlayOneShot(collectKeySound);
        }
        //Si le joueur a toute les cl�s on met � jour l'UI et on active la porte de sortie
        if (keysCollected == totalKeys)
        {
            GameObject[] sas = GameObject.FindGameObjectsWithTag("SAS");
            sas[1].GetComponentInChildren<Porte>().GetComponent<Collider>().isTrigger = true;
            mazeUiManager.UpdateObjective();
        }
    }
}
