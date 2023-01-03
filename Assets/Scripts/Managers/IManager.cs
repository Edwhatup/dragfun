public interface IManager
{
    void Refresh();
    void GameStart();
    void BroadcastCardEvent(AbstractCardEvent cardEvent);
}