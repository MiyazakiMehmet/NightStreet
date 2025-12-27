using UnityEngine;
using UnityEngine.InputSystem; // YENÝ EKLENDÝ

[RequireComponent(typeof(CharacterController))]
public class SimpleController : MonoBehaviour
{
    [Header("Ayarlar")]
    public float yurumeHizi = 6f;
    public float mouseHassasiyeti = 15f; // Yeni sistemde bu deðer daha düþük olmalý
    public float yerCekimi = -9.81f;

    [Header("Referanslar")]
    public Transform playerCamera;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Hareket ve Bakýþ fonksiyonlarýný çaðýr
        HareketEt();
        EtrafaBak();
    }

    void HareketEt()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // --- YENÝ SÝSTEM GÝRDÝLERÝ ---
        Vector2 inputVector = Vector2.zero;
        if (Keyboard.current.wKey.isPressed) inputVector.y += 1;
        if (Keyboard.current.sKey.isPressed) inputVector.y -= 1;
        if (Keyboard.current.aKey.isPressed) inputVector.x -= 1;
        if (Keyboard.current.dKey.isPressed) inputVector.x += 1;
        // -----------------------------

        Vector3 move = transform.right * inputVector.x + transform.forward * inputVector.y;
        controller.Move(move * yurumeHizi * Time.deltaTime);

        velocity.y += yerCekimi * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void EtrafaBak()
    {
        // --- YENÝ SÝSTEM MOUSE OKUMA ---
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float mouseX = mouseDelta.x * mouseHassasiyeti * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseHassasiyeti * Time.deltaTime;
        // -------------------------------

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}