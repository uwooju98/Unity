using System;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float width = 20.48f;

    private PlayerController player;

    public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void Update()
    {
         if (player.Islive())
        {
            if (transform.position.x <= -width)
            {
            transform.position += new Vector3(width * 2f, 0f, 0f);
            }
        }

    }
}
