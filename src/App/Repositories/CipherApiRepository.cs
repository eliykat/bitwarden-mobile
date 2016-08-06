﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bit.App.Abstractions;
using Bit.App.Models.Api;
using Newtonsoft.Json;
using Plugin.Connectivity.Abstractions;
using System.Net;

namespace Bit.App.Repositories
{
    public class CipherApiRepository : BaseApiRepository, ICipherApiRepository
    {
        public CipherApiRepository(IConnectivity connectivity)
            : base(connectivity)
        { }

        protected override string ApiRoute => "ciphers";

        public virtual async Task<ApiResult<CipherResponse>> GetByIdAsync(string id)
        {
            if(!Connectivity.IsConnected)
            {
                return HandledNotConnected<CipherResponse>();
            }

            using(var client = new ApiHttpClient())
            {
                var requestMessage = new TokenHttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(client.BaseAddress, string.Concat(ApiRoute, "/", id)),
                };

                try
                {
                    var response = await client.SendAsync(requestMessage);
                    if(!response.IsSuccessStatusCode)
                    {
                        return await HandleErrorAsync<CipherResponse>(response);
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<CipherResponse>(responseContent);
                    return ApiResult<CipherResponse>.Success(responseObj, response.StatusCode);
                }
                catch(WebException)
                {
                    return HandledWebException<CipherResponse>();
                }
            }
        }

        public virtual async Task<ApiResult<ListResponse<CipherResponse>>> GetAsync()
        {
            if(!Connectivity.IsConnected)
            {
                return HandledNotConnected<ListResponse<CipherResponse>>();
            }

            using(var client = new ApiHttpClient())
            {
                var requestMessage = new TokenHttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(client.BaseAddress, ApiRoute),
                };

                try
                {
                    var response = await client.SendAsync(requestMessage);
                    if(!response.IsSuccessStatusCode)
                    {
                        return await HandleErrorAsync<ListResponse<CipherResponse>>(response);
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<ListResponse<CipherResponse>>(responseContent);
                    return ApiResult<ListResponse<CipherResponse>>.Success(responseObj, response.StatusCode);
                }
                catch(WebException)
                {
                    return HandledWebException<ListResponse<CipherResponse>>();
                }
            }
        }

        public virtual async Task<ApiResult<CipherHistoryResponse>> GetByRevisionDateWithHistoryAsync(DateTime since)
        {
            if(!Connectivity.IsConnected)
            {
                return HandledNotConnected<CipherHistoryResponse>();
            }

            using(var client = new ApiHttpClient())
            {
                var requestMessage = new TokenHttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(client.BaseAddress, string.Concat(ApiRoute, "/history", "?since=", since)),
                };

                try
                {
                    var response = await client.SendAsync(requestMessage);
                    if(!response.IsSuccessStatusCode)
                    {
                        return await HandleErrorAsync<CipherHistoryResponse>(response);
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<CipherHistoryResponse>(responseContent);
                    return ApiResult<CipherHistoryResponse>.Success(responseObj, response.StatusCode);
                }
                catch(WebException)
                {
                    return HandledWebException<CipherHistoryResponse>();
                }
            }
        }
    }
}
