using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FragmentsSpawn : MonoBehaviour
{
    public GameObject fragmentPrefab;
    private List<Vector3> avoidPositions;
    private List<Vector3> fragmentPositions;
    private int dimension;
    private int numFragments = 5;
    void Start()
    {
        //creating a list of vectors for positions to avoid when spawning fragments (i.e. not where enemies, portals, or the player is)
        dimension = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>()._mazeWidth - 1;
        avoidPositions = new List<Vector3>();
        fragmentPositions = new List<Vector3>();
        avoidPositions.Add(GameObject.Find("Player").transform.position);
        for(int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            avoidPositions.Add(GameObject.FindGameObjectsWithTag("Enemy")[i].transform.position);
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Portal").Length; i++)
        {
            avoidPositions.Add(GameObject.FindGameObjectsWithTag("Portal")[i].transform.position);
        }

        //generate 5 points not overlapping with each other
        while(fragmentPositions.Count == fragmentPositions.Distinct().Count() && fragmentPositions.Count != numFragments)
        {
            fragmentPositions.Add(GenerateFragmentLocations());
        }
        
        //check for distance in fragments and update list???

        //instantiate fragments at their stored locations
        for(int i = 0; i < fragmentPositions.Count; i++)
        {
            Instantiate(fragmentPrefab, fragmentPositions[i], Quaternion.identity, transform);
        }

    }

    //generating points not overlapping with portals, player, and enemies
    private Vector3 GenerateFragmentLocations()
    {
        Vector3 randPosition = new Vector3(Random.Range(0, dimension), 0, Random.Range(0, dimension));

        for(int i = 0; i < avoidPositions.Count; i++)
        {
            if(randPosition == avoidPositions[i])
            {
                GenerateFragmentLocations();
            }
        }

        return randPosition;
    }
}
