using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerCharacter playerCharacter; // Add this at the top

    public float health = 100;
    
    void Start()
{
    StartCoroutine(Init());
}

IEnumerator Init()
{
    yield return PlayerLocator.WaitForPlayer();
    playerCharacter = PlayerLocator.Instance;
}

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Enemy " + gameObject.name + " took damage. Remaining HP: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
        if (playerCharacter != null)
        playerCharacter.Experience += 25f;
    }
}
