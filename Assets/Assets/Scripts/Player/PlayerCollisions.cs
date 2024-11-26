using UnityEngine;
using DG.Tweening;
using PathCreation.Examples;
using TMPro;
using UniRx;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _poor;
    [SerializeField] private GameObject _middle;
    [SerializeField] private GameObject _rich;
    [SerializeField] private PathFollower _pathFollower;
    [SerializeField] private int _dollar = 5;
    [SerializeField] private int _badChoice = 10;
    
    private Animator _playerAnim;
    private TextMeshProUGUI _sliderText;
    private readonly float _goodChoiceSlider = 0.05f;
    private readonly float _badChoiceSlider = 0.100f;
    private int _moneyCount = 0;

    
    private void Awake()
    {
        _playerAnim = GetComponent<Animator>();
        _slider.value = 0.01f;
        _sliderText = _slider.GetComponentInChildren<TextMeshProUGUI>();
        _moneyText.text = "0";
        _moneyCount = 0;
        _poor.SetActive(true);
        _middle.SetActive(false);
        _rich.SetActive(false);
    }

    private void Update()
    {
        if (_slider.value < 0.500f)
        {
            _sliderText.text = "Бедный";
            _poor.SetActive(true);
            _middle.SetActive(false);
            _rich.SetActive(false);
            _playerAnim.SetBool("Middle", false);
        }

        if (_slider.value > 0.500f)
        {
            _sliderText.text = "Состоятельный";
            _poor.SetActive(false);
            _middle.SetActive(true);
            _rich.SetActive(false);
            _playerAnim.SetBool("Middle", true);
            _playerAnim.SetBool("Poor", false);
        }

        if (_slider.value > 0.800f)
        {
            _sliderText.text = "Богатый";
            _rich.SetActive(true);
            _poor.SetActive(false);
            _middle.SetActive(false);
            _playerAnim.SetBool("Rich", false);
            _playerAnim.SetBool("Middle", true);
            ParticleManager.instance.PlayParticle(2, transform.position);
        }
        
        if (_slider.value <= 0)
        {
            GameEvents.instance.gameLost.SetValueAndForceNotify(true);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Money")
        {
            _slider.value = _slider.value + _goodChoiceSlider;
            _moneyCount = _moneyCount + _dollar;
            _moneyText.text = _moneyCount.ToString();
            other.GetComponent<Collider>().enabled = false;
            ParticleManager.instance.PlayParticle(1, transform.position);
             Destroy(other.gameObject); 
        }

        if (other.tag == "Bottle")
        {
            CheckHit();
            _slider.value = _slider.value - _badChoiceSlider;
            _moneyCount = _moneyCount - _badChoice;
            _moneyText.text = _moneyCount.ToString();
            other.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject); 
        }

        if (other.tag == "Finish")
        {
            GameEvents.instance.gameWon.SetValueAndForceNotify(true);
            _pathFollower.speed = 0;
        }
    }

    private void CheckHit()
    {
        Handheld.Vibrate();
        Camera.main.transform.DOShakePosition(0.1f, 0.5f, 5);
        ParticleManager.instance.PlayParticle(0, transform.position);
    }
}