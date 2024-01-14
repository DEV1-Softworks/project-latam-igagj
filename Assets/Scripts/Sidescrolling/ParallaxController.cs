using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [HideInInspector] public float distance;

    private void Update()
    {
        transform.position += Time.deltaTime * distance * Vector3.right;
    }
}
