using Newtonsoft.Json;
using System;

namespace Library.Core.Results
{
    /// <summary>
    /// Defines MRZData response from MRTD recognizer
    /// </summary>
    [Serializable]
    public class MrzDataResult
    {
        [JsonProperty("result")]
        public Result Result { get; set; }
    }

    /// <summary>
    /// Defines MRZData response in result from MRTDD recognizer
    /// </summary>
    [Serializable]
    public class Result
    {
        [JsonProperty("mrzData")]
        public MrzData MrzData { get; set; }
    }

    /// <summary>
    /// Defines rawMrzString response in MRZData response
    /// </summary>
    [Serializable]
    public class MrzData
    {
        [JsonProperty("rawMrzString")]
        public string RawMrzString { get; set; }
    }
}
