using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
    }
}
