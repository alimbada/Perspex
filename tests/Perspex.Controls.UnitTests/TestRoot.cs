﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using Moq;
using Perspex.Layout;
using Perspex.Platform;
using Perspex.Rendering;
using Perspex.Styling;

namespace Perspex.Controls.UnitTests
{
    internal class TestRoot : Decorator, ILayoutRoot, IRenderRoot, IStyleRoot
    {
        public Size ClientSize => new Size(100, 100);

        public Size MaxClientSize => Size.Infinity;

        public ILayoutManager LayoutManager => new Mock<ILayoutManager>().Object;

        public IRenderTarget RenderTarget
        {
            get { throw new NotImplementedException(); }
        }

        public IRenderQueueManager RenderQueueManager => null;

        public Point PointToClient(Point p)
        {
            throw new NotImplementedException();
        }

        public Point PointToScreen(Point p)
        {
            return new Point();
        }
    }
}
