using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Perikan.Infra.VFX
{
    public class ParticleSystemController : MonoBehaviour
    {
        [SerializeField] protected float _startDelay = 0f;
        [SerializeField] protected ParticleSystem[] _particlesExceptions;
        [ReadOnly,SerializeField] protected ParticleSystem[] _particleSystems;

        void Awake()
        {
            InitializeParticleSystems();
        }

        private void InitializeParticleSystems()
        {
            _particleSystems = GetComponentsInChildren<ParticleSystem>();
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
            ParticleSystem[] ParticleSystemToChange = _particleSystems.Except(_particlesExceptions).ToArray();
            foreach (var ps in ParticleSystemToChange)
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
