using System;
using System.Collections.Generic;
using RazorViewsForMarketers.Core.FieldRenderers.EnumerationTypes;
using RazorViewsForMarketers.Models.Fields;
using RazorViewsForMarketers.Presenters;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;

namespace RazorViewsForMarketers.Core.FieldRenderers
{
    public class RadioListInitializer : FieldInitializerBase<RadioButtonListField>
    {
        public RadioListInitializer(Item fieldItem) : base(fieldItem) { }

        public override void PopulateParameters(Field field, RadioButtonListField model)
        {
            IEnumerable<Pair<string, string>> p = ParametersUtil.XmlToPairArray(field.Value, true);
            foreach (var pair in p)
            {
                ERadioButtonListParametersType fieldParametersType;
                bool isKnownType = Enum.TryParse(pair.Part1, true, out fieldParametersType);
                if (!isKnownType) continue;

                switch (fieldParametersType)
                {
                    case ERadioButtonListParametersType.Items:
                        bool isQueryValid = !string.IsNullOrEmpty(pair.Part2);
                        if (isQueryValid)
                        {
                            var querySettings = QuerySettings.ParseRange(pair.Part2);
                            var dataSource = QueryManager.Select(querySettings);

                            // TODO: continue processing
                        }
                        break;
                    case ERadioButtonListParametersType.SelectedValue:
                        model.SelectedValue = pair.Part2;
                        break;
                    case ERadioButtonListParametersType.Columns:
                        model.Columns = pair.Part2;
                        break;
                    case ERadioButtonListParametersType.Direction:
                        model.Direction = pair.Part2;
                        break;
                }
            }
        }

        public override void PopulateLocalizedParameters(Field field, RadioButtonListField model)
        {

        }
    }
}