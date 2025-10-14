using UnityEngine;

public interface ITower
{
    void Attack();
    void SetPlaced(bool placed);
}

[RequireComponent(typeof(Animator))]
public class RangedTower : MonoBehaviour, ITower
{
    public GameObject projectilePrefab;
    public bool isPlaced = true;

    private Animator _animator;
    private float _fireCooldown = 3f;
    private float _fireTimer = 0f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isPlaced)
        {
            _fireTimer += Time.deltaTime;
            if (_fireTimer >= _fireCooldown)
            {
                _animator.SetTrigger("Fire");
                Attack();
                _fireTimer = 0f;
            }
        }
    }

    public void Attack()
    {
        Quaternion rotation = transform.rotation * Quaternion.Euler(90, 0, 0);
        GameObject proj = Instantiate(projectilePrefab, transform.position + transform.forward, rotation);
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * 20f;
        }
    }

    public void SetPlaced(bool placed)
    {
        this.isPlaced = placed;
    }
}
