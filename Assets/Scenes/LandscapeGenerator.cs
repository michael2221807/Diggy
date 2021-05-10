using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LandscapeGenerator : MonoBehaviour
{
    public string gameSeed = "Default";

    public bool isUniform = true;

    public int curSeed = 0;

    public int gameSize = 50;

    public float treeScaleMin = 0.2f;

    public float treeScaleMax = 4f;

    public float rockScaleMin = 0.2f;

    public float rockScaleMax = 4f;

    public float grassScaleMin = 0.2f;

    public float grassScaleMax = 4f;

    public GameObject[] groundObjects;

    public GameObject[] rockObjects;

    public int rockCount = 10;

    public GameObject[] treeObjects;

    public int treeCount = 10;

    public GameObject[] grassObjects;

    public int grassCount = 10;





    // Start is called before the first frame update
    void Start()
    {
        InitRandomState();
        GroundGenerator();
        RockGenerator();
        GrassGenerator();
        TreeGenerator();
    }

    public void InitRandomState()
    {
        curSeed = gameSeed.GetHashCode();
        Random.InitState(curSeed);

    }

    public void GroundGenerator()
    {
        Vector3 randPosition = new Vector3(0,0,0);
        Vector3 randRotation = new Vector3(0, Random.Range(0, 360), 0);
        GameObject selectedGround = groundObjects[Random.Range(0, groundObjects.Length)];
        GameObject groundClone = Instantiate(selectedGround, randPosition, Quaternion.Euler(randRotation));

    }

    public void RockGenerator()
    {
        RaycastHit hit;
        for (int i = 0; i < rockCount; i++)
        {
            Vector3 randPosition = new Vector3(Random.Range(-gameSize/2, gameSize/2), 50, Random.Range(-gameSize / 2, gameSize / 2));
            if (Physics.Raycast(randPosition, Vector3.down, out hit))
            {
                GameObject selectedRock = rockObjects[Random.Range(0, rockObjects.Length)];
                selectedRock.transform.localScale = RandomScale(isUniform, rockScaleMin, rockScaleMax);
                GameObject rockClone = Instantiate(selectedRock, hit.point, selectedRock.transform.rotation);
                rockClone.transform.rotation = Quaternion.Euler(Random.Range(0, 20), Random.Range(0, 360), Random.Range(0, 20));
                
            }
        }
    }

    // helper function that generate random scale for GameObject.
    private Vector3 RandomScale(bool isUniform, float min, float max)
    {
        Vector3 randScale = Vector3.one;
        if (isUniform)
        {
            float uniformScale = Random.Range(min, max);
            randScale = new Vector3(uniformScale, uniformScale, uniformScale);
        }
        else
        {
            randScale = new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        }

        return randScale;
    }

    private Vector3 RandomScaleY(float min, float max)
    {
        return new Vector3(1, Random.Range(min, max), 1);
    }

    public void TreeGenerator()
    {
        RaycastHit hit;
        for (int i = 0; i < treeCount; i++)
        {
            Vector3 randPosition = new Vector3(Random.Range(-gameSize / 2, gameSize / 2), 50, Random.Range(-gameSize / 2, gameSize / 2));
            if (Physics.Raycast(randPosition, Vector3.down, out hit))
            {
                GameObject selectedTree = treeObjects[Random.Range(0, treeObjects.Length)];
                selectedTree.transform.localScale = RandomScaleY(treeScaleMin, treeScaleMax);
                GameObject treeClone = Instantiate(selectedTree, hit.point, selectedTree.transform.rotation);
                treeClone.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            }
        }
    }

    public void GrassGenerator()
    {
        RaycastHit hit;
        for (int i = 0; i < grassCount; i++)
        {
            Vector3 randPosition = new Vector3(Random.Range(-gameSize / 2, gameSize / 2), 50, Random.Range(-gameSize / 2, gameSize / 2));
            if (Physics.Raycast(randPosition, Vector3.down, out hit))
            {
                GameObject selectedGrass = grassObjects[Random.Range(0, grassObjects.Length)];
                selectedGrass.transform.localScale = RandomScale(isUniform, grassScaleMin, grassScaleMax);
                GameObject grassClone = Instantiate(selectedGrass, hit.point, selectedGrass.transform.rotation);
                grassClone.transform.rotation = Quaternion.Euler(hit.normal);

            }
        }
    }

}
