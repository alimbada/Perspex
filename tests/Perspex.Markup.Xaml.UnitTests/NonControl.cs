// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.Controls;

namespace Perspex.Markup.Xaml.UnitTests
{
    public class NonControl : PerspexObject
    {
        public static readonly StyledProperty<IControl> FooProperty =
            PerspexProperty.Register<NonControl, IControl>("Foo");

        public IControl Foo
        {
            get { return GetValue(FooProperty); }
            set { SetValue(FooProperty, value); }
        }
    }
}
