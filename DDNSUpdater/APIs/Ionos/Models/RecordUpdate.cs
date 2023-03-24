using Microsoft.Kiota.Abstractions.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace DDNSUpdater.APIs.Ionos.ApiClient.Models {
    public class RecordUpdate : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The content property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Content { get; set; }
#nullable restore
#else
        public string Content { get; set; }
#endif
        /// <summary>When is true, the record is not visible for lookup.</summary>
        public bool? Disabled { get; set; }
        /// <summary>The prio property</summary>
        public int? Prio { get; set; }
        /// <summary>Time to live for the record, recommended 3600.</summary>
        public int? Ttl { get; set; }
        /// <summary>
        /// Instantiates a new RecordUpdate and sets the default values.
        /// </summary>
        public RecordUpdate() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static RecordUpdate CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new RecordUpdate();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"content", n => { Content = n.GetStringValue(); } },
                {"disabled", n => { Disabled = n.GetBoolValue(); } },
                {"prio", n => { Prio = n.GetIntValue(); } },
                {"ttl", n => { Ttl = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("content", Content);
            writer.WriteBoolValue("disabled", Disabled);
            writer.WriteIntValue("prio", Prio);
            writer.WriteIntValue("ttl", Ttl);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
