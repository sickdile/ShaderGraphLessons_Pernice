using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpponentBehaviout : MonoBehaviour
{
    MeshRenderer myRenderer;
    [SerializeField] float dissolveSpeed = 1.0f;

    Vector2 pos;
    private void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
    }
    public void Kill(InputAction.CallbackContext ctx)
    {
        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
        Physics.Raycast(ray, out RaycastHit info);
        if (info != null)
        {
            if (info.collider.CompareTag("Opponent"))
            {
                Debug.Log("Toccato Opponent");
                StartCoroutine(enumerator());
            }
        }
    }

    public void OnMousePositionChanged(InputAction.CallbackContext ctx)
    {
        pos = ctx.ReadValue<Vector2>();
    }

    IEnumerator enumerator()
    {
        while (myRenderer.material.GetFloat("_DissolveAmount") > 0)
        {
            float newDissolve = myRenderer.material.GetFloat("_DissolveAmount") + dissolveSpeed * 0.01f;
            myRenderer.material.SetFloat("_DissolveAmount", newDissolve);
            if(myRenderer.material.GetFloat("_DissolveAmount")<= 0) Destroy(gameObject);    
            yield return null;
        }
    }
}
