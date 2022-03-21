using Newtonsoft.Json;

namespace ASE_Calendar.Application.Shared
{
    public class CustomJsonConverter<TObject>
    {
        public string SerializeObject(TObject data)
        {
            var jsonObject = JsonConvert.SerializeObject(data);
            return jsonObject;
        }

        public TObject DeserializeObject(string subString)
        {
            TObject returnObject = JsonConvert.DeserializeObject<TObject>(subString);
            return returnObject;
        }
    }
}