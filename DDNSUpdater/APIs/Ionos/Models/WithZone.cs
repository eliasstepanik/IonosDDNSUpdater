using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace DDNSUpdater.APIs.Ionos.ApiClient.Models {
    public class WithZone : ApiException, IAdditionalDataHolder, IParsable {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The errorRecord property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Record? ErrorRecord { get; set; }
#nullable restore
#else
        public Record ErrorRecord { get; set; }
#endif
        /// <summary>The inputRecord property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public Record? InputRecord { get; set; }
#nullable restore
#else
        public Record InputRecord { get; set; }
#endif
        /// <summary>The invalid property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? Invalid { get; set; }
#nullable restore
#else
        public List<string> Invalid { get; set; }
#endif
        /// <summary>The invalidFields property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? InvalidFields { get; set; }
#nullable restore
#else
        public List<string> InvalidFields { get; set; }
#endif
        /// <summary>The requiredFields property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<string>? RequiredFields { get; set; }
#nullable restore
#else
        public List<string> RequiredFields { get; set; }
#endif
        /// <summary>
        /// Instantiates a new WithZone and sets the default values.
        /// </summary>
        public WithZone() {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static WithZone CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new WithZone();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"errorRecord", n => { ErrorRecord = n.GetObjectValue<Record>(Record.CreateFromDiscriminatorValue); } },
                {"inputRecord", n => { InputRecord = n.GetObjectValue<Record>(Record.CreateFromDiscriminatorValue); } },
                {"invalid", n => { Invalid = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                {"invalidFields", n => { InvalidFields = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
                {"requiredFields", n => { RequiredFields = n.GetCollectionOfPrimitiveValues<string>()?.ToList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteObjectValue<Record>("errorRecord", ErrorRecord);
            writer.WriteObjectValue<Record>("inputRecord", InputRecord);
            writer.WriteCollectionOfPrimitiveValues<string>("invalid", Invalid);
            writer.WriteCollectionOfPrimitiveValues<string>("invalidFields", InvalidFields);
            writer.WriteCollectionOfPrimitiveValues<string>("requiredFields", RequiredFields);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
