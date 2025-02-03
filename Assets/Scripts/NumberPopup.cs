using TMPro;
using UnityEngine;

public class NumberPopup : MonoBehaviour
{
    [Header("Disappearing")]
    [SerializeField,Tooltip("How long number sticks around before starting disappear")] 
    private float sustainTime = 0.4f;

    [SerializeField,Tooltip("How long number grows before sustain")] 
    private float growTime = 0.2f;

    [SerializeField,Tooltip("How fast number disappears after sustain")] 
    private float disappearSpeed = 5f;

    [Header("Font size")]
    [SerializeField] private int regularFontSize = 8;
    [SerializeField] private int criticalFontSize = 12;

    [Header("Scaling")]
    [SerializeField] private float increaseScaleAmount = 1;
    [SerializeField] private float decreaseScaleAmount = 1;

    [SerializeField,Tooltip("Should number 'land' by getting smaller at the end of grow")] 
    private bool land = false;

    [SerializeField,Range(0f, 1f),Tooltip("What percent of the grow should be the landing")] 
    private float landPercent = 0.4f;

    [SerializeField, Tooltip("Scale amount as landing happens")]
    private float landingDecreaseScaleAmount = 1;

    [Header("Movement")]
    [SerializeField,Tooltip("Direction and speed")] 
    private Vector3 initialMoveVector;
    [SerializeField] private Vector3 disappearMoveVector;
    [SerializeField,Tooltip("How fast the movement stops")] 
    private float moveReductionScalar = 8f;

    [SerializeField] private float growRotateAmount = 50;
    [SerializeField] private float disappearRotateAmount = -50;

    [Header("Color Override")]
    [SerializeField] private bool overrideRegularColor = false;
    [SerializeField] private Color newRegularColor = new Color(0, 0, 0, 1);
    [SerializeField] private bool overrideCriticalColor = false;
    [SerializeField] private Color newCriticalColor = new Color(0, 0, 0, 1);

    private static int sortingOrder;

    private TextMeshPro textMesh;
    private float growTimer;
    private float sustainTimer;

    private Color textColor;
    private string regularColorHex = "bf6b30";
    private string critColorHex = "9c0f0f";

    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * moveReductionScalar * Time.deltaTime;

        if(growTimer > 0)
        { // count down grow timer first
            growTimer -= Time.deltaTime;

            if(land && growTimer < growTime * landPercent)
            { // last little percentage of grow
                transform.localScale -= Vector3.one * landingDecreaseScaleAmount * Time.deltaTime;
            }
            else
            {
                transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
                transform.Rotate(new Vector3(0, 0, 1), growRotateAmount * Time.deltaTime);
            }
        }
        else 
        { // grow timer finished, count down sustain
            sustainTimer -= Time.deltaTime;
            if (sustainTimer < 0)
            {
                Disappearing();
            }
        }
    }

    public static NumberPopup Create(Vector3 position, int damageAmount, Vector3 inputMoveVec = default, bool isCriticalHit = false, bool invertRotate = false, PopupType type = PopupType.DEFAULT, Color color = default)
    {
        Transform popupPrefab = Assets.i.defaultPopup;

        float overrideFontSize = -1;
        if(!Utils.IsPositionInCameraBounds(position))
        {
            if (type == PopupType.DEFAULT) return null;

            // handle position in viewport context (0 to 1)
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(position);

            // clamp position to inside viewport
            if(viewportPos.x <= 0) viewportPos.x = 0.05f;
            else if(viewportPos.x >= 1) viewportPos.x = 0.95f;
            if (viewportPos.y <= 0) viewportPos.y = 0.05f;
            else if (viewportPos.y >= 1) viewportPos.y = 0.95f;

            // convert position back into world space
            position = Camera.main.ViewportToWorldPoint(viewportPos);

            inputMoveVec = -inputMoveVec; // come in towards the screen instead of away from player   
            overrideFontSize = 4f;
        }

        Transform damagePopupT = Instantiate(popupPrefab, position, Quaternion.identity);
        NumberPopup damagePopup = damagePopupT.GetComponent<NumberPopup>();
        damagePopup.Setup(damageAmount, inputMoveVec, isCriticalHit, invertRotate, overrideFontSize, color);

        return damagePopup;
    }

    public static NumberPopup Create(Vector3 position, float damageAmount, Vector3 inputMoveVec = default, bool isCriticalHit = false, bool invertRotate = false, PopupType type = PopupType.DEFAULT, Color color = default)
    {
        return Create(position,Mathf.RoundToInt(damageAmount), inputMoveVec, isCriticalHit, invertRotate, type, color);
    }

    public enum PopupType
    {
        DEFAULT,
        PLAYER,
        ENEMY
    }


	public void Setup(int damageAmount, Vector3 inputMoveVec = default, bool isCriticalHit = false, bool invertRotate = false, float overrideFontSize = -1, Color overrideColor = default)
    {
        textMesh.SetText(damageAmount.ToString());

        if(isCriticalHit)
        { // Critical Hit

            // crit font size
            textMesh.fontSize = criticalFontSize;

			// crit color
			if (overrideColor == default)
			{
				if (overrideCriticalColor) textColor = newCriticalColor;
				else textColor = GetColorFromString(critColorHex);
			}
			else
			{
				textColor = overrideColor;
			}
		}
        else
        { // Regular Hit

            // reg font size
            textMesh.fontSize = regularFontSize;

            // reg color
            if(overrideColor ==default)
            {
                if (overrideRegularColor) textColor = newRegularColor;
                else textColor = GetColorFromString(regularColorHex);
            }
            else
            {
                textColor = overrideColor;
            }
        }

        if(overrideFontSize != -1) textMesh.fontSize = overrideFontSize;

        textMesh.color = textColor;

        growTimer = growTime;
        sustainTimer = sustainTime;

        if (inputMoveVec == default) moveVector = initialMoveVector;
        else moveVector = inputMoveVec;

        if(invertRotate)
        {
            growRotateAmount = -growRotateAmount;
            disappearRotateAmount = -disappearRotateAmount;
        }

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    private void Disappearing()
    {
        textColor.a -= disappearSpeed * Time.deltaTime;
        if(transform.localScale.x > 0)
        {
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
            if(transform.localScale.x < 0)
            {
                transform.localScale = Vector3.zero;
            }
        }

        transform.Rotate(new Vector3(0, 0, 1), disappearRotateAmount * Time.deltaTime);
        textMesh.color = textColor;
        if (textColor.a < 0)
        { // Destroy once text turns completely transparent
            Destroy(gameObject);
        }
        transform.position += disappearMoveVector * Time.deltaTime;
    }

	public static Color GetColorFromString(string hex)
	{
		if (string.IsNullOrEmpty(hex))
		{
			Debug.LogError("Hex string is null or empty");
			return Color.white;
		}

		// Remove any leading characters (#, 0x)
		if (hex.StartsWith("#"))
			hex = hex.Substring(1);
		else if (hex.StartsWith("0x"))
			hex = hex.Substring(2);

		if (hex.Length != 6 && hex.Length != 8)
		{
			Debug.LogError("Invalid hex color length. Must be 6 or 8 characters.");
			return Color.white;
		}

		if (ColorUtility.TryParseHtmlString("#" + hex, out Color color))
		{
			return color;
		}

		Debug.LogError("Invalid hex color format.");
		return Color.white;
	}
}
