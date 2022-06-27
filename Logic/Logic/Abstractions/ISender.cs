namespace Logic.Abstractions
{
    public interface ISender
    {
        void Send(string to, string message);
    }
}
