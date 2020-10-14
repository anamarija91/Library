using Newtonsoft.Json;
using System;

namespace Library.Core.Results
{
    /// <summary>
    /// Defines MRZData response from MRTD recognizer
    /// </summary>
    [Serializable]
    public class MrzDataResponse
    {
        [JsonProperty("result")]
        public Result Result { get; set; }
    }

    /// <summary>
    /// Defines  result from MRZDataResponse recognizer
    /// </summary>
    [Serializable]
    public class Result
    {
        [JsonProperty("mrzData")]
        public MrzData MrzData { get; set; }
    }

    /// <summary>
    /// Defines rawMrzString response in MRZData response result
    /// </summary>
    [Serializable]
    public class MrzData
    {
        [JsonProperty("rawMrzString")]
        public string RawMrzString { get; set; }
    }
}
