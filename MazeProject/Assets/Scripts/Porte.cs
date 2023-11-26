using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porte : MonoBehaviour
{
    private MazeUIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<MazeUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Porte");
        if (uiManager != null && other.CompareTag("Player"))
        {
            uiManager.ShowCenterMessage("Gagné !");
            SceneManager.LoadScene("MenuScene");
        }
    }
}
