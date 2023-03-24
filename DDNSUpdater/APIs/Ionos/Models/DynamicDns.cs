using Microsoft.Kiota.Abstractions.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace DDNSUpdater.APIs.Ionos.ApiClient.Models {
    public class DynamicDns : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>DynDns configuration identifier.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? BulkId { get; set; }
#nullable restore
#else
        public string BulkId { get; set; }
#endif
        /// <summary>Dynamic Dns description.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Description { get; set; }
#nullable restore
#else
        public string Description { get; set; }
#endif
        /// <summary>The domains property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Domains { get; set; }
#nullable restore
#else
        public List<string> Domains { get; set; }
#endif
        /// <summary>Use the url with GET to update the ips of (sub)domains. Query parameters: ipv4, ipv6.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? UpdateUrl { get; set; }
#nullable restore
#else
        public string UpdateUrl { get; set; }
#endif
        /// <summary>
        /// Instantiates a new DynamicDns and sets the default values.
        /// </summary>
        public DynamicDns() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static DynamicDns CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new DynamicDns();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"bulkId", n => { BulkId = n.GetStringValue(); } },
                {"description", n => { Description = n.GetStringValue(); } },
                {"domains", n => { Domains = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                {"updateUrl", n => { UpdateUrl = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("bulkId", BulkId);
            writer.WriteStringValue("description", Description);
            writer.WriteCollectionOfPrimitiveValues<string>("domains", Domains);
            writer.WriteStringValue("updateUrl", UpdateUrl);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
