using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DanteStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;
    public BattleDantes hero;
    private Animator playerAnimator;
    public AudioSource SFX;
    public AudioClip Hit, Die;


    private bool actionStarted = false;
    private Vector3 startposition;
    private float animSpeed = 7f;
    public GameObject EnemyToAttack;

    
    private bool alive = true;

    private HeroPanelStats stats;
    public GameObject HeroPanel;
    public Transform HeroPanelSpacer;


    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }
    public TurnState currentState;

    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    private Image ProgressBar;

    
    void Start()
    {
        CreateHeroPanel();
        HeroPanelSpacer = GameObject.Find("BattleCanvas").transform.Find("HeroPanel").Find("HeroPanelSpaces");
        playerAnimator = GetComponent<Animator>();

        cur_cooldown = Random.Range(0, 2.5f);

        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        currentState = TurnState.PROCESSING;
        startposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);
            switch (currentState)
            {
                case (TurnState.PROCESSING):
                    UpgradeProgressBar();
                break;

                case (TurnState.ADDTOLIST):
                BSM.HerosToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;

                case(TurnState.WAITING):

                break;

                case(TurnState.SELECTING):
                break;

                case(TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;

                case(TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else
                {
                    this.gameObject.tag = "DeadHero";

                    BSM.HerosInBattle.Remove(this.gameObject);

                    BSM.HerosToManage.Remove(this.gameObject);

                    BSM.AttackPanel.SetActive(false);
                    BSM.EnemySelectPanel.SetActive(false);
                    if (BSM.HerosInBattle.Count > 0)
                    {
                        for (int i = 0; i < BSM.PerformList.Count; i++)
                        {
                            if (i != 0)
                            {
                                if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
                                {
                                    BSM.PerformList.Remove(BSM.PerformList[i]);
                                }
                                else if (BSM.PerformList[i].AttackersTarget == this.gameObject)
                                {
                                    BSM.PerformList[i].AttackersTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)];
                                }
                            }
                        }
                    }
                    playerAnimator.SetBool("IsDead", true);
                    SFX.clip = Die;
                    SFX.Play();
                    this.gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(105, 105, 105, 255);
                    StartCoroutine(DeathWithDelay());

                    alive = false;
                }
                break;
        }
    }
    IEnumerator DeathWithDelay()
    {
        yield return new WaitForSeconds(2);
        BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
        
    }
    void UpgradeProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
        if (cur_cooldown > max_cooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }
    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;

        playerAnimator.SetBool("isMoving", true);

        Vector3 enemyPosition = new Vector3(EnemyToAttack.transform.position.x - 1f, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z);

        while (MoveTowardsEnemy(enemyPosition)) { yield return null; }
        playerAnimator.SetBool("isAttacking", true);

        SFX.clip = Hit;
        SFX.Play();

        yield return new WaitForSeconds(0.5f);
        DoDamage();
        playerAnimator.SetBool("isAttacking", false);
        playerAnimator.SetBool("isMoving", false);


        Vector3 firstPosition = startposition;
        while (MoveTowardsEnemy(firstPosition)) { yield return null; }


        BSM.PerformList.RemoveAt(0);

        if (BSM.battleStates != BattleStateMachine.PerformAction.WIN && BSM.battleStates != BattleStateMachine.PerformAction.LOSE)
        {
            BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
            cur_cooldown = 0f;
            currentState = TurnState.PROCESSING;
        }
        else
        {
            currentState=TurnState.WAITING;
        }
        

        actionStarted = false;
        
    }
    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    public void TakeDamage(float getDamageAmount)
    {
        hero.curHP -= getDamageAmount;
        if (hero.curHP <= 0)
        {
            hero.curHP = 0;
            currentState = TurnState.DEAD;
        }
        UpdateHeroPanel();
    }
    void DoDamage()
    {
        float calc_damage = hero.curATK + BSM.PerformList[0].choosenAttack.attackDamage;
        EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calc_damage);
    }
    void CreateHeroPanel()
    {
        HeroPanel = Instantiate(HeroPanel,HeroPanelSpacer) as GameObject;
        stats = HeroPanel.GetComponent<HeroPanelStats>();
        stats.HeroName.text = hero.theName;
        stats.HeroHP.text = "HP: " + hero.curHP;
        stats.HeroMP.text = "SP: " + hero.curMP;

        ProgressBar = stats.ProgressBar;
        HeroPanel.transform.SetParent(HeroPanelSpacer, false);
    }
    void UpdateHeroPanel()
    {
        stats.HeroHP.text = "HP" + hero.curHP;
        stats.HeroMP.text = "SP" + hero.curMP;
    }


}

