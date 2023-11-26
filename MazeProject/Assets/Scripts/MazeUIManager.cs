using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeUIManager : MonoBehaviour
{
    [SerializeField]
    private Text keyCountText;

    [SerializeField]
    private Text objective;

    [SerializeField]
    private Text center;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateKeyCount(int keysCollected, int totalKeys)
    {
        if (keyCountText != null)
        {
            keyCountText.text = "Clés obtenues : " + keysCollected + "/" + totalKeys;
        }
    }

    public void UpdateObjective()
    {
        if (objective != null)
        {
            objective.text = "Objectif : Trouvez la sortie !";
        }
    }

    public void ShowCenterMessage(string message)
    {
        center.text = message;
        StartCoroutine(ClearCenterMessage());
    }

    private IEnumerator ClearCenterMessage()
    {
        yield return new WaitForSeconds(2f);
        center.text = "";
    }
}
