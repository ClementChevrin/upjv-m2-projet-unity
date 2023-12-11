using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (expectedObject != null && other.CompareTag(playerTag))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                if (playerScript.collectItem(gameObject)) {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
