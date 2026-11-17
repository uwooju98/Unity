using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public GameObject[] sprites;
    private int isOnSpriteIndex = 0;

    public float speed = 5f;
    public float spriteInterval = 0.15f;
    private float spriteTimer = 0f;

    public IEnumerator MovePath(List<Vector3> positions)
    {
        foreach (var target in positions)
        {
            yield return StartCoroutine(Move(target));
        }
    }

    public IEnumerator Move(Vector3 target)
    {
        while (transform.position != target)
        {
            spriteTimer += Time.deltaTime;

            if (spriteTimer >= spriteInterval)
            {
                ChangeSprite();
                spriteTimer = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }     
    }


    public void ChangeSprite()
    {
        sprites[isOnSpriteIndex].SetActive(false);

        isOnSpriteIndex = (isOnSpriteIndex + 1) % sprites.Length;

        sprites[isOnSpriteIndex].SetActive(true);
    }
}
