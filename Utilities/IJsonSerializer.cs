using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Utilities
{
    public interface IJsonSerializer : ISerializer, IDeserializer
    {
    }
}
