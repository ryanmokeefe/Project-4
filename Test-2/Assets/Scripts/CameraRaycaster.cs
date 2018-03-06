// Raycasting from Udemy Tutorial


using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

	[SerializeField]
    float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit m_hit;
	// public getter to get the object hit by Raycast
    public RaycastHit hit
    {
        get { return m_hit; }
    }

    Layer m_layerHit;
    public Layer layerHit
    {
        get { return m_layerHit; }
    }

	// must declare new DELEGATE type
	public delegate void OnLayerChange();
	// instantiate new Delegate Observer SET
	public OnLayerChange layerChangeObservers;

	// THIS must conform to the same signature as declared Delegate type above; ie: no params, void.
	void LayerChangeHandler() {
		print("something");
	}




    void Start() 
    {
        viewCamera = Camera.main;
		// make sure to ADD the observers set, not overwrite with =.
		layerChangeObservers += LayerChangeHandler;

		// call delegate
		layerChangeObservers();
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
                m_layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        m_hit.distance = distanceToBackground;
        m_layerHit = Layer.RaycastEndStop;
    }

	RaycastHit? RaycastForLayer(Layer layer) // ? = nullable param (allowed to return null)
    {
		// turn layer into int, then bit shift, then turn screen point to a ray...
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

		// define raycast, use previous ray as param, provide max layer to raycast, provide layermask
        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        // if hit, return hit, if not, return nothing
		if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
