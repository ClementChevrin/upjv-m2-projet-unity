using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{
    [SerializeField]
    private GameObject interieur;

    [SerializeField]
    private GameObject murHaut;

    [SerializeField]
    private GameObject murBas;

    [SerializeField]
    private GameObject murGauche;

    [SerializeField]
    private GameObject murDroite;

    private bool estExplore = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explore()
    {
        interieur.SetActive(false);
        estExplore = true;
    }

    public bool getExplore()
    {
        return estExplore;
    }

    public void RemoveMurHaut()
    {
        murHaut.SetActive(false);
    }

    public void RemoveMurBas()
    {
        murBas.SetActive(false);
    }

    public void RemoveMurGauche()
    {
        murGauche.SetActive(false);
    }

    public void RemoveMurDroite()
    {
        murDroite.SetActive(false);
    }
}
