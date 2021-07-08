using Microsoft.Xrm.Sdk.Metadata;

namespace Colso.Xrm.AttributeEditor.AppCode.AttributeTypes
{
    internal class DecimalAttribute : AttributeMetadataBase<DecimalAttributeMetadata>
    {
        protected override void AddAdditionalMetadata(DecimalAttributeMetadata attribute)
        {
            attribute.MaxValue = 1000000;
            attribute.MinValue = 0;
            attribute.Precision = 2;
        }
    }
}