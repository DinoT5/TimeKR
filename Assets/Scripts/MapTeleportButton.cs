using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTeleportButton : CurrencyDependent
{
    // Start is called before the first frame update

    public Transform teleportLocation;
    public string sceneToLoad;
    public GameObject MapPanel;
    [SerializeField] private ScreenFader _fader;
    [SerializeField] private int TravelCost;
    [SerializeField] private CurrencyUICounter CostCounter;
    [SerializeField] private CinemachineVirtualCamera _vCam;

    public Transform Player;

    private void Awake()
    {
        
        GameObject player = GameObject.FindWithTag("Player");
        Player = Player.transform;
        if (CostCounter != null)
        {
            CostCounter.TotalCurrency = TravelCost;
        }

    }

    public void Teleport()
    {
        if (_TotalCurrency >= TravelCost)
        {
            _fader.FadeToBlack(2f, FinishedFadingToBlack);


        }
        else
        {
            _fader.FadeToBlack(2f, FinishedFadingToBlackNoTime);
            //Can call an action here if we want to play an effect somewhere else to show u got no funds
        }
        MapPanel.SetActive(false);
        

       
    }
    public void TeleportScene()
    {

        if (_TotalCurrency >= TravelCost)
        {
            PlayerPrefs.SetFloat("TeleportX", teleportLocation.position.x);
            PlayerPrefs.SetFloat("TeleportY", teleportLocation.position.y);
            PlayerPrefs.SetFloat("TeleportZ", teleportLocation.position.z);

            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            
            //Can call an action here if we want to play an effect somewhere else to show u got no funds
        }
    }
    
    public void CloseMap()
    {
        MapPanel.SetActive(false);
        _fader.FadeFromBlack(4f, null);
    }

    private void FinishedFadingFromBlack()
    {
        MapPanel.SetActive(false);

        _fader.FadeFromBlack(1f,() => SetDamping(true));
        

    }
    private void FinishedFadingToBlack()
    {
        SetDamping(false);
        Actions.OrderCurrencyUpdate.InvokeAction(-TravelCost);
        //Player.gameObject.transform.position = teleportLocation.position;
        TeleportPlayer();
        _fader.FadeToBlack(2f, FinishedFadingFromBlack);
        MapPanel.SetActive(false);

    }
    private void FinishedFadingToBlackNoTime()
    {
        DialoguePrinter.Instance.PrintTimerRunOutLine("Time has run out\r\nyou make your way back to the apartment", 0.06f, () => FinishedFadingFromBlackNoTime());
        //_fader.FadeToBlack(2f, FinishedFadingFromBlackNoTime);
    }
    private void FinishedFadingFromBlackNoTime()
    {
        SceneManager.LoadScene("Battle Mode");
        //StartCoroutine(WaitAndFadeFromBlackNoTime());
        
    }

    private void SetDamping(bool enableDamping)
    {
        if (enableDamping)
        {
            _vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 1;
            _vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 1;
            _vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = 1;

        }
        else
        {
            _vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 0;
            _vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0;
            _vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = 0;
        }
    }
    private void TeleportPlayer()
    {
        if (teleportLocation.position != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject mainCamera = Camera.main.gameObject;
            if (player != null && mainCamera != null)
            {
                Vector3 offset = teleportLocation.position - player.transform.position;

                player.transform.position += offset;

                mainCamera.transform.position += offset;
            }
            else
            {
                Debug.LogError("Player GameObject not found.");
            }
        }
        else
        {
            Debug.LogError("Teleport destination not set.");
        }
    }

}
