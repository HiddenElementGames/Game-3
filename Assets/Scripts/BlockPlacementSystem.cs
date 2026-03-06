using UnityEngine;
using UnityEngine.InputSystem;

public class BlockPlacementSystem : MonoBehaviour
{
    [SerializeField] private Transform placementStartPoint;
    [SerializeField] private GameObject blockPrefab;

    // used for checking if player is in the way of block placement. Value is roughly half the size of a block
    private Vector3 halfExtents = new Vector3(0.49f, 0.49f, 0.49f);

    private void Update()
    {
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            PlaceBlock(blockPrefab);
        }  
    }

    private void PlaceBlock(GameObject block)
    {
        if(Physics.Raycast(placementStartPoint.position, placementStartPoint.forward, out RaycastHit hitInfo))
        {
            // calculate the spawn position
            Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x + hitInfo.normal.x / 2), 
                                                Mathf.RoundToInt(hitInfo.point.y + hitInfo.normal.y / 2), 
                                                Mathf.RoundToInt(hitInfo.point.z + hitInfo.normal.z / 2));

			// ensure the player is not in the way before spawning the box
			if (Physics.OverlapBox(spawnPosition, halfExtents).Length == 0)
            {
				Instantiate(block, spawnPosition, Quaternion.identity);
			}
        }
    }
}
