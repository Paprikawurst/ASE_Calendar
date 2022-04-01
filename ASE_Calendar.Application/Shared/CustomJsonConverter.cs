using Newtonsoft.Json;

namespace ASE_Calendar.Application.Shared
{
    /// <summary>
    ///     A service that serializes and deserializes given objects.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class CustomJsonConverter<TObject>
    {
        public string SerializeObject(TObject data)
        {
            var jsonObject = JsonConvert.SerializeObject(data);
            return jsonObject;
        }

        public TObject DeserializeObject(string subString)
        {
            var returnObject = JsonConvert.DeserializeObject<TObject>(subString);
            return returnObject;
        }
    }
}