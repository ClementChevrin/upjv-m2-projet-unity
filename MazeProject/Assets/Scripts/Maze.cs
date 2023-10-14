using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField]
    private Bloc bloc; //pour appeler la preFab

    System.Random random = new();

    private const int taille = 100; //pour l'instant on change la taille ici

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
                Bloc newBloc = Instantiate(bloc, new Vector3(x, -1, z), Quaternion.identity);
                grille[x, z] = newBloc;
            }
        }

        //On choisit un bloc de départ
        int startX = random.Next(taille);
        int startZ = random.Next(taille);
        //Debug.Log("startX: " + startX);
        //Debug.Log("startZ: " + startZ);

        generationMaze(startX, startZ);
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
    public void generationMaze(int x, int z)
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
                    generationMaze(nextX, nextZ);
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
}
