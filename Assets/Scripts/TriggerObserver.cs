using UnityEngine;

[RequireComponent (typeof(Cell))]
[RequireComponent(typeof(Collider2D))]
public class TriggerObserver : MonoBehaviour
{
    private Collider2D _collider;
    private Cell _cell;
    private EventBus _eventBus;

    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
        _cell = GetComponent<Cell>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _eventBus.Invoke<OnCellPointedEvent>(new OnCellPointedEvent(_cell));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _eventBus.Invoke<OnCellRemovePointEvent>(new OnCellRemovePointEvent(_cell));
    }
}
