using UnityEngine;

public class UnscaledParticlePlayer : MonoBehaviour
{
    private ParticleSystem ps;
    private float unscaledTime;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        unscaledTime = 0f;

        if (ps != null)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    void Update()
    {
        if (ps == null) return;

        float deltaTime = Time.unscaledDeltaTime;
        unscaledTime += deltaTime;

        ps.Simulate(deltaTime, true, false); // simulate in unscaled time
        ps.Play(); // needed to update internal state

        if (unscaledTime > ps.main.duration + ps.main.startLifetime.constantMax)
        {
            Destroy(gameObject); // auto-cleanup
        }
    }
}