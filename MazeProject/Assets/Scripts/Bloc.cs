using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{
    [SerializeField]
    private GameObject interieur;

    [SerializeField]
    private GameObject murNord;

    [SerializeField]
    private GameObject murSud;

    [SerializeField]
    private GameObject murOuest;

    [SerializeField]
    private GameObject murEst;

    private bool estExplore = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Est appelé quand on explore un bloc, on rend invisible l'intérieur du bloc
    public void Explore()
    {
        interieur.SetActive(false);
        estExplore = true;
    }

    public bool getExplore()
    {
        return estExplore;
    }

    // Cacher un des 4 murs
    public void RemoveMurNord()
    {
        murNord.SetActive(false);
    }

    public void RemoveMurSud()
    {
        murSud.SetActive(false);
    }

    public void RemoveMurOuest()
    {
        murOuest.SetActive(false);
    }

    public void RemoveMurEst()
    {
        murEst.SetActive(false);
    }
}
