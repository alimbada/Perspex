// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.Markup.Xaml.Data;
using OmniXaml;
using OmniXaml.Typing;
using System;
using OmniXaml.ObjectAssembler;

namespace Perspex.Markup.Xaml.Context
{
    public class PerspexXamlMember : Member
    {
        public PerspexXamlMember(string name,
            XamlType owner,
            ITypeRepository xamlTypeRepository,
            ITypeFeatureProvider featureProvider)
            : base(name, owner, xamlTypeRepository, featureProvider)
        {
        }

        public override void SetValue(object instance, object value, ValuePipeline pipeline)
        {
            PropertyAccessor.SetValue(instance, this, pipeline, value);
        }

        public override string ToString()
        {
            return "Perspex XAML Member " + base.ToString();
        }
    }
}