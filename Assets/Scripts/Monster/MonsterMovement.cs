using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Pathfinder;

public class MonsterMovement : MonoBehaviour
{
    private readonly float SPEED_FACTOR = 0.001f;

    public GameObject GameManager;

    private Tilemap worldMap;
    private Pathfinder pathfinder;
    private Monster monster;
    private bool paused = false;
    private bool moving = false;


    void Start()
    {
        
    }

    public void initialize()
    {
        monster = GetComponent<Monster>();
        pathfinder = GameManager.GetComponent<Pathfinder>();
        worldMap = pathfinder.WorldMap;
    }

    public void StartMoving()
    {
        if(!Moving())
            StartCoroutine(Move());
    }

    public void Pause() => paused = true;
    public void Resume() => paused = false;
    public bool Moving() => moving;

    IEnumerator Move()
    {
        Vector3Int initTilePos = worldMap.WorldToCell(transform.position);
        Debug.Log("Begin movement => monster starting node: "+initTilePos.ToString());
        Node currentNode = pathfinder.FindNode(initTilePos);
        if (currentNode != null)
        {
            while (currentNode.HasNext())
            {
                moving = true;
                paused = false;
                Node nextNode = currentNode.Next();
                
                Vector3 nextNodeWorldPos = nextNode.GetWorldPos(worldMap);
                Vector3 speedVector = (nextNodeWorldPos - transform.position);
                speedVector.Normalize();
                speedVector = SPEED_FACTOR * monster.movementSpeed * speedVector;
                while (Vector2.Distance(transform.position, nextNodeWorldPos) > speedVector.magnitude)
                {
                    if (!paused)
                    {
                        transform.position = new Vector3(transform.position.x + speedVector.x, transform.position.y + speedVector.y, transform.position.z);
                    }
                    yield return null;
                }
                currentNode = nextNode;
            }
            Debug.Log(string.Format("Monster reaches the end of the path, last {0} ", currentNode.ToLongString()));
            transform.position = currentNode.GetWorldPos(worldMap);
        }
        moving = false;
    }

}
