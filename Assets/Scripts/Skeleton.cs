using UnityEngine;

public class Skeleton : MonoBehaviour, IEnemy
{
    private int _health = 100;

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }
}
