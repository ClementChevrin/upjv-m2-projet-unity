using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField]
    private Bloc bloc; //pour appeler la preFab bloc

    [SerializeField]
    private Torch torch; //pour appeler la preFab torch

    [SerializeField]
    private Key key; //pour appeler la preFab key

    [SerializeField]
    private SAS sas;

    [SerializeField]
    public Player joueur;

    private Bloc entree;

    private int xEntree;

    private Bloc sortie;

    private int xSortie;

    System.Random random = new();

    private const int taille = 20; //pour l'instant on change la taille ici

    private const int numberOfKeys = 3; //pour l'instant on définit le nombre de clés ici, à param. dans le menu plus tard ?

    private Bloc[,] grille;


    // Start is called before the first frame update
    void Start()
    {
        //On crée la grille et on la remplie de la prefab Bloc
        grille = new Bloc[taille, taille];
        for (int x = 0; x < taille; ++x)
        {
            for (int z = 0; z < taille; ++z)
            {
                Bloc newBloc = Instantiate(bloc, new Vector3(x*2, (float)-1.6, z*2), Quaternion.identity);
                grille[x, z] = newBloc;
                grille[x, z].transform.SetParent(this.transform);
            }
        }

        //On choisit un bloc de départ
        int startX = random.Next(taille);
        int startZ = random.Next(taille);
        //Debug.Log("startX: " + startX);
        //Debug.Log("startZ: " + startZ);

        generationMaze(startX, startZ, 0);
        addEntreeSortie();
        addKeys();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Méthode récursive, permet de parcourir la grille en profondeur
     * Dans un bloc qu'on explore, on regarde si l'un des 4 blocs adjacents est libre
     * Si oui, on l'explore à son tour
     * Sinon, on revient au bloc d'avant et on cherche une autre direction
     * Au final, on sera revenu au premier bloc avec tous les blocs d'exploré
     */
    public void generationMaze(int x, int z, int cpt)
    {
        grille[x, z].Explore();
        int[] directions = { 0, 1, 2, 3 };
        ShuffleArray(directions); // On mélange les directions aux hasard

        foreach (int direction in directions)
        {
            int nextX = x;
            int nextZ = z;

            switch (direction)
            {
                case 0: nextZ = z + 1; //Debug.Log("Nord z + 1: " + nextZ);
                        break; // Nord
                case 1: nextX = x - 1; //Debug.Log("Ouest x - 1: " + nextX);
                        break; // Ouest
                case 2: nextZ = z - 1; //Debug.Log("Sud z - 1: " + nextZ);
                        break; // Sud
                case 3: nextX = x + 1; //Debug.Log("Est x + 1: " + nextX);
                        break; // Est
            }
            //Debug.Log("nextX: " + nextX);
            //Debug.Log("nextZ: " + nextZ);

            // Si on est bien dans la grille
            if (nextX >= 0 && nextX < taille && nextZ >= 0 && nextZ < taille)
            {
                // Si le bloc suivant n'a pas encore été exploré
                if(!grille[nextX, nextZ].getExplore())
                {
                    hideWalls(direction, x, nextX, z, nextZ);
                    generationMaze(nextX, nextZ, cpt+1);
                }
            }
        }

        //Ajout des torches (RESTE A ASSIGNER LES TORCHES AUX MURS (LIGNES EN PARENTHESES), PROBLEME : TORCHES CHANGENT DE FORMES)
        Torch newTorch = null;
        bool isTorched = false;
        if (cpt % 8 == 0)
        {
            foreach (int direction in directions)
            {
                switch (direction)
                {
                    case 0:
                        if (grille[x, z].murNordIsActive()){
                            newTorch = Instantiate(torch, new Vector3((x * 2) - (float)0.5, (float)1.533, (z * 2)+(float)0.8), Quaternion.Euler(-45, 0, 0));
                            isTorched = true;
                        }
                        break; // Nord
                    case 1:
                        if (grille[x, z].murOuestIsActive()){
                            newTorch = Instantiate(torch, new Vector3((x * 2) - (float)1.3, (float)1.533, (z * 2)), Quaternion.Euler(0, 0, -45));
                            isTorched = true;
                        }
                        break; // Ouest
                    case 2:
                        if (grille[x, z].murSudIsActive()){
                            newTorch = Instantiate(torch, new Vector3((x * 2) - (float)0.5, (float)1.533, (z * 2)-(float)0.8), Quaternion.Euler(45, 0, 0));
                            isTorched = true;
                        }
                        break; // Sud
                    case 3:
                        if (grille[x, z].murEstIsActive()){
                            newTorch = Instantiate(torch, new Vector3((x * 2) + (float)0.3, (float)1.533, (z * 2)), Quaternion.Euler(0, 0, 45));
                            isTorched = true;
                        }
                        break; // Est
                }
                if (isTorched)
                {
                    newTorch.transform.SetParent(grille[x, z].transform);
                    break;
                }
            }
        }
    }

    
    //Mélange au hasard les éléments un tableau d'entiers
    public void ShuffleArray(int[] array)
    {
        for (int i = 0; i < 4; i++)
        {
            int j = i + random.Next(4 - i);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    //Cacher deux murs (celui du bloc d'avant et l'actuel)
    public void hideWalls(int direction, int x, int nextX, int z, int nextZ)
    {
        switch (direction)
        {
            case 0:
                grille[x, z].RemoveMurNord();
                grille[nextX, nextZ].RemoveMurSud();
                break;
            case 1:
                grille[x, z].RemoveMurOuest();
                grille[nextX, nextZ].RemoveMurEst();
                break;
            case 2:
                grille[x, z].RemoveMurSud();
                grille[nextX, nextZ].RemoveMurNord();
                break;
            case 3:
                grille[x, z].RemoveMurEst();
                grille[nextX, nextZ].RemoveMurOuest();
                break;
        }
    }

    //Création et placement d'un bloc avec spécification des murs souhaités
    public void blocWithSpecificWalls(int x, int z, bool nord, bool est, bool sud, bool ouest)
    {
        Bloc instBloc = Instantiate(bloc, new Vector3(x, -1, z), Quaternion.identity);
        instBloc.setWall(nord, est, sud, ouest);
        instBloc.setCorner(false, false, false, false);
    }


    //Ajouter une entrée et une sortie
    public void addEntreeSortie()
    {
        // Placement de l'entrée
        xEntree = random.Next(1,taille - 2);
        entree = grille[xEntree, 0];
        entree.RemoveMurSud();

        // Placement de la sortie
        xSortie= random.Next(1,taille - 2);
        sortie = grille[xSortie, taille - 1];
        sortie.RemoveMurNord();

        //Création des SAS d'entrée et de sortie
        SAS instEntreeSAS = Instantiate(sas, new Vector3((xEntree*2)+2, (float)-1.6, -4), Quaternion.identity); //Entrée
        SAS instSortieSAS = Instantiate(sas, new Vector3((xSortie*2)-3, (float)-1.6, (taille*2)+2), Quaternion.Euler(0, 180, 0)); //Sortie

        //Association des SAS au maze
        instEntreeSAS.transform.SetParent(this.transform);
        instSortieSAS.transform.SetParent(this.transform);

        //On recherche si des torches existent à l'entrée et sortie, si oui on les rend invisibles.
        Torch torcheE = entree.GetComponentInChildren<Torch>();
        Torch torcheS = sortie.GetComponentInChildren<Torch>();
        if (torcheE != null) torcheE.gameObject.SetActive(false);
        if (torcheS != null) torcheS.gameObject.SetActive(false);

        // Placement du joueur à l'entrée
        if (joueur != null) {
            joueur.PlacePlayer(new Vector3((xEntree*2)-(float)0.5, (float)0.6, -4));
        }
    }

    public void addKeys()
    {
        int keyX, keyZ;
        for (int i = 0; i < numberOfKeys; i++)
        {
            keyX = random.Next(0, taille - 1);
            keyZ = random.Next(0, taille - 1);
            Key instKey = Instantiate(key, new Vector3((keyX * 2)- (float)0.5, (float)0.4, (keyZ * 2)), Quaternion.identity);
            instKey.transform.SetParent(grille[keyX,keyZ].transform);
        }
    }
}
