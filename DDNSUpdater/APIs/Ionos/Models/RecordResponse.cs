using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace DDNSUpdater.APIs.Ionos.ApiClient.Models {
    public class RecordResponse : ApiException, IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The date of the last change formatted as yyyy-MM-dd&apos;T&apos;HH:mm:ss.SSS&apos;Z&apos;</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? ChangeDate { get; set; }
#nullable restore
#else
        public string ChangeDate { get; set; }
#endif
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
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The name property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The prio property</summary>
        public int? Prio { get; set; }
        /// <summary>Root zone name.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? RootName { get; set; }
#nullable restore
#else
        public string RootName { get; set; }
#endif
        /// <summary>Time to live for the record, recommended 3600.</summary>
        public int? Ttl { get; set; }
        /// <summary>Holds supported dns record types.</summary>
        public RecordTypes? Type { get; set; }
        /// <summary>
        /// Instantiates a new recordResponse and sets the default values.
        /// </summary>
        public RecordResponse() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static RecordResponse CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new RecordResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"changeDate", n => { ChangeDate = n.GetStringValue(); } },
                {"content", n => { Content = n.GetStringValue(); } },
                {"disabled", n => { Disabled = n.GetBoolValue(); } },
                {"id", n => { Id = n.GetStringValue(); } },
                {"name", n => { Name = n.GetStringValue(); } },
                {"prio", n => { Prio = n.GetIntValue(); } },
                {"rootName", n => { RootName = n.GetStringValue(); } },
                {"ttl", n => { Ttl = n.GetIntValue(); } },
                {"type", n => { Type = n.GetEnumValue<RecordTypes>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("changeDate", ChangeDate);
            writer.WriteStringValue("content", Content);
            writer.WriteBoolValue("disabled", Disabled);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("name", Name);
            writer.WriteIntValue("prio", Prio);
            writer.WriteStringValue("rootName", RootName);
            writer.WriteIntValue("ttl", Ttl);
            writer.WriteEnumValue<RecordTypes>("type", Type);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
