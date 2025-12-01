using UnityEngine;

public class LanderVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem middleThrusterParticleSystem;
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;

    private Lander lander;

    private void Awake()
    {
        lander=GetComponent<Lander>();

        lander.OnUpForce += Lander_OnUpForce;
        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnRightForce += Lander_OnRightForce;
        lander.OnBeforeForce += Lander_OnBeforeForce;

        enableParticleSystem(leftThrusterParticleSystem, false);
        enableParticleSystem(middleThrusterParticleSystem, false);
        enableParticleSystem(rightThrusterParticleSystem, false);
    }

    private void Lander_OnBeforeForce(object sender, System.EventArgs e)
    {
        enableParticleSystem(leftThrusterParticleSystem, false);
        enableParticleSystem(middleThrusterParticleSystem, false);
        enableParticleSystem(rightThrusterParticleSystem, false);
    }

    private void Lander_OnRightForce(object sender, System.EventArgs e)
    {
        enableParticleSystem(leftThrusterParticleSystem, true);
        
    }

    private void Lander_OnLeftForce(object sender, System.EventArgs e)
    {
        enableParticleSystem(rightThrusterParticleSystem, true);
       
    }

    private void Lander_OnUpForce(object sender, System.EventArgs e)
    {
        enableParticleSystem(leftThrusterParticleSystem, true);
        enableParticleSystem(middleThrusterParticleSystem, true);
        enableParticleSystem(rightThrusterParticleSystem, true);
    }

    private void enableParticleSystem(ParticleSystem particleSystem, bool enable)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enable;

    }
}
