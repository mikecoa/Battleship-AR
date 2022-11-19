using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void PlayParticle()
    {
        particleSystem.Play();
    }
}