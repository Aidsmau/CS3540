using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TargetBehavior : MonoBehaviour
{
    public Slider healthSlider;
    public int health = 100;
    int  maxHealth;
    public ParticleSystem baseAttackVFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = health;

        if(healthSlider){
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        if(healthSlider)
            healthSlider.value = health;
        
        if (health <= 0)
        {
            Debug.Log("Game Over");
            health = 0;
            GameLost();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy")) {
            TakeDamage(other.GetComponent<EnemyAI>().GetEnemyDamageValue());
            if(baseAttackVFX)
                baseAttackVFX.Play();
        }

        Destroy(other.gameObject);
    }

    void GameLost()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
