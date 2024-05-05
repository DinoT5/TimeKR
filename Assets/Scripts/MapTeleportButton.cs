using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTeleportButton : CurrencyDependent
{
    // Start is called before the first frame update

    public Vector3 teleportLocation;
    public string sceneToLoad;
    public GameObject MapPanel;
    [SerializeField] private ScreenFader _fader;
    [SerializeField] private int TravelCost;
    [SerializeField] private CurrencyUICounter CostCounter;

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
            MapPanel.SetActive(false);
            _fader.FadeToBlack(4f, FinishedFadingToBlack);


        }
        else
        {
            //Can call an action here if we want to play an effect somewhere else to show u got no funds
        }

    }
    public void TeleportScene()
    {

        if (_TotalCurrency >= TravelCost)
        {
            PlayerPrefs.SetFloat("TeleportX", teleportLocation.x);
            PlayerPrefs.SetFloat("TeleportY", teleportLocation.y);
            PlayerPrefs.SetFloat("TeleportZ", teleportLocation.z);

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

        _fader.FadeFromBlack(4f, null);
    }
    private void FinishedFadingToBlack()
    {
        Actions.OrderCurrencyUpdate.InvokeAction(-TravelCost);
        Player.gameObject.transform.position = teleportLocation;
        
        _fader.FadeToBlack(2f, FinishedFadingFromBlack);


    }
}
