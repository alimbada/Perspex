﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using ReactiveUI;

namespace XamlTestApplication.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Items = new List<TestItem>();

            for (int i = 0; i < 10; ++i)
            {
                Items.Add(new TestItem($"Item {i}", $"Item {i} Value"));
            }

            Nodes = new List<TestNode>
            {
                new TestNode
                {
                    Header = "Root",
                    SubHeader = "Root Item",
                    IsExpanded = true,
                    Children = new[]
                    {
                        new TestNode
                        {
                            Header = "Child 1",
                            SubHeader = "Child 1 Value",
                        },
                        new TestNode
                        {
                            Header = "Child 2",
                            SubHeader = "Child 2 Value",
                            IsExpanded = false,
                            Children = new[]
                            {
                                new TestNode
                                {
                                    Header = "Grandchild",
                                    SubHeader = "Grandchild Value",
                                },
                                new TestNode
                                {
                                    Header = "Grandmaster Flash",
                                    SubHeader = "White Lines",
                                },
                            }
                        },
                    }
                }
            };

            CollapseNodesCommand = ReactiveCommand.Create();
            CollapseNodesCommand.Subscribe(_ => ExpandNodes(false));
            ExpandNodesCommand = ReactiveCommand.Create();
            ExpandNodesCommand.Subscribe(_ => ExpandNodes(true));
        }

        public List<TestItem> Items { get; }
        public List<TestNode> Nodes { get; }

        public ReactiveCommand<object> CollapseNodesCommand { get; }

        public ReactiveCommand<object> ExpandNodesCommand { get; }

        public void ExpandNodes(bool expanded)
        {
            foreach (var node in Nodes)
            {
                ExpandNodes(node, expanded);
            }
        }

        private void ExpandNodes(TestNode node, bool expanded)
        {
            node.IsExpanded = expanded;

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    ExpandNodes(child, expanded);
                }
            }
        }
    }
}
