using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Attributs
    public GameObject enemyPrefab;
    [SerializeField] private float switchSpeed = 30f;
    private Material[] materials = new Material[2];
    private Color ogColor;
    private bool switchColor;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = transform.parent.parent.GetComponent<GameManager>();
        materials[0] = GetComponent<MeshRenderer>().material;
        materials[1] = transform.GetChild(0).GetComponent<MeshRenderer>().material;
        ogColor = materials[0].color;
        gameObject.SetActive(false);
    }

    // Lance l'apparition d'un ennemi lorsque l'objet est actif
    void OnEnable()
    {
        StartCoroutine("Spawn");
    }

    // Change la couleur des materiaux pour faire une animation avant l'apparition de l'ennemi
    void Update()
    {
        foreach (Material material in materials)
        {
            if (!switchColor)
            {
                material.color = Color.Lerp(material.color, Color.red, Time.deltaTime * switchSpeed);
            } else {
                material.color = Color.Lerp(material.color, ogColor, Time.deltaTime * switchSpeed);
            }
        }

        if (materials[0].color == Color.red)
        {
            switchColor = true;
        }
        else if (materials[0].color == ogColor)
        {
            switchColor = false;
        }
    }

    // Fait apparaitre l'ennemi
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2.0f);

        // S'il n'y a pas le joueur sur la zone d'apparition, fait apparaitre l'ennemi
        Collider[] player = Physics.OverlapSphere(transform.position, 1.5f, 1 << 6);
        if (player.Length == 0)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            gameManager.EnemiesController(enemy);
        }
        gameObject.SetActive(false);
    }
}
