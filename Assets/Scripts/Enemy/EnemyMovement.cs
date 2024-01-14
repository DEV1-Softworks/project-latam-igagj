using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private void Update()
    {
        // TODO: Readability
        transform.Translate(movementSpeed * Time.deltaTime * (new Vector3(GetAnyPlayerXPos(), transform.position.y) - transform.position).normalized);
    }

    // TODO: Get closest player!
    private float GetAnyPlayerXPos() 
    {
        return FindAnyObjectByType<PlayerMovement_2>().transform.position.x; // Inefficient!
    }
}
