﻿using Microsoft.Xrm.Sdk.Metadata;

namespace Colso.Xrm.AttributeEditor.AppCode.AttributeTypes
{
    internal class MemoAttribute : AttributeMetadataBase<MemoAttributeMetadata>
    {
        public int? MaxLength { get; set; }

        protected override void AddAdditionalMetadata(MemoAttributeMetadata attribute)
        {
            attribute.Format = StringFormat.TextArea;
            attribute.ImeMode = ImeMode.Disabled;
            attribute.MaxLength = MaxLength;
        }

        protected override void LoadAdditionalAttributeMetadata(MemoAttributeMetadata attribute)
        {
            MaxLength = attribute.MaxLength;
        }
    }
}