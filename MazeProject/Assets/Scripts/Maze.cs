using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField]
    private Bloc bloc;

    System.Random random = new();

    private int taille = 25;

    private Bloc[,] grille;

    // Start is called before the first frame update
    void Start()
    {
        grille = new Bloc[taille, taille];
        for (int x = 0; x < taille; ++x)
        {
            for (int z = 0; z < taille; ++z)
            {
                Bloc newBloc = Instantiate(bloc, new Vector3(x, -1, z), Quaternion.identity);
                grille[x, z] = newBloc;
            }
        }
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
                case 0: nextZ = z + 1; //Debug.Log("Ouest z + 1: " + nextX);
                        break; // Ouest
                case 1: nextX = x - 1; //Debug.Log("Sud x - 1: " + nextX);
                        break; // Sud
                case 2: nextZ = z - 1; //Debug.Log("Est z - 1: " + nextX);
                        break; // Est
                case 3: nextX = x + 1; //Debug.Log("Nord x + 1: " + nextX);
                        break; // Nord
            }

            //Debug.Log("nextX: " + nextX);
            //Debug.Log("nextZ: " + nextZ);
            // Vérifiez si la nouvelle position est valide
            if (nextX >= 0 && nextX < taille && nextZ >= 0 && nextZ < taille)
            {
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
                grille[x, z].RemoveMurGauche();
                grille[nextX, nextZ].RemoveMurDroite();
                break;
            case 1:
                grille[x, z].RemoveMurBas();
                grille[nextX, nextZ].RemoveMurHaut();
                break;
            case 2:
                grille[x, z].RemoveMurDroite();
                grille[nextX, nextZ].RemoveMurGauche();
                break;
            case 3:
                grille[x, z].RemoveMurHaut();
                grille[nextX, nextZ].RemoveMurBas();
                break;
        }
    }
}
