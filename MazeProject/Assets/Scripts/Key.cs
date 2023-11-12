using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private GameObject expectedObject = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTrigger(Collider other)
    {
        if (expectedObject != null)
        {
            if (other.gameObject == expectedObject)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
