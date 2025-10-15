using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float _lifetime = 0f;
    private BoxCollider2D _collider2D;

    public void Start()
    {
        _collider2D = GetComponent<BoxCollider2D>();
    }
    public void Update()
    {
        _lifetime += Time.deltaTime;
        if (_lifetime > 5f)
        {
            Destroy(gameObject);
        }
    }
}
