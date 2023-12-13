using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    public static Game_Controller instance;

    private static int health = 5;
    private static int maxHealth = 5;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;

    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }

    public TMP_Text healthText;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health " + health;
    }

    public static void DmgPlayer(int Damage)
    {
        health -= Damage;
        if(health <= 0)
        {
            KillPlayer();
        }

    }

    public static void HealPlayer(int Heal)
    {
        health = Mathf.Min(maxHealth, health + Heal);
    }

    private static void KillPlayer()
    {
#if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
