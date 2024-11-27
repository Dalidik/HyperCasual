using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 initRotation;

    private void Awake()
    {
        initRotation = transform.eulerAngles;
    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x - 0.4f, player.position.y, player.position.z + 0.6f);

        if (GameEvents.instance.gameWon.Value || GameEvents.instance.gameLost.Value) return;
        transform.eulerAngles = new Vector3(player.eulerAngles.x -10f + initRotation.x, player.eulerAngles.y + initRotation.y, 0);
    }
}