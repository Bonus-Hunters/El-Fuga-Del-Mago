using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkDIstance();
    }
    // can be trigged for boss following player when close enough
    // or for tiggering a special ability
    void checkDIstance()
    {
        float distanceThreshold = 10.0f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject enemy = GameObject.FindGameObjectWithTag("Bull-Boss");

        // Make sure both exist in the scene
        if (player != null && enemy != null)
        {
            // Calculate distance between them
            float distance = Vector3.Distance(player.transform.position, enemy.transform.position);

            // Check if distance is less than threshold
            if (distance < distanceThreshold)
            {
                print("Player and Enemy are close! Distance: " + distance);
            }
        }
    }
}
