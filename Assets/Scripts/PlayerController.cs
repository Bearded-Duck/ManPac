using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    MovementController movementController;

    [Header("Health Settings")]
    public int currentHP = 2;
    private bool isInvincible = false;
    public float invincibilityDuration = 1.5f;

    void Start()
    {
        movementController = GetComponent<MovementController>();

        if (GameManager.instance != null)
        {
            GameManager.instance.UpdateHPDisplay(currentHP);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movementController.SetDirection("left");
            transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movementController.SetDirection("right");
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movementController.SetDirection("up");
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movementController.SetDirection("down");
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isInvincible) return;

        currentHP -= damageAmount;

        if (GameManager.instance != null)
        {
            GameManager.instance.UpdateHPDisplay(currentHP);
        }

        if (currentHP <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(BecomeInvincible());
        }
    }

    private System.Collections.IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    void GameOver()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}