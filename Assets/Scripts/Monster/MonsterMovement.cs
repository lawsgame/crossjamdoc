using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Pathfinder;

public class MonsterMovement : MonoBehaviour
{
    private readonly float SPEED_FACTOR = 0.001f;

    [SerializeField] private GameObject GameManager;

    private Tilemap worldMap;
    private Pathfinder pathfinder;
    private Monster monster;


    void Start()
    {
        monster = GetComponent<Monster>();
        pathfinder = GameManager.GetComponent<Pathfinder>();
        worldMap = pathfinder.WorldMap;
    }

    public void StartMoving() => StartCoroutine(Move());

    IEnumerator Move()
    {
        Vector3Int initTilePos = worldMap.WorldToCell(transform.position);
        Debug.Log("monster node: "+initTilePos.ToString());
        Node currentNode = pathfinder.FindNode(initTilePos);
        Vector3 currentNodeWorldPos = transform.position;
        if (currentNode != null)
        {
            while (currentNode.HasNext())
            {
                Node nextNode = currentNode.Next();
                
                Vector3 nextNodeWorldPos = nextNode.GetWorldPos(worldMap);
                Vector3 speedVector = (nextNodeWorldPos - currentNodeWorldPos);
                speedVector.Normalize();
                speedVector = SPEED_FACTOR * monster.movementSpeed * speedVector;
                while (Vector2.Distance(transform.position, nextNodeWorldPos) > speedVector.magnitude)
                {
                    Debug.Log(Vector2.Distance(transform.position, nextNodeWorldPos) + "?> " + speedVector.magnitude);
                    transform.position = new Vector3(transform.position.x + speedVector.x, transform.position.y + speedVector.y, transform.position.z);
                    yield return null;
                }
                currentNode = nextNode;
                currentNodeWorldPos = nextNodeWorldPos;
            }
            transform.position = currentNode.GetWorldPos(worldMap);
        }
    }

}
