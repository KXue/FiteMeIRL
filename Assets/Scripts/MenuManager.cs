using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public enum GameState {Title, Networking, Pause, Game, GameOver};
public class MenuManager : MonoBehaviour {
    public Transform[] m_UI;
    public GameState m_currentState;
    public InputField m_IPField;
    public Image m_HealthBar;
    public Image HealthBar{
        get{
            return m_HealthBar;
        }
    }
	private static MenuManager instance;
    // Static singleton property
    public static MenuManager Instance
    {
        get
        {
            return instance ?? (instance = new GameObject("Singleton").AddComponent<MenuManager>());
        }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        m_IPField.text = NetworkManager.singleton.networkAddress;
    }
    public void ToTitle(){
        HideAllMenus();
        ShowMenu(GameState.Title);
    }
    public void ToNetworking(){
        HideAllMenus();
        ShowMenu(GameState.Networking);
    }
    public void HostGame(){
        SetNetworkingConfig();
        NetworkManager.singleton.StartHost();
        HideAllMenus();
        EnableControls();
        ShowMenu(GameState.Game);
    }
    public void JoinGame(){
        SetNetworkingConfig();
        NetworkManager.singleton.StartClient();
        HideAllMenus();
        EnableControls();
        ShowMenu(GameState.Game);
    }
    public void ToGameOver(){
        HideAllMenus();
        DisableControls();
        ShowMenu(GameState.GameOver);
    }
    public void PauseGame(){
        DisableControls();
        HideAllMenus();
        ShowMenu(GameState.Pause);
    }
    public void UnpauseGame(){
        EnableControls();
        HideAllMenus();
        ShowMenu(GameState.Game);
    }
    public void QuitGame(){
        Disconnect();
        ToTitle();
    }
    public void QuitProgram(){
        Application.Quit();
    }
    public void PlayAgain(){
        
        Transform startPosition = NetworkManager.singleton.GetStartPosition();
        Instantiate(NetworkManager.singleton.playerPrefab, startPosition.position, startPosition.rotation);
        UnpauseGame();
    }
    void Disconnect(){
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopClient();
    }
    void HideAllMenus(){
        foreach(Transform t in m_UI){
            t.gameObject.SetActive(false);
        }
    }
    void ShowMenu(GameState state){
        if(state != GameState.GameOver){
            m_UI[(int)state].gameObject.SetActive(true);
        }
        m_currentState = state;
    }
    void SetNetworkingConfig(){
        string IP = m_IPField.text;
        NetworkManager.singleton.networkAddress = IP;
    }
    void DisableControls(){
        List<UnityEngine.Networking.PlayerController> localPlayers = ClientScene.localPlayers;
        foreach(UnityEngine.Networking.PlayerController localPlayer in localPlayers){
            if(localPlayer.gameObject != null){
                SetControlsEnabled(localPlayer.gameObject, false);
            }
        }
        SetCursorState(CursorLockMode.None);
    }
    void EnableControls(){
        List<UnityEngine.Networking.PlayerController> localPlayers = ClientScene.localPlayers;
        foreach(UnityEngine.Networking.PlayerController localPlayer in localPlayers){
            if(localPlayer.gameObject != null){
                SetControlsEnabled(localPlayer.gameObject, true);
            }
        }
        SetCursorState(CursorLockMode.Locked);
    }
    void SetControlsEnabled(GameObject playerObject, bool enabled){
        BaseMovement movementScript = playerObject.GetComponent<BaseMovement>();
        BattleScript combatScript = playerObject.GetComponent<BattleScript>();
        ThirdPersonCamera cameraScript = Camera.main.GetComponent<ThirdPersonCamera>();

        if(cameraScript != null){
            cameraScript.enabled = enabled;
        }
        if(movementScript != null){
            movementScript.enabled = enabled;
        }
        if(combatScript != null){
            combatScript.enabled = enabled;
        }
    }
    void SetCursorState(CursorLockMode wantedMode){
        Cursor.lockState = wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Cancel")){
            if(m_currentState == GameState.Game){
                PauseGame();
            }
            else if(m_currentState == GameState.Pause){
                UnpauseGame();
            }
        }
	}
}
