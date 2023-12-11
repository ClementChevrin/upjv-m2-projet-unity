using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeUIManager : MonoBehaviour
{
    [SerializeField]
    private Text keyCountText;

    [SerializeField]
    private Text objective;

    [SerializeField]
    private Text center;

    [SerializeField]
    private RawImage selectedImage;

    [SerializeField]
    public AudioClip winSound;

    private AudioSource audioSource;
    private int selectedItem = 1;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Si l'AudioSource n'est pas déjà attaché, ajoutez-le
            audioSource = gameObject.AddComponent<AudioSource>();
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateKeyCount(int keysCollected, int totalKeys)
    {
        if (keyCountText != null)
        {
            keyCountText.text = "Cl�s obtenues : " + keysCollected + "/" + totalKeys;
        }
    }

    public void UpdateObjective()
    {
        if (objective != null)
        {
            objective.text = "Objectif : Trouvez la sortie !";
        }
    }

    public void ShowWin()
    {
        center.gameObject.SetActive(true);
        StartCoroutine(ClearWinAndQuit());

    }

    private IEnumerator ClearWinAndQuit()
    {
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(winSound);
        center.gameObject.SetActive(false);
        SceneManager.LoadScene("MenuScene");
    }

    public int getItem() {
        return selectedItem;
    }
    public void setItem(int item) {
        if (item != selectedItem) {
            selectedItem = item;
            moveSelectedItem();
        }
    }
    public void moveSelectedItem() {
        Vector3 newVector = new Vector3(0, selectedImage.rectTransform.localPosition.y, selectedImage.rectTransform.localPosition.z);
        switch (selectedItem) {
            case 1:
                newVector.x = -69.2f; 
                break;
            case 2:
                newVector.x = 0f;
                break;
            case 3:
                newVector.x = 68.8f;
                break;
        }
        selectedImage.rectTransform.localPosition = newVector;
    }
    public void ChangeItem(int scroll) {
        selectedItem += scroll;
        if (selectedItem > 3) {
            selectedItem = 1;
        }
        if (selectedItem < 1) {
            selectedItem = 3;
        }
        moveSelectedItem();
    }

    public void collectItem(GameObject item) {
        
    }
}
