using UnityEngine;

public class SpellVisualizer : MonoBehaviour
{
    public float coneAngle = 45f;
    public float coneRange = 5f;
    public bool showSpellCone = true;

    private void OnDrawGizmosSelected()
    {
        if (!showSpellCone) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, coneRange);

        Vector3 forward = transform.forward;

        Quaternion leftRayRotation = Quaternion.AngleAxis(-coneAngle, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(coneAngle, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * forward;
        Vector3 rightRayDirection = rightRayRotation * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftRayDirection * coneRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * coneRange);

    }
}
