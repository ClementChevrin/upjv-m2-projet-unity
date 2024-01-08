using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

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

    [SerializeField]
    public AudioClip marteauSound;

    [SerializeField]
    public AudioClip murSound;

    private AudioSource audioSource;
    private int selectedItem = 1;

    private Transform canvasTransform;

    [SerializeField]
    private GameObject marteauImg;

    [SerializeField]
    private GameObject marteauNb;

    [SerializeField]
    public GameObject wallBreak;

    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            canvasTransform = canvas.GetComponent<Transform>();
        }
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
            keyCountText.text = "Cles obtenues : " + keysCollected + "/" + totalKeys;
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

    public void collectItem(GameObject item)
    {
        if (item.GetComponent<Hammer>() != null)
        {
            marteauImg.SetActive(true);
            marteauNb.SetActive(true);
            int nb = int.Parse(marteauNb.GetComponent<Text>().text);
            nb++;
            marteauNb.GetComponent<Text>().text = nb.ToString();
        }
    }

    public void useItem(GameObject hitObject)
    {
        if (selectedItem == 1)
        {
            audioSource.PlayOneShot(marteauSound);
            int nb = int.Parse(marteauNb.GetComponent<Text>().text);
            if (nb > 0 && System.Array.IndexOf(Bloc.murNames, hitObject.name) > -1)
            {
                // Détruisez l'ancien objet
                hitObject.SetActive(false);

                // Récupèration de la position de la caméra
                Vector3 cameraPosition = Camera.main.transform.position;

                // Récupèration de la direction de la caméra
                Vector3 cameraDir = Camera.main.transform.forward;

                // Récupèration des informations sur l'objet touché
                RaycastHit hit;

                // Longueur maximale du rayon lancé
                float maxRaycastDistance = 1.5f;

                // Lance un rayon depuis la position de la caméra dans la direction de la caméra
                if (Physics.Raycast(cameraPosition, cameraDir, out hit, maxRaycastDistance)) {
                    hit.collider.gameObject.SetActive(false);
                } else {
                    hitObject.SetActive(true);
                    return;
                }
                audioSource.PlayOneShot(murSound);
                nb--;
                marteauNb.GetComponent<Text>().text = nb.ToString();
                if (nb == 0) {
                    marteauImg.SetActive(false);
                    marteauNb.SetActive(false);
                }
            }
        }
    }
}
