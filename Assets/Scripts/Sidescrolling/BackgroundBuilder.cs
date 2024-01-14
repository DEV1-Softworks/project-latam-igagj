using System.Collections;
using UnityEngine;

public class BackgroundBuilder : MonoBehaviour
{
    [HideInInspector] public GameObject[] backgroundObjects;

    [SerializeField] private Sprite[] clouds;
    [SerializeField] private Sprite[] buildings;
    [SerializeField] private Sprite[] landscapes;
    [SerializeField] private Transform[] skyArea = new Transform[2];
    [SerializeField] private Transform[] groundArea = new Transform[2];

    private LayerMask backgroundLayer;
    private int spritesToGenerate;

    private void Awake()
    {
        backgroundLayer = LayerMask.NameToLayer("Background");
    }

    private void Start()
    {
        GenerateRandomBackground(Vector3.left * 40f);
        StartCoroutine(GenerateBackgroundRecursive());
    }

    private IEnumerator GenerateBackgroundRecursive()
    {
        while (true)
        {
            GenerateRandomBackground(Vector3.zero);
            yield return new WaitForSeconds(7f);
        }
    }

    public void GenerateRandomBackground(Vector3 offset)
    {
        GenerateSprites(clouds, skyArea, offset, 20);
        GenerateSprites(buildings, groundArea, offset, 7);
        GenerateSprites(landscapes, groundArea, offset, 5);
    }

    private void GenerateSprites(Sprite[] sprites, Transform[] area, Vector2 offset, int maxSprites)
    {
        spritesToGenerate = Random.Range(1, maxSprites);

        GameObject parentObject = new($"{sprites[0].name}")
        {
            layer = backgroundLayer
        };

        parentObject.transform.position = area[0].position + Vector3.left * 10f;

        for (int i = 0; i < spritesToGenerate; i++)
        {
            Sprite randomSprite = sprites[Random.Range(0, sprites.Length)];

            float randomPosX = Random.Range(area[0].position.x, area[1].position.x);
            float randomPosY = Random.Range(area[0].position.y, area[1].position.y);
            Vector2 randomPosition = new Vector2(randomPosX, randomPosY) + offset;

            GameObject newObject = new($"{sprites[0].name} ({i})");

            Collider2D newObjectCollider = newObject.AddComponent<BoxCollider2D>();
            newObjectCollider.isTrigger = true;

            float randomScaleMagnitude = Random.value;
            Vector3 randomScale = new(randomScaleMagnitude, randomScaleMagnitude, 1f);
            newObject.transform.localScale = randomScale;
            newObject.transform.position = randomPosition;
            newObject.transform.parent = parentObject.transform;

            ParallaxController parallax = newObject.AddComponent<ParallaxController>();
            parallax.distance = 1f / randomScaleMagnitude;
            parallax.distance = Mathf.Clamp(parallax.distance, 0f, 3f);

            SpriteRenderer newRenderer = newObject.AddComponent<SpriteRenderer>();
            newRenderer.sprite = randomSprite;
            newRenderer.sortingOrder = -1 * Mathf.RoundToInt(parallax.distance) - 1;
        }
    }


}
