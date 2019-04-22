interface IMessageHandler
{
    bool ProcessMessage(Message m);
}

class MessageHandler<T>: IMessageHandler where T:Message
{
    Func<T, bool> handlerDelegate;

    public MessageHandler(Func<T, bool> handlerDelegate)
    {
        this.handlerDelegate = handlerDelegate; 
    }

    public bool ProcessMessage(Message m)
    {
        handlerDelegate((T)m);  
    }
}
private Dictionary<Type, IMessageHandler> listeners;

public void AddListener<T>(Func<T, bool> listener) where T : Message 
{ 
    this.listeners[typeof(T)].Add(new MessageHandler<T>(listener));
}

public void SendMessage(Message message) 
{
    foreach (IMessageHandler listener in this.listeners[message.GetType()]) 
    {
       listener.ProcessMessage(message);
    }
}
abstract class BaseMessagePump
{
    protected abstract void SendMessage(Message m);

    public void AddListener<T>(Func<T, bool> l) where T : Message
    {
        //check a dictionary / cache
        new GenericMessageQueue<T>().listeners.Add(l);
    }
}

class GenericMessageQueue<T> : BaseMessagePump where T : Message
{
    public List<Func<T, bool>> listeners;

    protected override void SendMessage(Message m) 
    {
        listeners[0]((T)m);//the cast here is safe if you do correct cache / type checking in the base add listener
    }
}