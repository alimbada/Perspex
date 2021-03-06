﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.Styling;

namespace Perspex.Controls.Presenters
{
    /// <summary>
    /// Represents a control which hosts a content presenter.
    /// </summary>
    /// <remarks>
    /// This interface is implemented by <see cref="ContentControl"/> which usually contains a
    /// <see cref="ContentPresenter"/> and exposes it through its 
    /// <see cref="ContentControl.Presenter"/> property. ContentPresenters can be within
    /// nested templates or in popups and so are not necessarily created immediately when the
    /// parent control's template is instantiated so they register themselves using this 
    /// interface.
    /// </remarks>
    public interface IContentPresenterHost : ITemplatedControl
    {
        /// <summary>
        /// Registers an <see cref="IContentPresenter"/> with a host control.
        /// </summary>
        /// <param name="presenter">The content presenter.</param>
        void RegisterContentPresenter(IContentPresenter presenter);
    }
}
