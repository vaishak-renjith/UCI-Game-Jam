using Unity.Cinemachine;
using UnityEngine;

public class ShipSwitcher : MonoBehaviour
{
    public CinemachineCamera virtualCamera;
    public Transform[] ships;
    private int currentShipIndex = 0;

    void Start()
    {
        SetActiveShip(currentShipIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentShipIndex = (currentShipIndex + 1) % ships.Length;
            SetActiveShip(currentShipIndex);
        }
    }

    void SetActiveShip(int index)
    {
        for (int i = 0; i < ships.Length; i++)
        {
            var controller = ships[i].GetComponent<ShipController>();
            if (controller != null)
                controller.canMove = (i == index);

            var shooter = ships[i].GetComponent<ShipShooter>();
            if (shooter != null)
                shooter.canShoot = (i == index);
        }
        if (virtualCamera != null)
            virtualCamera.Follow = ships[index];
    }
}