using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
            WAIT,
            TAKEACTON,
            PERFORMACTION,
            CHECKALIVE,
            WIN,
            LOSE
    }
    public PerformAction battleStates;

    public List<HandleTurn> PerformList = new List<HandleTurn>();
    public List<GameObject> HerosInBattle = new List<GameObject>();
    public List<GameObject> EnemysInBattle = new List<GameObject>();
    
    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE
    }
    public HeroGUI HeroInput;

    public List<GameObject> HerosToManage = new List<GameObject>();
    private HandleTurn HeroChoice;
    public GameObject enemyButton;
    public Transform Spacer;

    public GameObject AttackPanel;
    public GameObject EnemySelectPanel;
    public GameObject MagicPanel;

    public Transform actionSpacer;
    public Transform magicSpacer;
    public GameObject actionButton;
    public GameObject magicButton;
    private List<GameObject> atkBtns = new List<GameObject>();

    private List<GameObject> enemyBtns = new List<GameObject>();
    public Canvas _eventCanvas;
    public Canvas _battleCanvas;

    //sounds
    public AudioSource sfx;
    public AudioClip _loseGame;




    void Start()
    {

        _eventCanvas.gameObject.SetActive(false);
        battleStates = PerformAction.WAIT;
        EnemysInBattle.AddRange (GameObject.FindGameObjectsWithTag("Enemy"));
        HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        HeroInput = HeroGUI.ACTIVATE;

        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(false);
        MagicPanel.SetActive(false);

        EnemyButtons();
    }

    // Update is called once per frame
    void Update()
    {
        switch (battleStates)
        {
            case(PerformAction.WAIT):
                if (PerformList.Count > 0)
                {
                    battleStates = PerformAction.TAKEACTON;
                }
            break;
            case(PerformAction.TAKEACTON):
                GameObject performer = PerformList[0].Attacker;
                if (PerformList[0].Type == "Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    for (int i = 0; i < HerosInBattle.Count; i++)
                    {
                        if (PerformList[0].AttackersTarget == HerosInBattle[i])
                        {
                            ESM.HeroToAttack = PerformList[0].AttackersTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                            break;
                        }
                        else
                        {
                            PerformList[0].AttackersTarget = HerosInBattle[Random.Range(0, HerosInBattle.Count)];
                            ESM.HeroToAttack = PerformList[0].AttackersTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                        }
                    }
                    ESM.HeroToAttack = PerformList[0].AttackersTarget;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                }
                if (PerformList[0].Type == "Hero")
                {
                    DanteStateMachine HSM = performer.GetComponent<DanteStateMachine>();
                    HSM.EnemyToAttack = PerformList[0].AttackersTarget;
                    HSM.currentState = DanteStateMachine.TurnState.ACTION;
                }
                battleStates = PerformAction.PERFORMACTION;
                    break;
            case (PerformAction.PERFORMACTION):
                break;
            
            
                 case (PerformAction.CHECKALIVE):
                        if (HerosInBattle.Count < 1)
                    {
                        battleStates = PerformAction.LOSE;
                    }
                        else if (EnemysInBattle.Count < 1)
                    {
                        battleStates = PerformAction.WIN;
                    }
                    else
                    {
                        clearAttackPanel();
                        HeroInput = HeroGUI.ACTIVATE;
                    }
                break;
            case (PerformAction.LOSE):
                {
                    for (int i = 0; i < EnemysInBattle.Count; i++)
                    {
                        EnemysInBattle[i].GetComponent<EnemyStateMachine>().currentState = EnemyStateMachine.TurnState.WAITING;
                    }

                    _battleCanvas.enabled = false;
                    _eventCanvas.gameObject.SetActive(true);
                    sfx.clip = _loseGame;
                    sfx.Play();

                }
                break;
            case (PerformAction.WIN):
                {
                    /*for (int i = 0; i < HerosInBattle.Count; i++)
                    {
                        HerosInBattle[i].GetComponent<DanteStateMachine>().currentState = DanteStateMachine.TurnState.WAITING;
                    }*/
                    if (PlayerPrefs.HasKey("Cure"))
                    {
                        if (PlayerPrefs.GetInt("Cure") == 1)
                        {
                            SceneManager.LoadScene("Won_WithCure");
                        }
                        else
                        {
                            SceneManager.LoadScene("Won_NoCure");
                        }
                    }
                    else
                    {
                        SceneManager.LoadScene("Won_NoCure");
                    }
                    
                    
                }
                break;
        }
        switch (HeroInput)
        {
            case(HeroGUI.ACTIVATE):
                if (HerosToManage.Count > 0)
                {
                    HeroChoice = new HandleTurn();

                    AttackPanel.SetActive(true);
                    CreateAttackButtons();

                    
                    HeroInput = HeroGUI.WAITING;
                }
            break;
            case (HeroGUI.WAITING):
                break;
            case (HeroGUI.DONE):
                HeroInputDone();
            break;
        }
    }
    public void CollectActions(HandleTurn input)
    {
        PerformList.Add (input);
    }
    public void EnemyButtons()
    {
        

            foreach(GameObject enemyBtn in enemyBtns)
            {
                Destroy(enemyBtn);
            }
            enemyBtns.Clear();

        foreach (GameObject enemy in EnemysInBattle)
        {
            GameObject newButton = Instantiate (enemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine cur_enemy = enemy.GetComponent<EnemyStateMachine> ();

            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = cur_enemy.enemy.theName;
            button.EnemyPrefab = enemy;

            newButton.transform.SetParent(Spacer, false);
            enemyBtns.Add(newButton);


        }
    }
    public void Input1()
    {
        HeroChoice.Attacker = HerosToManage[0];
        HeroChoice.AttackersGameObject = HerosToManage[0];
        HeroChoice.Type = "Hero";
        HeroChoice.choosenAttack = HerosToManage[0].GetComponent<DanteStateMachine>().hero.attacks[0];
        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }
    public void Input2(GameObject choosenEnemy)
    {
        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
    }
    void HeroInputDone()
    {
        PerformList.Add(HeroChoice);
        clearAttackPanel();
        HerosToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
    }
    void clearAttackPanel()
    {
        EnemySelectPanel.SetActive(false);
        AttackPanel.SetActive(false);
        MagicPanel.SetActive(false);
        foreach (GameObject atkBtn in atkBtns)
        {
            Destroy(atkBtn);
        }
        atkBtns.Clear();
    }
    void CreateAttackButtons()
    {
        GameObject AttackButton = Instantiate(actionButton) as GameObject;
        TMP_Text AttackButtonText = AttackButton.transform.GetChild(0).GetComponent<TMP_Text>();
        AttackButtonText.text = "Attack";
        AttackButton.GetComponent<Button>().onClick.AddListener(() => Input1());
        AttackButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(AttackButton);

        GameObject MagicAttackButton = Instantiate(actionButton) as GameObject;
        TMP_Text MagicAttackButtonText = MagicAttackButton.transform.GetChild(0).GetComponent<TMP_Text>();
        MagicAttackButtonText.text = "Skill";
        MagicAttackButton.GetComponent<Button>().onClick.AddListener(() => Input3());
        MagicAttackButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(MagicAttackButton);

        if (HerosToManage[0].GetComponent<DanteStateMachine>().hero.MagicAttacks.Count > 0)
        {
            foreach (BaseAttack magicAtk in HerosToManage[0].GetComponent<DanteStateMachine>().hero.MagicAttacks)
            {
                GameObject MagicButton = Instantiate(magicButton) as GameObject;
                TMP_Text MagicButtonText = MagicButton.transform.GetChild(0).GetComponent<TMP_Text>();
                MagicButtonText.text = magicAtk.attackName;
                AttackButton ATB = MagicButton.GetComponent<AttackButton>();
                ATB.magicAttackToPerform = magicAtk;
                MagicButton.transform.SetParent(magicSpacer, false);
                atkBtns.Add(MagicButton);
                
            }
        }
        else
        {
            MagicAttackButton.GetComponent<Button>().interactable = false;
        }
    }
    public void Input4(BaseAttack choosenMagic)
    {
        HeroChoice.Attacker = HerosToManage[0];
        HeroChoice.AttackersGameObject = HerosToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.choosenAttack = choosenMagic;
        
        MagicPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }
    public void Input3()
    {
        AttackPanel.SetActive(false);
        MagicPanel.SetActive(true);
    }
}
