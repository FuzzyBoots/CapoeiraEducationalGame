
using UnityEngine;

    [CreateAssetMenu(fileName = "MoveEntry", menuName = "ScriptableObjects/MoveEntry")]
    class MoveEntry : ScriptableObject
    {
        [SerializeField] AnimationClip _moveAnimation;
        [SerializeField] string _moveName;

        public AnimationClip GetAnimation()
        {
            return _moveAnimation;
        }

        public string GetName() { 
            return _moveName; 
        }
    }
