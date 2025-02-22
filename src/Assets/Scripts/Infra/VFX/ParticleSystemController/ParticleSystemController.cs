using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Perikan.Infra.VFX
{
    public class ParticleSystemController : MonoBehaviour
    {
        [SerializeField] private float _startDelay = 0f;
        [SerializeField] private ParticleSystem[] _particlesExceptions;
        [ReadOnly,SerializeField] private ParticleSystem[] _particleSystems;

        void Awake()
        {
            InitializeParticleSystems();
        }

        private void InitializeParticleSystems()
        {
            ParticleSystem[] allParticles = GetComponentsInChildren<ParticleSystem>();
            _particleSystems = allParticles.Except(_particlesExceptions).ToArray();
        }

        private void OnValidate()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                InitializeParticleSystems();
                ApplyStartDelay();
            }
        }

        public void Play()
        {
            foreach (var ps in _particleSystems)
            {
                ps.Play();
            }
        }

        public void Stop()
        {
            foreach (var ps in _particleSystems)
            {
                ps.Stop();
            }
        }

        public void ApplyStartDelay()
        {
            foreach (var ps in _particleSystems)
            {
                var main = ps.main;
                main.startDelay = _startDelay;
            }
        }
    }

    /*-----------------------------------------------------------*/
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
