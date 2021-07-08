using Microsoft.Xrm.Sdk.Metadata;

namespace Colso.Xrm.AttributeEditor.AppCode.AttributeTypes
{
    internal class DoubleAttribute : AttributeMetadataBase<DoubleAttributeMetadata>
    {
        protected override void AddAdditionalMetadata(DoubleAttributeMetadata attribute)
        {
            attribute.MaxValue = 1000000;
            attribute.MinValue = 0;
            attribute.Precision = 2;
        }
    }
}