using DDNSUpdater.APIs.Ionos.ApiClient.V1;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Serialization.Form;
using Microsoft.Kiota.Serialization.Json;
using Microsoft.Kiota.Serialization.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace DDNSUpdater.APIs.Ionos.ApiClient {
    /// <summary>
    /// The main entry point of the SDK, exposes the configuration and the fluent API.
    /// </summary>
    public class IonosAPIClient {
        /// <summary>Path parameters for the request</summary>
        private Dictionary<string, object> PathParameters { get; set; }
        /// <summary>The request adapter to use to execute the requests.</summary>
        private IRequestAdapter RequestAdapter { get; set; }
        /// <summary>Url template to use to build the URL for the current request builder</summary>
        private string UrlTemplate { get; set; }
        /// <summary>The v1 property</summary>
        public V1RequestBuilder V1 { get =>
            new V1RequestBuilder(PathParameters, RequestAdapter);
        }
        /// <summary>
        /// Instantiates a new IonosAPIClient and sets the default values.
        /// </summary>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public IonosAPIClient(IRequestAdapter requestAdapter) {
            _ = requestAdapter ?? throw new ArgumentNullException(nameof(requestAdapter));
            PathParameters = new Dictionary<string, object>();
            UrlTemplate = "{+baseurl}";
            RequestAdapter = requestAdapter;
            ApiClientBuilder.RegisterDefaultSerializer<JsonSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<TextSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultSerializer<FormSerializationWriterFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<JsonParseNodeFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<TextParseNodeFactory>();
            ApiClientBuilder.RegisterDefaultDeserializer<FormParseNodeFactory>();
            if (string.IsNullOrEmpty(RequestAdapter.BaseUrl)) {
                RequestAdapter.BaseUrl = "https://api.hosting.ionos.com/dns";
            }
            PathParameters.TryAdd("baseurl", RequestAdapter.BaseUrl);
        }
    }
}
