namespace Citadel.Unity.Management
{
    using System.Collections.Generic;
    using UnityEngine;
    [RequireComponent(typeof(Animator))]
    public sealed class AnimationController : MonoBehaviour
    {
        private Animator _animator;
        private List<IEventUpdateHandler> _observers;
        public void Install()
        {
            _animator = GetComponent<Animator>();
            _observers = new();
        }
        public void Play(string name)
        {
            _animator.Play(name);
        }
        public void Notify(string name)
        {
            _observers.ForEach(observer => observer.EventUpdate(name));
        }
        public void Add(IEventUpdateHandler observer)
        {
            _observers.Add(observer);
        }
        public void Remove(IEventUpdateHandler observer)
        {
            _observers.Remove(observer);
        }
    }
}