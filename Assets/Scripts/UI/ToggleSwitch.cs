using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    [Header("Slider setup")]
    [SerializeField, Range(0, 1f)]
    protected float sliderValue;
    public bool CurrentValue { get; private set; }

    private bool _previousValue;
    private Slider _slider;

    [Header("Animation")]
    [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f;
    [SerializeField]
    private AnimationCurve slideEase =
        AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine _animateSliderCoroutine;

    [Header("Events")]
    [SerializeField] public UnityEvent onToggleOn;
    [SerializeField] public UnityEvent onToggleOff;

    private ToggleSwitchGroupManager _toggleSwitchGroupManager;

    protected Action transitionEffect;

    protected virtual void OnValidate()
    {
        SetupToggleComponents();

        _slider.value = sliderValue;
    }
    private void Start()
    {
        SetupSliderComponent();

        string key = "ToggleState_" + gameObject.name;
        bool savedState = PlayerPrefs.GetInt(key, 0) == 1;
        Debug.Log($"Loaded Toggle State for {gameObject.name}: {savedState}");

        StartCoroutine(DelayedStateSetup(savedState));
    }

    private IEnumerator DelayedStateSetup(bool savedState)
    {
        yield return null;

        SetStateWithoutAnimation(savedState);
    }

    private void SetStateWithoutAnimation(bool state)
    {
        CurrentValue = state;
        sliderValue = state ? 1 : 0;
        _slider.value = sliderValue;
    }

    private void SetupToggleComponents()
    {
        if (_slider != null)
            return;

        SetupSliderComponent();
    }

    private void SetupSliderComponent()
    {
        _slider = GetComponent<Slider>();

        if (_slider == null)
        {
            Debug.Log("No slider found!", this);
            return;
        }

        _slider.interactable = false;
        var sliderColors = _slider.colors;
        sliderColors.disabledColor = Color.white;
        _slider.colors = sliderColors;
        _slider.transition = Selectable.Transition.None;
    }

    public void SetupForManager(ToggleSwitchGroupManager manager)
    {
        _toggleSwitchGroupManager = manager;
    }

    private void Awake()
    {
        SetupSliderComponent();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }


    private void Toggle()
    {
        if (_toggleSwitchGroupManager != null)
            _toggleSwitchGroupManager.ToggleGroup(this);
        else
            SetStateAndStartAnimation(!CurrentValue);
    }

    public void ToggleByGroupManager(bool valueToSetTo)
    {
        SetStateAndStartAnimation(valueToSetTo);
    }

    private void SetStateAndStartAnimation(bool state)
    {
        _previousValue = CurrentValue;
        CurrentValue = state;

        string key = "ToggleState_" + gameObject.name;
        PlayerPrefs.SetInt(key, CurrentValue ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log($"Saved Toggle State for {gameObject.name}: {CurrentValue}");

        if (_previousValue != CurrentValue)
        {
            if (CurrentValue)
                onToggleOn?.Invoke();
            else
                onToggleOff?.Invoke();
        }

        if (gameObject.activeInHierarchy)
        {
            if (_animateSliderCoroutine != null)
                StopCoroutine(_animateSliderCoroutine);

            _animateSliderCoroutine = StartCoroutine(AnimateSlider());
        }
        else
        {
            _slider.value = CurrentValue ? 1 : 0;
        }
    }

    private IEnumerator AnimateSlider()
    {
        float startValue = _slider.value;
        float endValue = CurrentValue ? 1 : 0;

        float time = 0;
        if (animationDuration > 0)
        {
            while (time < animationDuration)
            {
                time += Time.deltaTime;

                float lerpFactor = slideEase.Evaluate(time / animationDuration);
                _slider.value = sliderValue = Mathf.Lerp(startValue, endValue, lerpFactor);

                transitionEffect?.Invoke();

                yield return null;
            }
        }

        _slider.value = endValue;
    }
}
