using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerLogic : NetworkBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        Debug.Log("spawned player");
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        GetPlayerVisualsServerRpc();
    }
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            GetPlayerVisualsServerRpc();
        }
    }
    [ServerRpc]
    private void GetPlayerVisualsServerRpc()
    {
        print("excecuting server rpc");
        transform.position = PlayerVisualsAssigner.Instance.GetRandomPositionOnHost().position;
        _spriteRenderer.sprite = PlayerVisualsAssigner.Instance.GetRandomSpriteOnHost();

        Vector3 clientPosition = PlayerVisualsAssigner.Instance.GetRandomPositionOnClient().position;
        int clientSpriteID = PlayerVisualsAssigner.Instance.GetRandomSpriteIDOnClient();

        GetPlayerVisualsClientRpc(clientPosition, clientSpriteID);
    }
    [ClientRpc]
    private void GetPlayerVisualsClientRpc(Vector3 playerPosition, int playerSpriteID)
    {
        print("executing client rpc");
        if (!IsServer)
        {
            transform.position = playerPosition;
            gameObject.GetComponent<SpriteRenderer>().sprite = PlayerVisualsAssigner.Instance.GetSpriteByID(playerSpriteID);
        }
    }
}
