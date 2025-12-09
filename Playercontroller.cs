
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField]
    [Range(0.5f, 2f)]
    float mouseSense = 1;
    [SerializeField]
    [Range(-20, -10)]
    int lookUp = -15;
    [SerializeField]
    [Range(15, 25)]
    int lookDown = 20;
    // Una variable booleana que rastreará el estado actual del jugador
    public bool isSpectator;
    // Script de vuelo de cámara libre
    [SerializeField] float speed = 50f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {

        float rotateX = Input.GetAxis("Mouse X") * mouseSense;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense;
        if (!isSpectator)
        {
            Vector3 rotCamera = transform.rotation.eulerAngles;
            Vector3 rotPlayer = player.transform.rotation.eulerAngles;
            rotCamera.x = (rotCamera.x > 180) ? rotCamera.x - 360 : rotCamera.x;
            rotCamera.x = Mathf.Clamp(rotCamera.x, lookUp, lookDown);
            rotCamera.x -= rotateY;

            rotCamera.z = 0;
            rotPlayer.y += rotateX;
            transform.rotation = Quaternion.Euler(rotCamera);
            player.transform.rotation = Quaternion.Euler(rotPlayer);
        }
        else
        {
            // Obtener la rotación actual de la cámara
            Vector3 rotCamera = transform.rotation.eulerAngles;
            // Cambiar la rotación de la cámara según el movimiento del mouse
            rotCamera.x -= rotateY;
            rotCamera.z = 0;
            rotCamera.y += rotateX;
            transform.rotation = Quaternion.Euler(rotCamera);
            // Leer las pulsaciones de teclas WASD
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // Configurar el vector de movimiento de la cámara
            Vector3 dir = transform.right * x + transform.forward * z;
            // Cambiar la posición de la cámara
            transform.position += dir * speed * Time.deltaTime;

            // Si el jugador presiona la tecla Escape, entonces
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Si el cursor del mouse está bloqueado, entonces...
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    // Desbloquear el cursor
                    Cursor.lockState = CursorLockMode.None;
                }
                // De lo contrario...
                else
                {
                    // Bloquear el cursor
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
}
