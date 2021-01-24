public interface ITryUseItem<T>
{
    bool TryItem(T usedItem);
}

public interface IInteractable
{
    void Interact();
}

public interface ITogglePuzzle
{
   void DisablePuzzle();
   void EnablePuzzle();
    bool IsPuzzleSolved();
}
