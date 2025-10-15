using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    private InputAction rewindAoe;
    public Animator animator;
    public Object staticPrefab;

    public CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
        rewindAoe = InputSystem.actions.FindAction("Rewind AOE");
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SetUpListeners();
    }

    void SetUpListeners()
    {
        rewindAoe.started += _ => HoverStatic();
        rewindAoe.performed += _ => PlaceStatic();
        rewindAoe.canceled += _ => CancelStatic();
    }

    void HoverStatic()
    {

    }

    void PlaceStatic()
    {
        animator.SetTrigger("Cast");
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Physics.Raycast(ray, out RaycastHit hit);
        
        Instantiate(staticPrefab, hit.point, Quaternion.identity);

        GetComponent<PlayerControl>().enabled = false;
        StartCoroutine(RotateToCast(hit.point));
    }

    void CancelStatic()
    {

    }

    IEnumerator RotateToCast(Vector3 point)
    {
        for (int i = 0; i < 50; i++)
        {

            Vector3 direction = point - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(CameraDirection(direction)), 0.5f);
            yield return null;
        }
        GetComponent<PlayerControl>().enabled = true;
    }
        Vector3 CameraDirection(Vector3 movementDirection)
    {
        var cameraForward = Camera.main.transform.forward;
        var cameraRight = Camera.main.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        return cameraForward * movementDirection.z + cameraRight * movementDirection.x;

    }
}
