using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpponentBehaviout : MonoBehaviour
{
    [SerializeField] float dissolveSpeed = 1.0f;

    Vector2 pos;

    bool killInput = false;

    public InputAction action;

    public Material quellaCosaFuori;

    [SerializeField] float VignetteIntensityDefault;
    private void Start()
    {
        MyActions myActions = new MyActions();
        action = myActions.FindAction("Kill");
        action.Enable();
        action.performed += ctx => Ciao();
        quellaCosaFuori.SetFloat("_VignetteIntensity", VignetteIntensityDefault);
    }
    void Ciao()
    {

    }
    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) killInput = true;
        Debug.Log("HAI CLICCATO!!");
    }
    Coroutine hadesCR;
    public void Kill(InputAction.CallbackContext ctx)
    {
        if (killInput)
        {
            Ray ray = Camera.main.ScreenPointToRay(ctx.ReadValue<Vector2>());
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if (hit.collider != null && hit.collider.CompareTag("Opponent"))
            {
                Debug.Log("Toccato Opponent");
                if (hadesCR != null)
                {
                    StopCoroutine(hadesCR);
                }

                StartCoroutine(enumerator(hit.collider.gameObject.GetComponent<Renderer>()));
                StartCoroutine(viewEnum(quellaCosaFuori));
            }
        }

    }


    IEnumerator enumerator(Renderer renderer)
    {
        killInput = false;

        while (renderer.material.GetFloat("_DissolveAmount") < 1.0f)
        {
            float newDissolve = renderer.material.GetFloat("_DissolveAmount") + (dissolveSpeed);
            renderer.material.SetFloat("_DissolveAmount", newDissolve);
            yield return null;
        }
        while (renderer.material.GetFloat("_DissolveAmount") > 0.0f)
        {
            float newDissolve = renderer.material.GetFloat("_DissolveAmount") - (dissolveSpeed);
            renderer.material.SetFloat("_DissolveAmount", newDissolve);
            yield return null;
        }
    }


    IEnumerator viewEnum(Material material)
    {
        while (material.GetFloat("_VignetteIntensity") > 0)
        {
            float newIntensity = material.GetFloat("_VignetteIntensity")  -  dissolveSpeed;
            material.SetFloat("_VignetteIntensity", newIntensity);
            yield return null;
        }
    }
}
