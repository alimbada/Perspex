﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using OmniXaml.ObjectAssembler;
using OmniXaml.Typing;
using Perspex.Controls;
using Perspex.Data;
using Perspex.Markup.Xaml.Data;
using Perspex.Styling;

namespace Perspex.Markup.Xaml.Context
{
    internal static class PropertyAccessor
    {
        public static void SetValue(
            object instance, 
            MutableMember member, 
            ValuePipeline pipeline, 
            object value)
        {
            var perspexProperty = FindPerspexProperty(instance, member);

            if (value is IBinding)
            {
                SetBinding(instance, member, pipeline, perspexProperty, (IBinding)value);
            }
            else if (perspexProperty != null)
            {
                ((PerspexObject)instance).SetValue(perspexProperty, value);
            }
            else if (instance is Setter && member.Name == "Value")
            {
                // TODO: Make this more generic somehow.
                var setter = (Setter)instance;
                var targetType = setter.Property.PropertyType;
                var xamlType = member.TypeRepository.GetByType(targetType);
                SetClrProperty(instance, member, pipeline.ConvertValueIfNecessary(value, xamlType));
            }
            else
            {
                SetClrProperty(instance, member, value);
            }
        }

        private static PerspexProperty FindPerspexProperty(object instance, MutableMember member)
        {
            var target = instance as IPerspexObject;
            var attached = member as PerspexAttachableXamlMember;

            if (target == null)
            {
                return null;
            }

            PerspexProperty property;
            string propertyName;

            if (attached == null)
            {
                propertyName = member.Name;
                property = PerspexPropertyRegistry.Instance.GetRegistered((PerspexObject)target)
                    .FirstOrDefault(x => x.Name == propertyName);
            }
            else
            {
                // Ensure the OwnerType's static ctor has been run.
                RuntimeHelpers.RunClassConstructor(attached.DeclaringType.UnderlyingType.TypeHandle);

                propertyName = attached.DeclaringType.UnderlyingType.Name + '.' + member.Name;

                property = PerspexPropertyRegistry.Instance.GetRegistered((PerspexObject)target)
                    .Where(x => x.IsAttached && x.OwnerType == attached.DeclaringType.UnderlyingType)
                    .FirstOrDefault(x => x.Name == member.Name);
            }

            return property;
        }

        private static void SetBinding(
            object instance,
            MutableMember member, 
            ValuePipeline pipeline,
            PerspexProperty property,
            IBinding binding)
        {
            var control = instance as IControl;
            var xamlBinding = binding as Binding;

            if (control == null && xamlBinding != null)
            {
                xamlBinding.TreeContext = pipeline.TopDownValueContext.GetLastInstance<IControl>();
            }

            if (!(AssignBinding(instance, member, binding) || ApplyBinding(instance, property, binding)))
            {
                throw new InvalidOperationException(
                    $"Cannot assign to '{member.Name}' on '{instance.GetType()}");
            }
        }

        private static void SetClrProperty(object instance, MutableMember member, object value)
        {
            if (member.IsAttachable)
            {
                member.Setter.Invoke(null, new[] { instance, value });
            }
            else
            {
                member.Setter.Invoke(instance, new[] { value });
            }
        }

        private static bool AssignBinding(object instance, MutableMember member, IBinding binding)
        {
            var property = instance.GetType()
                .GetRuntimeProperties()
                .FirstOrDefault(x => x.Name == member.Name);

            if (property?.GetCustomAttribute<AssignBindingAttribute>() != null)
            {
                property.SetValue(instance, binding);
                return true;
            }

            return false;
        }

        private static bool ApplyBinding(object instance, PerspexProperty property, IBinding binding)
        {
            if (property != null)
            {
                ((IPerspexObject)instance).Bind(property, binding);
                return true;
            }

            return false;
        }
    }
}
