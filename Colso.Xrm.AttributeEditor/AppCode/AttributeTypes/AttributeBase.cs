using System;
using System.Linq;
using System.Reflection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace Colso.Xrm.AttributeEditor.AppCode.AttributeTypes
{
    internal abstract class AttributeBase : IAttribute
    {
        public readonly int LanguageCode = 2052;

        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string Entity { get; set; }
        public string LogicalName { get; set; }
        public string Requirement { get; set; }
        public string SchemaName { get; set; }

        public abstract void CreateAttribute(IOrganizationService service);

        public void DeleteAttribute(IOrganizationService service)
        {
            var request = new DeleteAttributeRequest
            {
                EntityLogicalName = Entity,
                LogicalName = LogicalName
            };

            service.Execute(request);
        }

        public void LoadFromAttributeMetadata(AttributeMetadata attribute)
        {
            LogicalName = attribute.LogicalName;
            DisplayName = attribute.DisplayName?.UserLocalizedLabel?.Label;
            Requirement = attribute.RequiredLevel.Value.ToString();
            SchemaName = attribute.SchemaName;
            Description = attribute.Description?.UserLocalizedLabel?.Label;

            LoadAdditionalAttributeMetadata(attribute);
        }

        public void LoadFromAttributeMetadataRow(AttributeMetadataRow row)
        {
            var columns = AttributeMetadataRow.GetColumns();
            var attributeMetadataRowProperties = typeof(AttributeMetadataRow).GetProperties();
            var attributeMetadataProperties = GetType().GetProperties();

            Entity = row.Entity;

            for (var i = 0; i < columns.Length; i++)
            {
                var column = columns[i];

                var attributeMetadataRowProperty = attributeMetadataRowProperties.First(x =>
                    (x.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute)?.Header == column.Header);
                var attributeProperty =
                    attributeMetadataProperties.FirstOrDefault(x => x.Name == attributeMetadataRowProperty.Name);

                if (attributeProperty != null)
                {
                    var value = attributeMetadataRowProperty.GetValue(row);
                    attributeProperty.SetValue(this, value);
                }
            }
        }

        public AttributeMetadataRow ToAttributeMetadataRow()
        {
            var typeName = GetType().Name;
            var result = new AttributeMetadataRow { AttributeType = typeName.Substring(0, typeName.Length - 9) };

            var columns = AttributeMetadataRow.GetColumns();
            var attributeMetadataRowProperties = typeof(AttributeMetadataRow).GetProperties();
            var attributeMetadataProperties = GetType().GetProperties();

            for (var i = 0; i < columns.Length; i++)
            {
                var column = columns[i];

                var attributeMetadataRowProperty = attributeMetadataRowProperties.First(x =>
                    (x.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute)?.Header == column.Header);
                var attributeProperty =
                    attributeMetadataProperties.FirstOrDefault(x => x.Name == attributeMetadataRowProperty.Name);

                if (attributeProperty != null)
                {
                    var value = attributeProperty.GetValue(this);
                    attributeMetadataRowProperty.SetValue(result, value);
                }
            }

            return result;
        }

        public abstract void UpdateAttribute(IOrganizationService service);

        protected virtual void LoadAdditionalAttributeMetadata(AttributeMetadata attribute)
        {
        }

        protected AttributeRequiredLevelManagedProperty ParseRequiredLevel(string level)
        {
            var requiredLevel = AttributeRequiredLevel.None;
            Enum.TryParse(level, out requiredLevel);
            return new AttributeRequiredLevelManagedProperty(requiredLevel);
        }
    }
}