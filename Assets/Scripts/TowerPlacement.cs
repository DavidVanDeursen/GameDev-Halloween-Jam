using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacement : MonoBehaviour
{
    public GameObject towerPrefab;
    public Material previewMaterial;

    private GameObject previewTower;
    private Vector3 previewPosition;
    private Camera mainCamera;

    private InputAction _rotateAction;
    private Quaternion _quaternion;

    void Start()
    {
        mainCamera = Camera.main;

        if (towerPrefab != null && previewMaterial != null)
        {
            previewTower = Instantiate(towerPrefab);
            SetPreviewMaterial(previewTower, previewMaterial);
            previewTower.SetActive(false);
        }

        _rotateAction = InputSystem.actions.FindAction("Rotate");
        _quaternion = Quaternion.identity;
        _rotateAction.Enable();
    }

    void Update()
    {
        HandleTowerPreviewAndPlacement();
    }

    public void HandleTowerPreviewAndPlacement()
    {
        if (mainCamera == null || previewTower == null)
            return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (_rotateAction.WasPressedThisFrame())
            {
                _quaternion *= Quaternion.Euler(0, 90, 0);
            }
            previewPosition = hit.point;
            previewTower.GetComponent<BoxCollider>().enabled = false;
            previewTower.transform.position = previewPosition;
            previewTower.transform.rotation = _quaternion;
            previewTower.GetComponent<Animator>().SetTrigger("Idle");
            previewTower.GetComponent<ITower>().SetPlaced(false);
            previewTower.SetActive(true);

            // Place tower on left click
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Instantiate(towerPrefab, previewPosition, _quaternion);
            }
        }
        else
        {
            previewTower.SetActive(false);
        }
    }

    public void SetPreviewMaterial(GameObject obj, Material mat)
    {
        foreach (var renderer in obj.GetComponentsInChildren<Renderer>())
        {
            renderer.material = mat;
        }
    }
}