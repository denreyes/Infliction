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

        playerHUD.SetHUD (playerUnit);
        enemyHUD.SetHUD (enemyUnit);

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
        dialogueText.text = "The attack is successful!";

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
            dialogueText.text = "You won the battle!";
            StartCoroutine(backToWorld());
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You are Ass";
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
        dialogueText.text = "You feel like a king!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator backToWorld(){
        yield return new WaitForSeconds(2f);
        
        SceneManager.LoadScene("World");
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
