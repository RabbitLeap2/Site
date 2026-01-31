using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnecUIScript : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private string nextSceneName = "GameScene";

    void Start()
    {
        hostButton.onClick.AddListener(OnHostButtonClick);
        clientButton.onClick.AddListener(OnClientButtonClick);
    }

    private void OnClientButtonClick()
    {
        // Conecta como client
        NetworkManager.Singleton.StartClient();
    }

    private void OnHostButtonClick()
    {
        // Callback para spawn de players quando um cliente se conecta
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

        // Inicia o host
        if (NetworkManager.Singleton.StartHost())
        {
            // Carrega a próxima cena via Netcode
            LoadNextScene();
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.IsHost)
        {
            // Spawn do player do cliente que acabou de conectar
            SpawnPlayer(clientId);
        }

        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
    }

    private void LoadNextScene()
    {
        NetworkManager.Singleton.SceneManager.LoadScene(nextSceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    private void SpawnPlayer(ulong clientId)
    {
        // Prefab do player precisa estar configurado no NetworkManager (PlayerPrefab)
        var playerPrefab = NetworkManager.Singleton.NetworkConfig.PlayerPrefab;

        if (playerPrefab != null)
        {
            // Instancia o player como NetworkObject
            var playerInstance = Instantiate(playerPrefab);

            // Spawna o player na rede para o client específico
            playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
        }
        else
        {
            Debug.LogError("PlayerPrefab não configurado no NetworkManager!");
        }
    }

    void Awake()
    {
        if (NetworkManager.Singleton != null)
        {
            DontDestroyOnLoad(NetworkManager.Singleton.gameObject);
        }
    }
}
