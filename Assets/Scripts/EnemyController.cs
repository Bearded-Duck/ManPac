using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Setup References")]
    public NodeController startingNode;

    private Transform player;

    [HideInInspector]
    public MovementController movementController;

    void Awake()
    {
        movementController = GetComponent<MovementController>();
        movementController.currentNode = startingNode;
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Ghost couldn't find a GameObject with the tag 'Player'! Make sure your Pacman has the 'Player' tag set in the Inspector.");
        }

        movementController.direction = "right";
    }

    public void ReachedCenterOfNode(NodeController nodeController)
    {
        if (player == null) return;

        string bestDirection = "";
        float shortestDistance = float.MaxValue;

        string[] directions = { "left", "right", "up", "down" };
        string currentMovingDir = movementController.direction;

        foreach (string dir in directions)
        {
            if (
                (dir == "left" && currentMovingDir == "right") ||
                (dir == "right" && currentMovingDir == "left") ||
                (dir == "up" && currentMovingDir == "down") ||
                (dir == "down" && currentMovingDir == "up")
            )
            {
                continue;
            }

            GameObject neighborObject = nodeController.GetNodeFromDirection(dir);

            if (neighborObject != null)
            {
                float distanceToPlayer = Vector2.Distance(neighborObject.transform.position, player.position);

                if (distanceToPlayer < shortestDistance)
                {
                    shortestDistance = distanceToPlayer;
                    bestDirection = dir;
                }
            }
        }

        if (bestDirection == "")
        {
            if (currentMovingDir == "left") bestDirection = "right";
            else if (currentMovingDir == "right") bestDirection = "left";
            else if (currentMovingDir == "up") bestDirection = "down";
            else if (currentMovingDir == "down") bestDirection = "up";
        }

        if (bestDirection != "")
        {
            movementController.direction = bestDirection;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerHealth = collision.GetComponent<PlayerController>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }
}