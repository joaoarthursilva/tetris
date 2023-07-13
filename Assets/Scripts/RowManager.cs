using TMPro;
using UnityEngine;

public class RowManager : MonoBehaviour
{
    private int _totalDeletedRows;

    [SerializeField] private TextMeshProUGUI totalDeletedRowsText;

    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private float timeToClearNotificationText = .6f;

    private void Start()
    {
        _totalDeletedRows = 0;
        UpdateDeletedRowsText();
    }

    public void AddDeletedRows(int amount, int lastDeletedRowY)
    {
        _totalDeletedRows += amount;
        UpdateDeletedRowsText();
        UpdateNotificationText(amount, lastDeletedRowY);
    }

    private void UpdateDeletedRowsText()
    {
        totalDeletedRowsText.text = $"{_totalDeletedRows}";
    }

    private void UpdateNotificationText(int amount, int lastDeletedRowY)
    {
        // implementar notificação na altura da ultima linha deletada

        notificationText.text = amount switch
        {
            1 => "Line",
            2 => "Duo",
            3 => "Trio",
            4 => "Tetris",
            _ => notificationText.text
        };
        Invoke(nameof(ClearNotificationText), timeToClearNotificationText);
    }

    private void ClearNotificationText()
    {
        notificationText.text = "";
    }
}