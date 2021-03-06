﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.Controls.Mixins;
using Perspex.Controls.Presenters;
using Perspex.Controls.Primitives;
using Perspex.Controls.Templates;
using Perspex.Layout;
using Perspex.Metadata;

namespace Perspex.Controls
{
    /// <summary>
    /// Displays <see cref="Content"/> according to a <see cref="FuncDataTemplate"/>.
    /// </summary>
    public class ContentControl : TemplatedControl, IContentControl, IContentPresenterHost
    {
        /// <summary>
        /// Defines the <see cref="Content"/> property.
        /// </summary>
        public static readonly StyledProperty<object> ContentProperty =
            PerspexProperty.Register<ContentControl, object>(nameof(Content));

        /// <summary>
        /// Defines the <see cref="HorizontalContentAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<HorizontalAlignment> HorizontalContentAlignmentProperty =
            PerspexProperty.Register<ContentControl, HorizontalAlignment>(nameof(HorizontalContentAlignment));

        /// <summary>
        /// Defines the <see cref="VerticalContentAlignment"/> property.
        /// </summary>
        public static readonly StyledProperty<VerticalAlignment> VerticalContentAlignmentProperty =
            PerspexProperty.Register<ContentControl, VerticalAlignment>(nameof(VerticalContentAlignment));

        /// <summary>
        /// Initializes static members of the <see cref="ContentControl"/> class.
        /// </summary>
        static ContentControl()
        {
            ContentControlMixin.Attach<ContentControl>(ContentProperty, x => x.LogicalChildren);
        }

        /// <summary>
        /// Gets or sets the content to display.
        /// </summary>
        [Content]
        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// Gets the presenter from the control's template.
        /// </summary>
        public IContentPresenter Presenter
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the horizontal alignment of the content within the control.
        /// </summary>
        public HorizontalAlignment HorizontalContentAlignment
        {
            get { return GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the vertical alignment of the content within the control.
        /// </summary>
        public VerticalAlignment VerticalContentAlignment
        {
            get { return GetValue(VerticalContentAlignmentProperty); }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }

        /// <inheritdoc/>
        void IContentPresenterHost.RegisterContentPresenter(IContentPresenter presenter)
        {
            Presenter = presenter;
        }
    }
}
