using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creamBOdy : MonoBehaviour
{
    public int currentIndex = 1;
    private SkinnedMeshRenderer skinnedMeshRendererrenderer;
    

    [SerializeField] private Material myTexture;
    [SerializeField] private Transform scopePoint;


    void Start()
    {
        skinnedMeshRendererrenderer = GetComponent<SkinnedMeshRenderer>();

        myTexture = skinnedMeshRendererrenderer.material;
    }

    

    public Vector3 GetPos()
    {
        StartCoroutine(blendRutine());
        return scopePoint.position;
    }

    public Material getTextre()
    {
        return myTexture;
    }





    private IEnumerator blendRutine()
    {
        float elapsedTime = 0f;
        float duration = 1.2f;
        float blend = skinnedMeshRendererrenderer.GetBlendShapeWeight(currentIndex);

        while (elapsedTime < duration)
        {
            
            blend = Mathf.MoveTowards(blend, 100, elapsedTime / duration);
            skinnedMeshRendererrenderer.SetBlendShapeWeight(currentIndex,blend);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    
        blend = 100;
        skinnedMeshRendererrenderer.SetBlendShapeWeight(currentIndex,blend);

        currentIndex += 1;


    
    }


}
