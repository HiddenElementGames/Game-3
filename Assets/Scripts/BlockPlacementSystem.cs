using UnityEngine;
using UnityEngine.InputSystem;

public class BlockPlacementSystem : MonoBehaviour
{
    [SerializeField] private Transform placementStartPoint;
    [SerializeField] private GameObject blockPrefab;

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            PlaceBlock(blockPrefab);
        }  
    }

    private void PlaceBlock(GameObject block)
    {
        if(Physics.Raycast(placementStartPoint.position, placementStartPoint.forward, out RaycastHit hitInfo))
        {
            if(hitInfo.collider.CompareTag("Block"))
            {
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x + hitInfo.normal.x / 2), 
                                                    Mathf.RoundToInt(hitInfo.point.y + hitInfo.normal.y / 2), 
                                                    Mathf.RoundToInt(hitInfo.point.z + hitInfo.normal.z / 2));
                Instantiate(block, spawnPosition, Quaternion.identity);
            }
            else
            {
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x), 
                                                    Mathf.RoundToInt(hitInfo.point.y), 
                                                    Mathf.RoundToInt(hitInfo.point.z));
                Instantiate(block, spawnPosition, Quaternion.identity);
            }
        }
    }
}
