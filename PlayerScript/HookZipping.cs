using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookZipping : MonoBehaviour
{
    [SerializeField] GameObject prefabChain;
    public Vector2 scaleTarget;
    float t = 0;
    public float duration = 2.0f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(SizeChanging());
        }
    }

    IEnumerator SizeChanging()
    {
        t += Time.deltaTime / duration;
        t = Mathf.Clamp(t, 0.0f, 1.0f);
        Quaternion originalRotation = Quaternion.Euler(0f,0f,0f) ;
        GameObject newChain = Instantiate(prefabChain, transform.position, originalRotation );
        Vector2 originalScale = prefabChain.transform.localScale;  
        newChain.transform.localScale = Vector2.Lerp(originalScale, scaleTarget, t);
        
        
        
        yield return null; 
    }
}
