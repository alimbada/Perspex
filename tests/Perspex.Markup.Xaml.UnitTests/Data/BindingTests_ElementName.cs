﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.Controls;
using Perspex.Markup.Xaml.Data;
using Xunit;

namespace Perspex.Markup.Xaml.UnitTests.Data
{
    public class BindingTests_ElementName
    {
        [Fact]
        public void Should_Bind_To_Element_Path()
        {
            TextBlock target;
            var root = new TestRoot
            {
                Child = new StackPanel
                {
                    Children = new Controls.Controls
                    {
                        new TextBlock
                        {
                            Name = "source",
                            Text = "foo",
                        },
                        (target = new TextBlock
                        {
                            Name = "target",
                        })
                    }
                }
            };

            var binding = new Binding
            {
                ElementName = "source",
                Path = "Text",
            };

            target.Bind(TextBox.TextProperty, binding);

            Assert.Equal("foo", target.Text);
        }

        [Fact]
        public void Should_Bind_To_Element()
        {
            TextBlock source;
            ContentControl target;

            var root = new TestRoot
            {
                Child = new StackPanel
                {
                    Children = new Controls.Controls
                    {
                        (source = new TextBlock
                        {
                            Name = "source",
                            Text = "foo",
                        }),
                        (target = new ContentControl
                        {
                            Name = "target",
                        })
                    }
                }
            };

            var binding = new Binding
            {
                ElementName = "source",
            };

            target.Bind(ContentControl.ContentProperty, binding);

            Assert.Same(source, target.Content);
        }

        [Fact]
        public void Should_Bind_To_Later_Added_Element_Path()
        {
            TextBlock target;
            StackPanel stackPanel;

            var root = new TestRoot
            {
                Child = stackPanel = new StackPanel
                {
                    Children = new Controls.Controls
                    {
                        (target = new TextBlock
                        {
                            Name = "target",
                        }),
                    }
                }
            };

            var binding = new Binding
            {
                ElementName = "source",
                Path = "Text",
            };

            target.Bind(TextBox.TextProperty, binding);

            stackPanel.Children.Add(new TextBlock
            {
                Name = "source",
                Text = "foo",
            });

            Assert.Equal("foo", target.Text);
        }

        [Fact]
        public void Should_Bind_To_Later_Added_Element()
        {
            ContentControl target;
            StackPanel stackPanel;

            var root = new TestRoot
            {
                Child = stackPanel = new StackPanel
                {
                    Children = new Controls.Controls
                    {
                        (target = new ContentControl
                        {
                            Name = "target",
                        }),
                    }
                }
            };

            var binding = new Binding
            {
                ElementName = "source",
            };

            target.Bind(ContentControl.ContentProperty, binding);

            var source = new TextBlock
            {
                Name = "source",
                Text = "foo",
            };

            stackPanel.Children.Add(source);

            Assert.Same(source, target.Content);
        }
    }
}
