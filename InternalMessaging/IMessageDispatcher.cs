using System.Collections;

namespace InternalMessaging;

public interface IMessageDispatcher
{
    void Dispatch(IEnumerable message);
}
