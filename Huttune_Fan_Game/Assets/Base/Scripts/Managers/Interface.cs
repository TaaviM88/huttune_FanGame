public interface ITryUseItem<T>
{
    bool TryItem(T usedItem);
}

public interface IInteractable
{
    void Interact();
}
