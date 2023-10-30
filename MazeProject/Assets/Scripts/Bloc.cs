using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{

    [SerializeField]
    private GameObject murNord;

    [SerializeField]
    private GameObject murSud;

    [SerializeField]
    private GameObject murOuest;

    [SerializeField]
    private GameObject murEst;
    
    [SerializeField]
    private GameObject pyloneNordOuest;

    [SerializeField]
    private GameObject pyloneNordEst;
    
    [SerializeField]
    private GameObject pyloneSudEst;

    [SerializeField]
    private GameObject pyloneSudOuest;


    // Ne sert a rien aussi du coup
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

    public void setCorner(bool nordOuest,bool nordEst,bool sudOuest,bool sudEst)
    {
        pyloneNordEst.SetActive(nordEst);
        pyloneNordOuest.SetActive(nordOuest);
        pyloneSudEst.SetActive(sudEst);
        pyloneSudOuest.SetActive(sudOuest);
    }

    public void setWall(bool nord,bool est,bool sud,bool ouest)
    {
        murEst.SetActive(est);
        murOuest.SetActive(ouest);
        murNord.SetActive(nord);
        murSud.SetActive(sud);
    }
}
