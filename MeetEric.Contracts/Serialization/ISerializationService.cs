namespace MeetEric.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISerializationService
    {
        string SerializeObject<T>(T payload);

        T Deserialize<T>(string payload);
    }
}
