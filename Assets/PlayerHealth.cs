using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public int startingHealth = 100;
    private int currentHealth;
    public static bool IsAlive { get; private set; }
    public AudioClip deathSFX;
    void Start()
    {
        currentHealth = startingHealth;
        IsAlive = true;
        UpdateSlider();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);

        UpdateSlider();
        
        Debug.Log("Damage taken " + currentHealth);
        if (currentHealth <= 0 && IsAlive)
        {
            //player death
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Dies");
        IsAlive = false;
        var audioSource = GetComponent<AudioSource>();
        if (audioSource)
        {
            audioSource.Play();
        }

        transform.Rotate(-90, 0, 0, Space.Self);
    }

    void UpdateSlider()
    {
        if (healthSlider)
        {
            healthSlider.value = currentHealth;
        }
    }
}