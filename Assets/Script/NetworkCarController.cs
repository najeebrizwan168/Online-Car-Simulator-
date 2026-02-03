using Fusion;
using UnityEngine;

public class NetworkCarController : NetworkBehaviour
{
    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            CameraController cam = Camera.main.GetComponent<CameraController>();
            if (cam != null)
            {
                // Find the child object that has your meshes (e.g., "Meshes" or "Interpolation Target")
                Transform visualPart = transform.Find("Meshes");

                // If it can't find "Meshes", it will just use the root transform
                cam.SetTarget(visualPart != null ? visualPart : this.transform);
            }
        }
    }
}