using UnityEngine;

public class miniGameManager : MonoBehaviour
{
    [SerializeField] private int health = 3;

    public void Damage(int amount)
    {
        health -= amount;
        print(health);
        if (health <= 0)
        {
            print("Game Over");
            Time.timeScale = 0;
        }
    }
}
