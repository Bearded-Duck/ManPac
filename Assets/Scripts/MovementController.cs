using UnityEngine;

public class MovementController : MonoBehaviour
{
    public NodeController currentNode;

    public float speed = 1f;

    public string direction = "";
    public string lastMovingDirection = "";

    public string queuedDirection = "";

    public bool isGhost = false;

    void Start()
    {
        transform.position = currentNode.transform.position;
    }

    void Update()
    {
        NodeController currentNodeController = currentNode;

        transform.position = Vector2.MoveTowards(
            transform.position,
            currentNode.transform.position,
            speed * Time.deltaTime
        );

        bool reverseDirection = false;

        if (
            (direction == "left" && lastMovingDirection == "right")
            || (direction == "right" && lastMovingDirection == "left")
            || (direction == "up" && lastMovingDirection == "down")
            || (direction == "down" && lastMovingDirection == "up")
        )
        {
            reverseDirection = true;
        }

        if (
            Vector2.Distance(transform.position, currentNode.transform.position) < 0.05f
            || reverseDirection
        )
        {
            if (isGhost)
            {
                GetComponent<EnemyController>().ReachedCenterOfNode(currentNodeController);
            }

            transform.position = currentNode.transform.position;

            GameObject newNode = null;

            if (queuedDirection != "")
            {
                newNode = currentNodeController.GetNodeFromDirection(queuedDirection);

                if (newNode != null)
                {
                    direction = queuedDirection;
                    queuedDirection = "";
                }
            }

            if (newNode == null)
            {
                newNode = currentNodeController.GetNodeFromDirection(direction);
            }

            if (newNode != null)
            {
                currentNode = newNode.GetComponent<NodeController>();
                lastMovingDirection = direction;
            }
        }
    }

    public void SetDirection(string newDirection)
    {
        queuedDirection = newDirection;
    }
}