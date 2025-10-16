using UnityEngine;

public class Skeleton : MonoBehaviour, IEnemy
{
    [SerializeField]
    private int _health = 5;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
