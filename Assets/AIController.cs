using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private Vector3 randTargetLocation;
    private int dimension;
    private NavMeshAgent agent;
    private int multiplier = 6;

    public int distanceThreshold = 30;
    public GameObject player;
    private Vector3 targLocation;

    bool reachedLoc = false;
    void Start()
    {
        player = GameObject.Find("Player");
        dimension = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>()._mazeWidth - 1;
        targLocation = GenerateRandLocation();
        agent = GetComponent<NavMeshAgent>();
        
    }

    //ToDo: update function that checks whether the enemy has went to the random location
    private void Update()
    {
        agent.SetDestination(targLocation);

        if (transform.position == targLocation)
        {
            //choose nearest wall and break it
            if (reachedLoc == false)
            {
                targLocation = GenerateRandLocation();
                reachedLoc = true;
            }
        }
        
        if(Vector3.Distance(transform.position, player.transform.position) < distanceThreshold)
        {
            Debug.Log("Start Chasing!");
            targLocation = player.transform.position;
        }
        else
        {
            //choose nearest wall and break it
            if (reachedLoc == true)
            {
                targLocation = GenerateRandLocation();
                agent.SetDestination(targLocation);
                reachedLoc = false;
            }
        }
    }

    private Vector3 GenerateRandLocation()
    {
        randTargetLocation = new Vector3(Random.Range(0, dimension), 0, Random.Range(0, dimension));
        randTargetLocation *= multiplier;

        return randTargetLocation;
    }
}
