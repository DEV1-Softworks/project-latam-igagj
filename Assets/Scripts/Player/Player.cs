using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        Destroy(gameObject);
    }

    public enum Number
    {
        One,
        Two
    }
    public Number number;

    public void DestroyComponentsExcept(Component component)
    {
        Component[] components = GetComponents<Component>();
        foreach (Component comp in components)
        {
            if (comp != component && comp != transform && comp != this)
                Destroy(comp);
        }
    }
}