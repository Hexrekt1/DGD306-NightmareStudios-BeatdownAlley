using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ManualPlayerJoiner : MonoBehaviour
{
    public GameObject playerWPrefab;
    public GameObject playerGPrefab;

    public Transform spawnPointW;
    public Transform spawnPointG;

    private int playerCount = 0;

    void Update()
    {
        
        foreach (var gamepad in Gamepad.all)
        {
            if (!IsDevicePaired(gamepad) && playerCount < 2 && AnyButtonPressed(gamepad))
            {
                SpawnPlayer(gamepad);
            }
        }

        
        foreach (var joystick in Joystick.all)
        {
            if (!IsDevicePaired(joystick) && playerCount < 2 && AnyButtonPressed(joystick))
            {
                SpawnPlayer(joystick);
            }
        }
    }

    private void SpawnPlayer(InputDevice device)
    {
        GameObject prefabToSpawn = playerCount == 0 ? playerWPrefab : playerGPrefab;
        Transform spawnPoint = playerCount == 0 ? spawnPointW : spawnPointG;

        var playerInput = PlayerInput.Instantiate(
            prefabToSpawn,
            playerIndex: playerCount,
            controlScheme: null,
            pairWithDevice: device);

        playerInput.transform.position = spawnPoint.position;
        playerInput.transform.rotation = spawnPoint.rotation;

        Debug.Log($"Spawned Player {playerCount + 1} with device {device.displayName}");

        playerCount++;
    }

    private bool IsDevicePaired(InputDevice device)
    {
        foreach (var player in PlayerInput.all)
        {
            foreach (var pairedDevice in player.user.pairedDevices)
            {
                if (pairedDevice.deviceId == device.deviceId)
                    return true;
            }
        }
        return false;
    }

    private bool AnyButtonPressed(InputDevice device)
    {
        foreach (var control in device.allControls)
        {
            if (control is ButtonControl button && button.wasPressedThisFrame)
            {
                return true;
            }
        }
        return false;
    }
}
