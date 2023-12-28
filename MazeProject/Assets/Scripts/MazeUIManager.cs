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

    private Transform canvasTransform;
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

    public void collectItem(GameObject item, int position)
    {
        // Récupération du Renderer associé à l'item
        Renderer itemRenderer = item.GetComponent<Renderer>();

        if (itemRenderer != null)
        {
            // Récupération de la texture du Renderer
            Texture itemTexture = itemRenderer.material.mainTexture;

            // Mettez en place la logique pour déterminer la position de l'image
            Vector3 targetPosition = CalculateTargetPosition(position);

            // Créer un GameObject pour l'image
            GameObject imageObject = new GameObject("ItemImage");
            RectTransform rectTransform = imageObject.AddComponent<RectTransform>();
            RawImage rawImage = imageObject.AddComponent<RawImage>();
            rawImage.texture = itemTexture;

            // Placez l'image à la position cible
            rectTransform.localPosition = targetPosition;
            rectTransform.SetParent(canvasTransform); // Assurez-vous d'avoir une référence à votre Canvas

            // Ajoutez d'autres propriétés ou composants au besoin
        }
    }

    // Fonction pour calculer la position cible en fonction de la position donnée
    private Vector3 CalculateTargetPosition(int position)
    {
        // Ajoutez votre logique pour déterminer la position en fonction de la valeur 'position'
        // Par exemple, utilisez un switch pour des positions spécifiques
        switch (position)
        {
            case 1:
                return new Vector3(-69.2f, 0f, 0f);
            case 2:
                return new Vector3(0f, 0f, 0f);
            case 3:
                return new Vector3(68.8f, 0f, 0f);
            default:
                return Vector3.zero;
        }
    }

}
