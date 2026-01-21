using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class MouseBehaviour : MonoBehaviour
{
    MeshRenderer myRenderer;

    private void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
    }


    public void OnMouseMoveX(InputAction.CallbackContext context)
    {
        float a = context.ReadValue<float>();
        a = math.remap(-Screen.width * 0.5f, Screen.width * 0.5f, 0, 1, a);
        Debug.Log("Changing mouse: " + a);
         myRenderer.sharedMaterial.SetFloat("_Amplitude", a);
       // myRenderer.material.SetFloat("Amplitude", a);
        Debug.Log("AMPLITUDE IS " + myRenderer.sharedMaterial.GetFloat("_Amplitude"));

    }

    public void OnMouseMoveY(InputAction.CallbackContext ctx)
    {
        float a = ctx.ReadValue<float>();
        a = math.remap(-Screen.height * 0.5f, Screen.height * 0.5f, 0, 1, a);
        myRenderer.sharedMaterial.SetFloat("_Velocity", a);
    }
}
