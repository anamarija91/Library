﻿using Library.Core.Clients;
using Library.Core.Exceptions;
using Library.Core.Parsers;
using Library.Core.Requests;
using Library.Core.Results;
using Library.Core.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    /// <summary>
    /// Defines Microblink Client Service
    /// </summary>
    public class MicroblinkClientService
        : IMicroblinkClientService
    {
        private readonly MicroblinkClient microblinkClient;
        private readonly IParserFactory parserFactory;

        /// <summary>
        /// Initializes new instance of <see cref="MicroblinkClientService"/>
        /// </summary>
        /// <param name="microblinkClient">Microblink client.</param>
        public MicroblinkClientService(MicroblinkClient microblinkClient, IParserFactory parserFactory)
        {
            this.microblinkClient = microblinkClient ?? throw new ArgumentNullException(nameof(microblinkClient));
            this.parserFactory = parserFactory ?? throw new ArgumentNullException(nameof(parserFactory));
        }

        /// <inherutdoc />
        public async Task<string> GetInfo()
        {
            return await microblinkClient.GetInfo();
        }

        /// <inheritdoc />
        public async Task<MrzParserResult> CallMRTDRecognizer(ImageRequest request)
        {
            var mrtdRequest = await CreateMRTDRequest(request?.Image);

            var response = await microblinkClient.MRTDRecognizer(mrtdRequest);

            var mrzDataResult = DeserializeMRTDResponse(response);

            if (mrzDataResult.Result is null || mrzDataResult.Result.MrzData is null || mrzDataResult.Result.MrzData.RawMrzString is null)
            {
                throw new MicroblinkClientException("Client response resulted with empty rawMrzString data.");
            }

            return ReadRawMrzString(mrzDataResult.Result.MrzData.RawMrzString);
        }

        /// <summary>
        /// Calls parser and reads rawMrzString
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        private MrzParserResult ReadRawMrzString(string rawString)
        {
            var parser = parserFactory.CreateDataParser(rawString);

            return parser.ReadAndValidateBackSideData();
        }

        /// <summary>
        /// Creates new MRTD request with image source as Base64 string
        /// </summary>
        /// <param name="file">Form file</param>
        /// <returns></returns>
        private async Task<MRTDRequest> CreateMRTDRequest(IFormFile file)
        {
            var imageSource = await Helpers.GetBase64StringForImage(file);

            return new MRTDRequest
            {
                ImageSource = imageSource
            };
        }

        /// <summary>
        /// Deserializes MRTD respone saving only result.mrzData.rawMrzString field
        /// </summary>
        /// <param name="response">String Response from MRTD recognizer</param>
        /// <returns></returns>
        private MrzDataResponse DeserializeMRTDResponse(string response)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var result = JsonConvert.DeserializeObject<MrzDataResponse>(response, jsonSerializerSettings);

            return result;
        }
    }
}