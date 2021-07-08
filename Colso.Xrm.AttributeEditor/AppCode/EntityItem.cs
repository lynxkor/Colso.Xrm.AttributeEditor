using System;
using System.Linq;
using Microsoft.Xrm.Sdk.Metadata;

namespace Colso.Xrm.AttributeEditor.AppCode
{
    public class EntityItem : IEquatable<EntityItem>, IComparable<EntityItem>
    {
        private readonly EntityMetadata record;

        public EntityItem(EntityMetadata record)
        {
            this.record = record;
        }

        public string DisplayName
        {
            get
            {
                return record.DisplayName == null ?
                    string.Empty :
                    record.DisplayName.UserLocalizedLabel == null ?
                    record.DisplayName.LocalizedLabels.Select(l => l.Label).FirstOrDefault() :
                    record.DisplayName.UserLocalizedLabel.Label;
            }
        }

        public int LanguageCode
        {
            get
            {
                return record.DisplayName == null || record.DisplayName.UserLocalizedLabel == null ?
                    2052 :
                    record.DisplayName.UserLocalizedLabel.LanguageCode;
            }
        }

        public string LogicalName
        {
            get
            {
                return record.LogicalName;
            }
        }

        public int CompareTo(EntityItem other)
        {
            // A null value means that this object is greater.
            if (other == null)
                return 1;

            if (record == null)
                return -1;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public bool Equals(EntityItem other)
        {
            if (record == null) return false;
            if (other == null) return false;

            return record.LogicalName.Equals(other.LogicalName);
        }
    }
}