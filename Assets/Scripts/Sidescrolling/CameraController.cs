using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float speed;

    private void LateUpdate()
    {
        transform.position += speed * Time.deltaTime * transform.right;
    }
}
