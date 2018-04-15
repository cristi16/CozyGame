using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Listen")]
public class ListenDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return HeardNoise(controller);
    }

    bool HeardNoise(StateController controller)
    {
        Collider[] colliders = Physics.OverlapSphere(controller.transform.position, controller.enemyStats.listenForNoiseRadius, 1 << LayerMask.NameToLayer("Players"));
        if (colliders.Length > 0)
        {
            IGenerateNoise loudestNoiseSource = null;
            float loudestNoise = float.MinValue;

            foreach (Collider collider in colliders)
            {
                IGenerateNoise noiseSource = collider.GetComponent<IGenerateNoise>();
                if (noiseSource != null)
                {
                    float noise = noiseSource.GetNoiseData().amount;
                    float distance = Vector3.Distance(new Vector3(collider.transform.position.x, 0f, collider.transform.position.z),
                                    new Vector3(controller.transform.position.x, 0f, controller.transform.position.z));

                    float rollOffAmount = controller.enemyStats.noiseRolloff.Evaluate(distance / controller.enemyStats.listenForNoiseRadius);
                    noise -= noise * rollOffAmount;

                    if (noise > loudestNoise)
                    {
                        loudestNoise = noise;
                        loudestNoiseSource = noiseSource;
                    }
                }
            }

            if (loudestNoiseSource != null)
            {
                controller.followNoiseTarget = loudestNoiseSource.GetNoiseData().source;
                return true;
            }

            return false;
        }

        return false;
    }

}