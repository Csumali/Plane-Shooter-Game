using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUpdate : MonoBehaviour
{
    private Text text;
    private string mode, touched, eggs, enemies, enemiesDestroyed;
    private HeroMovement hero;
    private Shooting shooting;
    private EggSystem eggSystem; 
    private EnemySystem enemySystem;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        hero = GameObject.Find("Hero").GetComponent<HeroMovement>();
        shooting = GameObject.Find("Hero").GetComponent<Shooting>();
        eggSystem = GameObject.Find("EggSystem").GetComponent<EggSystem>();
        enemySystem = GameObject.Find("EnemySystem").GetComponent<EnemySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Status();
    }

    void Status()
    {
        if (hero.getMode()) mode = "Mouse";
        else mode = "Keyboard";

        touched = hero.getTouched().ToString();

        eggs = eggSystem.getEggs().ToString();

        enemies = enemySystem.getEnemies().ToString();

        enemiesDestroyed = enemySystem.getEnemiesDestroyed().ToString();
        textColor();
    }

    private void textColor()
    {
        if (Time.time >= shooting.getNextMissile())
        {
            text.text = "Mode: " + mode + "\t\tEnemies Touched: " + touched + "\t\tEggs On Screen: " + eggs +
                    "\t\tEnemies: " + enemies + "\t\tDestroyed: " + enemiesDestroyed + "\t\tRandom Movement [E]" +
                    "\t\tToggle Health [H]" + "\t\t<color=#00FF00>Missile [C]</color>" + "\t\tQuit [Q]";
        }
        else
        {
            text.text = "Mode: " + mode + "\t\tEnemies Touched: " + touched + "\t\tEggs On Screen: " + eggs +
                    "\t\tEnemies: " + enemies + "\t\tDestroyed: " + enemiesDestroyed + "\t\tRandom Movement [E]" + 
                    "\t\tToggle Health [H]" + "\t\t<color=#FF0000>Missile [C]</color>" + "\t\tQuit [Q]";
        }
    }
}
