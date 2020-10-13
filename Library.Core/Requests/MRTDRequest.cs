using Newtonsoft.Json;
using System;

namespace Library.Core.Requests
{
    /// <summary>
    /// Defines MRTD Request for MRTD recognizer
    /// </summary>
    [Serializable]
    public class MRTDRequest
    {
        [JsonProperty("imageSource")]
        public string ImageSource { get; set; }
    }
}
