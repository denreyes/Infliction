using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject enemyPrefab;

    public Transform playerBattleStation;

    public Transform enemyBattleStation;

    Unit playerUnit;

    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;

    public BattleHUD enemyHUD;

    public BattleState state;

    // array of random successful attacks messages
    private string[] successBattleMessages = new string[] {
        "You hit the enemy!",
        "You dealt damage to the enemy!",
        "You did a good job!",
        "The attack was successful!",
        "The enemy is hurt!",
    };

    private string[] healBattleMessages = new string[] {
        "You healed yourself!",
        "You feel better!",
        "You are now healthier!",
    };

    private string[] lostBattleMessages = new string[] {
        "You lost the battle!",
        "You are defeated!",
        "You are no longer in the battle!",
    };

    private string[] wonBattleMessages = new string[] {
        "You won the battle!",
        "You are victorious!",
        "You are the winner!",
    };

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        dialogueText.text =
            "What to do you want to do" + " " + playerUnit.unitName + "?";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack(string type = "normal")
    {
        // Damage the enemy
        bool isDead;
        if (type == "skill")
        {
            isDead = enemyUnit.TakeDamage(playerUnit.skillDamage);
        }
        else
        {
            isDead = enemyUnit.TakeDamage(playerUnit.damage);
        }

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = successBattleMessages[Random.Range(0, successBattleMessages.Length)];

        yield return new WaitForSeconds(2f);

        // Check if the enemy is dead
        if (isDead)
        {
            // End the battle
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            //Enemy turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        // Change state based on what happened
    }

    IEnumerator EnemyTurn() // Enemy actions logic

    {
        dialogueText.text = enemyUnit.unitName + " " + "atacks!";
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = wonBattleMessages[Random.Range(0, wonBattleMessages.Length)];
            StartCoroutine(backToWorld());
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = lostBattleMessages[Random.Range(0, lostBattleMessages.Length)];
            StartCoroutine(gameOver());
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = healBattleMessages[Random.Range(0, healBattleMessages.Length)];

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator backToWorld()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("World");
    }

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("GameOver");
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        StartCoroutine(PlayerAttack());
    }

    public void OnSkillButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        StartCoroutine(PlayerAttack("skill"));
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN) return;

        StartCoroutine(PlayerHeal());
    }
}
