using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health System Management")]
    [SerializeField] int maxHealth = 100; //Vida maxima del enemigo
    [SerializeField] int health = 100; //Vida actual del enemigo

    [Header("Feedback Configuration")]
    [SerializeField] Material DamagedMat; //Material del feedback del daño
    [SerializeField] GameObject deathVfx;
    [SerializeField] MeshRenderer enemyRend; //Ref al componente que dibuja los materiales del enemigo en pantalla
    Material baseMat;

    private void Awake()
    {
        
        health = maxHealth;
        baseMat = enemyRend.material;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            health = 0;
            deathVfx.SetActive(true);
            deathVfx.transform.position = transform.position;
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        enemyRend.material = DamagedMat;
        Invoke(nameof(ResetEnemyMaterial), 0.1f); //Espera de tiempo que permite ver el parpadeo

    }

    void ResetEnemyMaterial()
    {
        enemyRend.material = baseMat;
    }
}
