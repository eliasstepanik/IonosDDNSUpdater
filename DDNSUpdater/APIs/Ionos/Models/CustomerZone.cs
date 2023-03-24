using Microsoft.Kiota.Abstractions.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace DDNSUpdater.APIs.Ionos.ApiClient.Models {
    public class CustomerZone : IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The zone id.</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The zone name</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Name { get; set; }
#nullable restore
#else
        public string Name { get; set; }
#endif
        /// <summary>The records property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<RecordResponse>? Records { get; set; }
#nullable restore
#else
        public List<RecordResponse> Records { get; set; }
#endif
        /// <summary>Represents the possible zone types.</summary>
        public ZoneTypes? Type { get; set; }
        /// <summary>
        /// Instantiates a new CustomerZone and sets the default values.
        /// </summary>
        public CustomerZone() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static CustomerZone CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new CustomerZone();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"id", n => { Id = n.GetStringValue(); } },
                {"name", n => { Name = n.GetStringValue(); } },
                {"records", n => { Records = n.GetCollectionOfObjectValues<RecordResponse>(RecordResponse.CreateFromDiscriminatorValue)?.ToList(); } },
                {"type", n => { Type = n.GetEnumValue<ZoneTypes>(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("name", Name);
            writer.WriteCollectionOfObjectValues<RecordResponse>("records", Records);
            writer.WriteEnumValue<ZoneTypes>("type", Type);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
