using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 150f;
    private InputSystem_Actions controls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        Debug.Log(controls.Player.Move);
    }
    // Update is called once per frame
    private void Update()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        float horizzontalInput = moveInput.x;

        transform.Rotate(Vector3.up, horizzontalInput * Time.deltaTime * rotationSpeed);
    }
}
