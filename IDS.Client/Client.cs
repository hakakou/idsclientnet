using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace IDSApi
{
    public partial class Client
    {
        public HttpClient HttpClient => _httpClient;

        public static Client CreateClient(string url, string username = "admin", string password = "password")
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) => true;

            var httpClient = new HttpClient(handler);
            httpClient.BaseAddress = new Uri(url);

            httpClient.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(username, password);

            var client = new Client(httpClient);
            client.BaseUrl = url;
            return client;
        }

        public virtual async Task<GraphContext> ApiIdsDescriptionAsyncEx(Uri recipient, Uri? elementId)
        {
            var fs = await ApiIdsDescriptionAsync(recipient, elementId, System.Threading.CancellationToken.None);

            string result;
            using (var textWriter = new StringWriter())
            using (var reader = new StreamReader(fs.Stream))
            {
                var readChunk = new char[1024];
                int readChunkLength;
                //do while: is useful for the last iteration in case readChunkLength < chunkLength
                do
                {
                    readChunkLength = await reader.ReadBlockAsync(readChunk, 0, 1024);
                    textWriter.Write(readChunk, 0, readChunkLength);
                } while (readChunkLength > 0);

                result = textWriter.ToString();
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<GraphContext>(result);
        }

        public virtual async System.Threading.Tasks.Task<AgreementView> ApiIdsContractAsyncEx(System.Uri recipient,
            System.Collections.Generic.IEnumerable<System.Uri> resourceIds, System.Collections.Generic.IEnumerable<System.Uri> artifactIds,
            bool download, System.Collections.Generic.IEnumerable<StartNegotiation> body)
        {
            var cancellationToken = CancellationToken.None;

            if (recipient == null)
                throw new System.ArgumentNullException("recipient");

            if (body == null)
                throw new System.ArgumentNullException("body");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/ids/contract?");
            urlBuilder_.Append(System.Uri.EscapeDataString("recipient") + "=").Append(System.Uri.EscapeDataString(ConvertToString(recipient, System.Globalization.CultureInfo.InvariantCulture))).Append("&");

            if (resourceIds == null)
                urlBuilder_.Append("resourceIds=&");
            else
                foreach (var item_ in resourceIds) { urlBuilder_.Append(System.Uri.EscapeDataString("resourceIds") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }

            if (artifactIds == null)
                urlBuilder_.Append("artifactIds=&");
            else
                foreach (var item_ in artifactIds) { urlBuilder_.Append(System.Uri.EscapeDataString("artifactIds") + "=").Append(System.Uri.EscapeDataString(ConvertToString(item_, System.Globalization.CultureInfo.InvariantCulture))).Append("&"); }

            urlBuilder_.Append(System.Uri.EscapeDataString("download") + "=").Append(System.Uri.EscapeDataString(ConvertToString(download, System.Globalization.CultureInfo.InvariantCulture))).Append("&");

            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(body, _settings.Value);
                    var content_ = new System.Net.Http.StringContent(json_);
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("*/*"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 401)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<AgreementView>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<AgreementView>("Unauthorized", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                        else
                        if (status_ == 201)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<AgreementView>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }

                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }
    }

}