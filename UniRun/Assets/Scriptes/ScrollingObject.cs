using System;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 15;

    private PlayerController player;

        public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void Update()
    {
        if (player.Islive())
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
    }



}
