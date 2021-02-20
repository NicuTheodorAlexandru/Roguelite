using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBridge : MonoBehaviour
{
    public MonoBehaviour listener;
    // Start is called before the first frame update
    public void Initalize(MonoBehaviour script)
    {
        listener = script;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!gameObject.activeSelf)
            return;
        listener.SendMessage("OnTriggerStay2D", collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeSelf)
            return;
        listener.SendMessage("OnTriggerEnter2D", collision);
    }
}
