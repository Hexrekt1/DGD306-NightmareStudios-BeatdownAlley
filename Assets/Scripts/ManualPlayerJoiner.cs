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
        // Gamepad kontrol�
        foreach (var gamepad in Gamepad.all)
        {
            if (gamepad.startButton.wasPressedThisFrame)
            {
                if (!IsDevicePaired(gamepad) && playerCount < 2)
                {
                    SpawnPlayer(gamepad);
                }
            }
        }

        // Joystick kontrol� (�rnek: button1 "start" olarak varsay�lm��t�r)
        foreach (var joystick in Joystick.all)
        {
            if (!IsDevicePaired(joystick) && playerCount < 2)
            {
                // D��menin var olup olmad���n� kontrol et
                if (joystick.TryGetChildControl<ButtonControl>("button7") is ButtonControl startButton)
                {
                    if (startButton.wasPressedThisFrame)
                    {
                        SpawnPlayer(joystick);
                    }
                }
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
            controlScheme: "Gamepad", // Joystick i�in de bu �al��abilir, ancak gerekirse ay�rabilirim
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
}
