using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float _lifetime = 0f;
    private BoxCollider _collider;
    [SerializeField] private int _damage = 10;

    public void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }
    public void Update()
    {
        _lifetime += Time.deltaTime;
        if (_lifetime > 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(_damage);

            Destroy(gameObject);
        }
        else
        {
        }
    }
}