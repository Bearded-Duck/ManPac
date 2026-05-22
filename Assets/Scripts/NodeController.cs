using UnityEngine;

public class NodeController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rayDistance = 0.5f;

    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;

    public GameObject nodeLeft;
    public GameObject nodeRight;
    public GameObject nodeUp;
    public GameObject nodeDown;

    public bool isPelleteNode = false;
    public bool hasPellete = false;

    public SpriteRenderer pelletSprite;

    void Start()
    {
        if (transform.childCount > 0)
        {
            hasPellete = true;
            isPelleteNode = true;
            pelletSprite = GetComponentInChildren<SpriteRenderer>();
        }

        RaycastHit2D hitDown = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            rayDistance,
            layerMask
        );

        if (hitDown.collider != null)
        {
            canMoveDown = true;
            nodeDown = hitDown.collider.gameObject;
        }

        RaycastHit2D hitUp = Physics2D.Raycast(
            transform.position,
            Vector2.up,
            rayDistance,
            layerMask
        );

        if (hitUp.collider != null)
        {
            canMoveUp = true;
            nodeUp = hitUp.collider.gameObject;
        }

        RaycastHit2D hitLeft = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            rayDistance,
            layerMask
        );

        if (hitLeft.collider != null)
        {
            canMoveLeft = true;
            nodeLeft = hitLeft.collider.gameObject;
        }

        RaycastHit2D hitRight = Physics2D.Raycast(
            transform.position,
            Vector2.right,
            rayDistance,
            layerMask
        );

        if (hitRight.collider != null)
        {
            canMoveRight = true;
            nodeRight = hitRight.collider.gameObject;
        }
    }

    public GameObject GetNodeFromDirection(string direction)
    {
        if (direction == "left" && canMoveLeft)
        {
            return nodeLeft;
        }

        if (direction == "right" && canMoveRight)
        {
            return nodeRight;
        }

        if (direction == "up" && canMoveUp)
        {
            return nodeUp;
        }

        if (direction == "down" && canMoveDown)
        {
            return nodeDown;
        }

        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isPelleteNode && hasPellete)
        {
            hasPellete = false;

            if (pelletSprite != null)
            {
                pelletSprite.enabled = false;
            }

            // If the shortcut link is broken, look for the object manually in the scene
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(10);
            }
            else
            {
                // Manual fallback search
                GameManager foundManager = FindAnyObjectByType<GameManager>();
                if (foundManager != null)
                {
                    foundManager.AddScore(10);
                }
                else
                {
                    Debug.LogError("The pellets are disappearing, but Unity cannot find a 'GameManager' script anywhere in your active scene hierarchy!");
                }
            }
        }
    }
}