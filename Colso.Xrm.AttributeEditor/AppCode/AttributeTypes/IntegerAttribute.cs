using Microsoft.Xrm.Sdk.Metadata;

namespace Colso.Xrm.AttributeEditor.AppCode.AttributeTypes
{
    internal class IntegerAttribute : AttributeMetadataBase<IntegerAttributeMetadata>
    {
        protected override void AddAdditionalMetadata(IntegerAttributeMetadata attribute)
        {
            attribute.Format = IntegerFormat.None;
            attribute.MaxValue = 9999;
            attribute.MinValue = 0;
        }
    }
}