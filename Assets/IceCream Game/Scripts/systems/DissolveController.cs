using UnityEngine;

public class DissolveController : MonoBehaviour
{
    private Renderer[] renderers;
    
    private MaterialPropertyBlock propertyBlock;
    public float dissolveAmount = 0f;
    private float targetDissolveAmount = 0f;
    [SerializeField] private float duration = 2f;

    [SerializeField] private bool desable_nonDissolve = false;

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
        propertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        dissolveAmount = Mathf.MoveTowards(dissolveAmount, targetDissolveAmount, Time.deltaTime * (targetDissolveAmount - dissolveAmount) / duration);

        // Update the dissolve amount property in the shader property block
        propertyBlock.SetFloat("_desloveAmount", dissolveAmount / 100f);

        // Apply the property block to all renderers with shared materials
        foreach (Renderer renderer in renderers)
        {
        
           
            renderer.SetPropertyBlock(propertyBlock);
           
        }
    }

    public void StartDissolve()
    {
        Start();
        dissolveAmount = 0f;
        targetDissolveAmount = 100f;

        GameObject vfx = ObjectPool.instance.SpwanFromPool("disolvePuff",transform.position,Quaternion.identity);

        if(desable_nonDissolve)
        {
            foreach (Renderer renderer in renderers)
            {
            
                if (!renderer.sharedMaterial.HasProperty("_desloveAmount"))
                {
                    // If the material does not have the property, disable the GameObject
                    renderer.gameObject.SetActive(false);
                }
            }
        }
      //  vfx.GetComponent<ParticleSystem>().Play();
    }
}
