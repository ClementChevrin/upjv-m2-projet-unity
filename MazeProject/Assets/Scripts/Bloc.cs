using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{
    // [SerializeField]
    // private GameObject sol;

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

    [SerializeField]
    private GameObject murCasseNord;

    [SerializeField]
    private GameObject murCasseSud;

    [SerializeField]
    private GameObject murCasseOuest;

    [SerializeField]
    private GameObject murCasseEst;
    public static readonly string[] murNames = { "MurNord", "MurSud", "MurEst", "MurOuest" };

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

    public GameObject getMurNord()
    {
        return murNord;
    }

    public GameObject getMurSud()
    {
        return murSud;
    }

    public GameObject getMurOuest()
    {
        return murOuest;
    }

    public GameObject getMurEst()
    {
        return murEst;
    }

    // Est appel� quand on explore un bloc, on rend invisible l'int�rieur du bloc
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
        murCasseNord.SetActive(false);
    }

    public void RemoveMurSud()
    {
        murSud.SetActive(false);
        murCasseSud.SetActive(false);
    }

    public void RemoveMurOuest()
    {
        murOuest.SetActive(false);
        murCasseOuest.SetActive(false);
    }

    public void RemoveMurEst()
    {
        murEst.SetActive(false);
        murCasseEst.SetActive(false);
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

    public bool murNordIsActive()
    {
        return murNord.activeSelf;
    }

    public bool murSudIsActive()
    {
        return murSud.activeSelf;
    }

    public bool murOuestIsActive()
    {
        return murOuest.activeSelf;
    }

    public bool murEstIsActive()
    {
        return murEst.activeSelf;
    }
    
    public int numberWallsActive()
    {
        int activeWallCount = 0;

        if (murNordIsActive()) activeWallCount++;
        if (murSudIsActive()) activeWallCount++;
        if (murOuestIsActive()) activeWallCount++;
        if (murEstIsActive()) activeWallCount++;

        return activeWallCount;
    }

    public string corridor()
    {
        if(numberWallsActive() == 2 && ((!murNordIsActive() && !murSudIsActive())^(!murOuestIsActive() && !murEstIsActive())))
        {
            if(!murNordIsActive() && !murSudIsActive()) return "NS";
            else return "OE";
        }
        return null;
    }
}
