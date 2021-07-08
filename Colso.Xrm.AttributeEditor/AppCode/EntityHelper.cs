using System;
using System.Linq;
using System.Reflection;
using Colso.Xrm.AttributeEditor.AppCode.AttributeTypes;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace Colso.Xrm.AttributeEditor.AppCode
{
    public class EntityHelper
    {
        public readonly int languageCode = 2052;
        private readonly IOrganizationService service;
        private string entity;

        public EntityHelper(string entitylogicalname, int languageCode, IOrganizationService service)
        {
            this.entity = entitylogicalname;
            this.languageCode = languageCode;
            this.service = service;
        }

        public static IAttribute GetAttributeFromTypeName(string type)
        {
            var attributeType =
            (from t in Assembly.GetExecutingAssembly().GetTypes()
             where t.GetInterfaces().Contains(typeof(IAttribute))
                   && t.Name.Equals(type + "Attribute", StringComparison.OrdinalIgnoreCase)
             select t).FirstOrDefault();

            if (attributeType == null)
                return null;

            var attribute = (IAttribute)Activator.CreateInstance(attributeType);
            return attribute;
        }

        public void Publish()
        {
            var pubRequest = new PublishXmlRequest();
            pubRequest.ParameterXml = string.Format(@"<importexportxml>
                                                           <entities>
                                                              <entity>{0}</entity>
                                                           </entities>
                                                        </importexportxml>",
                                                    entity);

            service.Execute(pubRequest);
        }
    }
}