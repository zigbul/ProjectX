using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 2f;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private LayerMask _collectionLayer;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactionDistance, _interactionLayer))
        {
            IInterectable device = hit.collider.GetComponent<IInterectable>();

            if (device != null)
            {
                device.Interact();
            }
        }

        if (Physics.Raycast(ray, out hit, _interactionDistance, _collectionLayer))
        {
            ICollectable item = hit.collider.GetComponent<ICollectable>();

            if (item != null)
            {
                item.Collect();
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * _interactionDistance, Color.green);
    }
}
