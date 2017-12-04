using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MainNetworkManager : NetworkManager {
	/// <summary>
	/// Called on the client when the connection was lost or you disconnected from the server.
	/// </summary>
	/// <param name="info">NetworkDisconnection data associated with this disconnect.</param>
	void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		MenuManager.Instance.QuitGame();
	}
}
