using UniRx;
using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }
    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);

        GameEvents.instance.gameStarted.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                _animator.SetBool("Poor", value);
            })
            .AddTo(subscriptions);


        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if(value)
                    _animator.SetTrigger("Stop");
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                    _animator.SetTrigger("Stop");
            })
            .AddTo(subscriptions);
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }
}