using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;
    public BattleEnemy enemy;
    private Animator enemyAnimator;
    public AudioSource SFX;
    public AudioClip Hit,Die;


    public enum TurnState
    {
        PROCESSING,
        CHOSEACTION,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }
    public TurnState currentState;

    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;

    private Vector3 startposition;
    private bool actionStarted = false;
    public GameObject HeroToAttack;
    private float animSpeed = 7f;

    private bool alive = true;



    void Start()
    {
        currentState = TurnState.PROCESSING;
        enemyAnimator = GetComponent<Animator>();

        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;


            case (TurnState.CHOSEACTION):
                ChooseAction();
                currentState = TurnState.WAITING;

                break;


            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;

            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else
                {
                    this.gameObject.tag = "DeadEnemy";
                    BSM.EnemysInBattle.Remove(this.gameObject);
                    SFX.clip = Die;
                    SFX.Play();
                    enemyAnimator.SetBool("ZombieDeath", true);

                    if (BSM.EnemysInBattle.Count > 0)
                    {
                        for (int i = 0; i < BSM.EnemysInBattle.Count; i++)
                        {
                            if (i != 0)
                            {

                                if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
                                {
                                    BSM.PerformList.Remove(BSM.PerformList[i]);
                                }
                                else if(BSM.PerformList[i].AttackersTarget == this.gameObject)
                                {
                                    BSM.PerformList[i].AttackersTarget = BSM.EnemysInBattle[Random.Range(0, BSM.EnemysInBattle.Count)];
                                }
                            }
                        }
                        
                    }
                    this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(105, 105, 105, 255);
                    //ADD DEAD ANIM
                    alive = false;
                    BSM.EnemyButtons();
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                }
                break;
        }
    }
        void UpgradeProgressBar()
        {
            cur_cooldown = cur_cooldown + Time.deltaTime;
            if (cur_cooldown > max_cooldown)
            {
                currentState = TurnState.CHOSEACTION;
            }
        }
        void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = enemy.gameObject;
        myAttack.Type = "Enemy";
        myAttack.AttackersGameObject = this.gameObject;
        if (BSM.HerosInBattle.Count > 0)
        {
            myAttack.AttackersTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)];
            int num = Random.Range(0, enemy.attacks.Count);
            myAttack.choosenAttack = enemy.attacks[num];
            Debug.Log(this.gameObject.name + " has choosen " + myAttack.choosenAttack.attackName + " and deals " + myAttack.choosenAttack.attackDamage + " damage! ");

            BSM.CollectActions(myAttack);
        }
        else
        {
            currentState = TurnState.WAITING;
        }
        

        


    }
    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;
        //enemy animation
        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x + 1f,transform.position.y, HeroToAttack.transform.position.z);
        while (MoveTowardsEnemy(heroPosition)) {yield return null;}
        enemyAnimator.SetBool("ZombieAttack", true);
        SFX.clip = Hit;
        SFX.Play();
        yield return new WaitForSeconds(1f);

        DoDamage();
        enemyAnimator.SetBool("ZombieAttack", false);

        Vector3 firstPosition = startposition;
        while (MoveTowardsEnemy(firstPosition)) { yield return null; }

        BSM.PerformList.RemoveAt(0);
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;



        actionStarted = false;
        cur_cooldown = 0f;
        currentState = TurnState.PROCESSING;
    }
    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    void DoDamage()
    {
        float calc_damage = enemy.curATK + BSM.PerformList[0].choosenAttack.attackDamage;
        HeroToAttack.GetComponent<DanteStateMachine>().TakeDamage(calc_damage);
    }
    public void TakeDamage(float getDamageAmount)
    {
        enemy.curHP -= getDamageAmount;
        if (enemy.curHP <= 0)
        {
            enemy.curHP = 0;
            currentState = TurnState.DEAD;
        }
    }
}
