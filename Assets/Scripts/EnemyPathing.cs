using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //config params
    WaveConfig waveConfig;
    List<Transform> waypoints = new List<Transform>();
    
    int waypointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        //if it is not the last waypoint
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.MoveSpeed * Time.deltaTime;

            //move the gameobject to the target position at a speed
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            //if it has reached, increase the waypoint index
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
