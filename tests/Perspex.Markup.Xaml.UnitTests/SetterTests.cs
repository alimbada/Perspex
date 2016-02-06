// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System.Linq;
using System.Reactive.Linq;
using Moq;
using Perspex.Controls;
using Perspex.Markup.Xaml.Data;
using Perspex.Platform;
using Perspex.Styling;
using Xunit;

namespace Perspex.Markup.Xaml.UnitTests
{
    public class SetterTests
    {
        [Fact]
        public void Element_Can_Be_Bound_To_Non_Control()
        {
            using (PerspexLocator.EnterScope())
            {
                PerspexLocator.CurrentMutable
                    .Bind<IPclPlatformWrapper>()
                    .ToConstant(Mock.Of<IPclPlatformWrapper>());

                var xaml = @"
<UserControl xmlns='https://github.com/perspex'
             xmlns:test='clr-namespace:Perspex.Markup.Xaml.UnitTests;assembly=Perspex.Markup.Xaml.UnitTests'>
    <Button Name='btn'/>
    <Panel Name='target'>
        <Panel.Tag>
            <test:NonControl Foo='{Binding #btn}'/>
        </Panel.Tag>
    </Panel>
</UserControl>";
                var loader = new PerspexXamlLoader();
                var userControl = (UserControl)loader.Load(xaml);
            }                
        }
    }
}
