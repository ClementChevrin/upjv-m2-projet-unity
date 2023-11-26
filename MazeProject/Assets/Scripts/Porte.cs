using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //Debug.Log("Porte");
        if (uiManager != null && other.CompareTag("Player"))
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            uiManager.ShowWin();
        }
    }
}
